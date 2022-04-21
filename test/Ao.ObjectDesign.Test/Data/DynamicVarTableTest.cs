using Ao.ObjectDesign.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;
using System.Linq;

namespace Ao.ObjectDesign.Test.Data
{
    [TestClass]
    public class DynamicVarTableTest
    {
        class IntDynamicVarTable : DynamicVarTable<int,object>
        {
            public IntDynamicVarTable(NotifyableMap<int, object> dataMap) : base(dataMap)
            {
            }

            protected override int ToKey(string name)
            {
                return int.Parse(name);
            }

            protected override string ToName(int key)
            {
                return key.ToString();
            }
            protected override object VisitValue(object value)
            {
                return value;
            }
        }
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            var map = new NotifyableMap<string, object>();
            var set = ReadOnlyHashSet<string>.Empty;

            Assert.ThrowsException<ArgumentNullException>(() => new DynamicVarTable(null));
            Assert.ThrowsException<ArgumentNullException>(() => new DynamicVarTable(map, null));
            Assert.ThrowsException<ArgumentNullException>(() => new DynamicVarTable(null, set));
        }
        [TestMethod]
        public void ListenOrUnListenTwice_NothingTodo()
        {
            var map = new NotifyableMap<string, object>();
            var tb = new DynamicVarTable(map);

            tb.Listen();
            Assert.IsTrue(tb.IsListening);
            tb.Listen();
            Assert.IsTrue(tb.IsListening);

            tb.UnListen();
            Assert.IsFalse(tb.IsListening);
            tb.UnListen();
            Assert.IsFalse(tb.IsListening);
        }
        [TestMethod]
        public void GetDynamicMemberNames_AllAcceptWasReturnAll_NotReturlInputs()
        {
            var map = new NotifyableMap<string, object>();
            map.AddOrUpdate("a", (byte)0);
            map.AddOrUpdate("b", (byte)0);
            map.AddOrUpdate("c", (byte)0);
            map.AddOrUpdate("d", (byte)0);
            map.AddOrUpdate("e", (byte)0);
            map.AddOrUpdate("f", (byte)0);
            map.AddOrUpdate("g", (byte)0);
            var tb = new DynamicVarTable(map);

            var vals = tb.GetDynamicMemberNames();

            foreach (var item in vals)
            {
                if (!map.ContainsKey(item))
                {
                    Assert.Fail("{0} is not return", item);
                }
            }
            var set = new ReadOnlyHashSet<string>(new string[]
            {
                "a","c","qweqw"
            });
            tb = new DynamicVarTable(map, set);

            vals = tb.GetDynamicMemberNames();
            Assert.AreEqual(set.Count, vals.Count());
            foreach (var item in vals)
            {
                if (!set.Contains(item))
                {
                    Assert.Fail("{0} is not return", item);
                }
            }
        }
        [TestMethod]
        public void ListenChange()
        {
            var map = new NotifyableMap<string, object>();
            var tb = new DynamicVarTable(map);

            Assert.AreEqual(map, tb.DataMap);

            tb.Listen();
            object sender = null;
            PropertyChangedEventArgs args = null;
            tb.PropertyChanged += (o, e) =>
            {
                sender = o;
                args = e;
            };
            Assert.IsTrue(tb.IsListening);

            map.AddOrUpdate("a", 0f);

            Assert.AreEqual(tb, sender);
            Assert.AreEqual("a", args.PropertyName);

            sender = null;
            args = null;
            tb.UnListen();
            Assert.IsFalse(tb.IsListening);
            Assert.IsNull(sender);
            Assert.IsNull(args);
        }
        [TestMethod]
        public void SetValue()
        {
            var map = new NotifyableMap<string, object>();
            var tb = new DynamicVarTable(map);
            dynamic dtb = tb;

            tb.SetValue("a", "aaa");

            Assert.AreEqual("a", map.Keys.Single());
            Assert.AreEqual("aaa", map.Values.Single());

            dtb.a = 123;
            Assert.AreEqual(123, dtb.a);

            tb.SetValue("a", "bbb");

            Assert.AreEqual("a", map.Keys.Single());
            Assert.AreEqual("bbb", map.Values.Single());

            var map1 = new NotifyableMap<int, object>();
            var tb1 = new IntDynamicVarTable(map1);
            dynamic dtb1 = tb1;

            tb1.SetValue(1, 1);

            Assert.AreEqual(1, dtb1[1]);

        }
        [TestMethod]
        public void UseageTest()
        {
            var map = new NotifyableMap<string, object>();
            var tb = new DynamicVarTable(map);

            tb.Listen();

            object sender = null;
            PropertyChangedEventArgs args = null;
            tb.PropertyChanged += (o, e) =>
            {
                sender = o;
                args = e;
            };

            tb.SetValue("a", 1);

            Assert.AreEqual(tb, sender);
            Assert.AreEqual("a", args.PropertyName);

            sender = null;
            args = null;

            tb.SetValue("a", 2);

            Assert.AreEqual(tb, sender);
            Assert.AreEqual("a", args.PropertyName);

            sender = null;
            args = null;

            map.Remove("a");

            Assert.AreEqual(tb, sender);
            Assert.AreEqual("a", args.PropertyName);

            sender = null;
            args = null;

            dynamic dn = tb;

            dn["b"] = 222;

            Assert.AreEqual(tb, sender);
            Assert.AreEqual("b", args.PropertyName);

            sender = null;
            args = null;

            dn.b = 333;

            Assert.AreEqual(tb, sender);
            Assert.AreEqual("b", args.PropertyName);

            tb.UnListen();
        }
    }
}
