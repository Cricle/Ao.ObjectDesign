using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

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
        public void GivenNullCall_MustThrowException()
        {
            var inst = new object();
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.Clone((Type)null, inst));
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.Clone((object)null, inst));
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.Clone(1, null));
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.Clone<object>(null));
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.Clone<object>(1, null));
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.Clone(null, 1, ReadOnlyHashSet<Type>.Empty));
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.Clone(1, null, ReadOnlyHashSet<Type>.Empty));
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.Clone(1, 1, null));

            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.GetDefaultValueMap(null, false));

            var propInfo = typeof(Class).GetProperty(nameof(Class.Student1));
            var identity = new PropertyIdentity(typeof(Class), nameof(Class.Student1));
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.GetValue(null, propInfo));
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.GetValue(null, identity));
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.GetValue(1, (PropertyInfo)null));

            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.Create(null));

            var type = typeof(object);
            var dents = new Dictionary<Type, IReadOnlyDictionary<PropertyIdentity, object>>();
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.SetDefault(null, type, SetDefaultOptions.ClassGenerateNew));
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.SetDefault(inst, (Type)null, SetDefaultOptions.ClassGenerateNew));
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.SetDefault(null, SetDefaultOptions.ClassGenerateNew));
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.SetDefault(inst, (IReadOnlyDictionary<Type, IReadOnlyDictionary<PropertyIdentity, object>>)null, SetDefaultOptions.ClassGenerateNew));
            Assert.ThrowsException<ArgumentNullException>(() => ReflectionHelper.SetDefault(null, dents, SetDefaultOptions.ClassGenerateNew));
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

            ReflectionHelper.SetValue(stu, "hello", identity);
            Assert.AreEqual("hello", stu.Name);

            ReflectionHelper.SetValue(stu, "world", stu.GetType().GetProperty(nameof(Student.Name)));
            Assert.AreEqual("world", stu.Name);
        }
        class Item
        {
            public int Id { get; set; }
        }
        class ArrayBox
        {
            public List<int> Ints { get; set; }

            public List<Item> Items { get; set; }
        }
        class DictonaryBox
        {
            public Dictionary<int,int> Ints { get; set; }

            public Dictionary<int,Item> Items { get; set; }
        }
        [TestMethod]
        public void CanDirectClone()
        {
            var lst = new List<int> { 1, 3, 4, 5 };

            var next=ReflectionHelper.Clone(lst);

            Assert.AreNotSame(lst,next);
            Assert.AreEqual(4, next.Count);
            Assert.AreEqual(1, next[0]);
            Assert.AreEqual(3, next[1]);
            Assert.AreEqual(4, next[2]);
            Assert.AreEqual(5, next[3]);

            var map=new Dictionary<int, Item>
            {
                [1]=new Item { Id=3},
                [4]=new Item { Id=4}
            };
            var nextm = ReflectionHelper.Clone(map);

            Assert.AreNotSame(map, nextm);
            Assert.AreEqual(2, nextm.Count);
            Assert.AreNotSame(map[1], nextm[1]);
            Assert.AreNotSame(map[4], nextm[4]);
            Assert.AreNotSame(map[1].Id, nextm[1].Id);
            Assert.AreNotSame(map[4].Id, nextm[4].Id);
        }
        [TestMethod]
        public void CanCloneDictionary()
        {
            var box = new DictonaryBox
            {
                 Ints=new Dictionary<int, int>
                 {
                     [1]=2,
                     [2]=3,
                 },
                 Items=new Dictionary<int, Item> 
                 {
                     [1]=new Item { Id=2},
                     [2]=new Item { Id=3},
                 }
            };

            var next = ReflectionHelper.Clone(box);

            Assert.AreNotSame(box.Ints, next.Ints);
            Assert.AreEqual(2, next.Ints.Count);
            Assert.AreEqual(2, next.Ints[1]);
            Assert.AreEqual(3, next.Ints[2]);

            Assert.AreNotSame(box.Items, next.Items);
            Assert.AreEqual(2, next.Items.Count);
            Assert.AreEqual(2, next.Items[1].Id);
            Assert.AreEqual(3, next.Items[2].Id);
        }
        [TestMethod]
        public void CanCloneArray()
        {
            var box = new ArrayBox
            {
                Ints = new List<int> { 1, 2 },
                Items = new List<Item> { new Item { Id = 1 }, new Item { Id = 2 } }
            };

            var next = ReflectionHelper.Clone(box);

            Assert.AreNotSame(box.Ints, next.Ints);
            Assert.AreEqual(2,next.Ints.Count);
            Assert.AreEqual(1, next.Ints[0]);
            Assert.AreEqual(2, next.Ints[1]);

            Assert.AreNotSame(box.Items, next.Items);
            Assert.AreEqual(2, next.Items.Count);
            Assert.AreEqual(1, next.Items[0].Id);
            Assert.AreEqual(2, next.Items[1].Id);

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
            ReflectionHelper.SetDefault(c, SetDefaultOptions.IgnoreNotNull | SetDefaultOptions.Deep);
            Assert.AreEqual("c1", c.Name);
            Assert.AreEqual(11, c.Count);
            Assert.IsNull(c.Student2);
            Assert.IsNull(c.Student1);
        }
    }
}
