using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class CompiledPropertyVisitorTest
    {
        [TestMethod]
        public void GetSetValue()
        {
            var student = new Student();
            var prop = student.GetType().GetProperty(nameof(Student.Name));
            var c = new CompiledPropertyVisitor(student, prop);
            c.SetValue("aaa");
            Assert.AreEqual("aaa", student.Name);
            var val = c.GetValue();
            Assert.AreEqual("aaa", val);

            val = c.Value;
            Assert.AreEqual("aaa", val);
            c.Value = "bbb";
            Assert.AreEqual("bbb", student.Name);
            c.SetValue("ddd");
            Assert.AreEqual("ddd", student.Name);
        }
        [TestMethod]
        public void GetSetPrimaryValue()
        {
            var student = new ReadOnlyStudent();
            var prop = student.GetType().GetProperty(nameof(ReadOnlyStudent.Age));
            var c = new CompiledPropertyVisitor(student, prop);
            c.SetValue(333d);
            Assert.AreEqual(333d, student.Age);
            var val = c.GetValue();
            Assert.AreEqual(333d, val);

            val = c.Value;
            Assert.AreEqual(333d, val);
            c.Value =444d;
            Assert.AreEqual(444d, student.Age);
            c.SetValue(555d);
            Assert.AreEqual(555d, student.Age);
            c.SetValue(123.456d);
            Assert.AreEqual(123.456d, student.Age);

        }
        class ReadOnlyStudent
        {
            public string Name { get; } = "aaa";

            public double Age { get; set; } = 123d;
        }
        [TestMethod]
        public void ReadOnlyProperty_GetMustGotValue_SetThrowException()
        {
            var student = new ReadOnlyStudent();
            var prop = student.GetType().GetProperty(nameof(ReadOnlyStudent.Name));
            var c = new CompiledPropertyVisitor(student, prop);
            var val = c.GetValue();
            Assert.AreEqual("aaa", val);
            Assert.ThrowsException<InvalidOperationException>(() => c.SetValue("111"));
        }
    }
}
