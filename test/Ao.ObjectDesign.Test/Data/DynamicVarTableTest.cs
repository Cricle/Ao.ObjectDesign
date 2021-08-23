﻿using Ao.ObjectDesign.Data;
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

            tb.SetValue("a", "aaa");

            Assert.AreEqual("a", map.Keys.Single());
            Assert.AreEqual("aaa", map.Values.Single().Value);

            var val = new RefValue("bbb");

            tb.SetValue("a",val);

            Assert.AreEqual("a", map.Keys.Single());
            Assert.AreEqual(val,map.Values.Single());
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
