using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class ReflectionHelperTest
    {
        class Class
        {
            public Student Student1 { get; set; }

            public Student Student2 { get; set; }
            [DefaultValue("c1")]
            public string Name { get; set; }
            [DefaultValue(11)]
            public int Count { get; set; }
        }
        class Student
        {
            [DefaultValue("world")]
            public string Name { get; set; }
        }

        [TestMethod]
        public void Create_MustGotInstance()
        {
            var val = ReflectionHelper.Create(typeof(Student));
            Assert.IsInstanceOfType(val, typeof(Student));
        }
        [TestMethod]
        public void GetValue_MustGotPropertyValue()
        {
            var stu = new Student { Name = "hello" };
            var identity = new PropertyIdentity(stu.GetType(), nameof(Student.Name));

            var val = ReflectionHelper.GetValue(stu, identity);
            Assert.AreEqual(stu.Name, val);

            val = ReflectionHelper.GetValue(stu, stu.GetType().GetProperty(nameof(Student.Name)));
            Assert.AreEqual(stu.Name, val);
        }
        [TestMethod]
        public void SetValue_MustSettedPropertyValue()
        {
            var stu = new Student();
            var identity = new PropertyIdentity(stu.GetType(), nameof(Student.Name));

            ReflectionHelper.SetValue(stu,"hello", identity);
            Assert.AreEqual("hello", stu.Name);

            ReflectionHelper.SetValue(stu,"world", stu.GetType().GetProperty(nameof(Student.Name)));
            Assert.AreEqual("world", stu.Name);
        }
        [TestMethod]
        public void Clone_MustEqual()
        {
            var c = new Class
            {
                Name = "c1",
                Count = 22,
                Student1 = new Student { Name = "s1" },
                Student2 = new Student { Name = "s2" }
            };

            var next = ReflectionHelper.Clone(c);

            Assert.AreNotEqual(c, next);
            Assert.AreEqual(c.Name, next.Name);
            Assert.AreEqual(c.Count, next.Count);
            Assert.AreNotEqual(c.Student1, next.Student1);
            Assert.AreNotEqual(c.Student2, next.Student2);
            Assert.AreEqual(c.Student1.Name, next.Student1.Name);
            Assert.AreEqual(c.Student2.Name, next.Student2.Name);
        }
        [TestMethod]
        public void SetDefault_AllMustBeSetted()
        {
            var c = new Class { Student1 = new Student() };
            ReflectionHelper.SetDefault(c, SetDefaultOptions.Deep);
            Assert.AreEqual("c1", c.Name);
            Assert.AreEqual(11, c.Count);
            Assert.IsNull(c.Student2);
            Assert.AreEqual("world", c.Student1.Name);

            c = new Class { Student1 = new Student() };
            ReflectionHelper.SetDefault(c, SetDefaultOptions.IgnoreNotNull| SetDefaultOptions.Deep);
            Assert.AreEqual("c1", c.Name);
            Assert.AreEqual(11, c.Count);
            Assert.IsNull(c.Student2);
            Assert.IsNull(c.Student1);
        }
    }
}
