using Ao.ObjectDesign.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Test.Data
{
    [TestClass]
    public class DynamicVarTableTest
    {
        class IntDynamicVarTable : DynamicVarTable<int>
        {
            public IntDynamicVarTable(NotifyableMap<int, IVarValue> dataMap) : base(dataMap)
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
            protected override object VisitValue(IVarValue value)
            {
                return value;
            }
        }
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            var map = new NotifyableMap<string, IVarValue>();
            var set = ReadOnlyHashSet<string>.Empty;

            Assert.ThrowsException<ArgumentNullException>(() => new DynamicVarTable(null));
            Assert.ThrowsException<ArgumentNullException>(() => new DynamicVarTable(map, null));
            Assert.ThrowsException<ArgumentNullException>(() => new DynamicVarTable(null, set));
        }
        [TestMethod]
        public void ListenOrUnListenTwice_NothingTodo()
        {
            var map = new NotifyableMap<string, IVarValue>();
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
            var map = new NotifyableMap<string, IVarValue>();
            map.AddOrUpdate("a", VarValue.Byte0Value);
            map.AddOrUpdate("b", VarValue.Byte0Value);
            map.AddOrUpdate("c", VarValue.Byte0Value);
            map.AddOrUpdate("d", VarValue.Byte0Value);
            map.AddOrUpdate("e", VarValue.Byte0Value);
            map.AddOrUpdate("f", VarValue.Byte0Value);
            map.AddOrUpdate("g", VarValue.Byte0Value);
            var tb = new DynamicVarTable(map);

            var vals = tb.GetDynamicMemberNames();

            foreach (var item in vals)
            {
                if (!map.ContainsKey(item))
                {
                    Assert.Fail("{0} is not return",item);
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
            var map = new NotifyableMap<string, IVarValue>();
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

            map.AddOrUpdate("a", VarValue.Float0Value);

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
            var map = new NotifyableMap<string, IVarValue>();
            var tb = new DynamicVarTable(map);
            dynamic dtb = tb;

            tb.SetValue("a", "aaa");

            Assert.AreEqual("a", map.Keys.Single());
            Assert.AreEqual("aaa", map.Values.Single().Value);

            dtb.a = 123;
            Assert.AreEqual(123, dtb.a);

            var val = new RefValue("bbb");

            tb.SetValue("a",val);

            Assert.AreEqual("a", map.Keys.Single());
            Assert.AreEqual(val,map.Values.Single());

            var map1 = new NotifyableMap<int, IVarValue>();
            var tb1 = new IntDynamicVarTable(map1);
            dynamic dtb1 = tb1;

            tb1.SetValue(1, VarValue.Char0Value);

            Assert.AreEqual(VarValue.Char0Value, dtb1[1]);
            Assert.AreEqual(VarValue.Char0Value, dtb1["1"]);
            Assert.AreEqual(VarValue.Char0Value, dtb1["1"]);

        }
        [TestMethod]
        public void UseageTest()
        {
            var map = new NotifyableMap<string, IVarValue>();
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
