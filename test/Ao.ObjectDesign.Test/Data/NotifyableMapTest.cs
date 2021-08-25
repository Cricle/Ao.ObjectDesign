using Ao.ObjectDesign.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Test.Data
{
    [TestClass]
    public class NotifyableMapTest
    {
        
        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void AddOrUpdate(bool concurrent)
        {
            var m = new MyNotifyableMap<int, int> { Concurrent = concurrent };
            object sender = null;
            DataChangedEventArgs<int, int> args = null;
            m.DataChanged += (o, e) =>
            {
                sender = o;
                args = e;
            };
            m.AddOrUpdate(1, 2);
            Assert.AreEqual(2, m[1]);
            Assert.AreEqual(m, sender);
            Assert.AreEqual(0, args.Old);
            Assert.AreEqual(2, args.New);
            Assert.AreEqual(ChangeModes.New, args.Mode);

            m.AddOrUpdate(1, 3);
            Assert.AreEqual(3, m[1]);
            Assert.AreEqual(m, sender);
            Assert.AreEqual(2, args.Old);
            Assert.AreEqual(3, args.New);
            Assert.AreEqual(ChangeModes.Change, args.Mode);
        }
        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void GetOrAdd(bool concurrent)
        {
            var m = new MyNotifyableMap<int, int> { Concurrent = concurrent };
            object sender = null;
            DataChangedEventArgs<int, int> args = null;
            m.DataChanged += (o, e) =>
            {
                sender = o;
                args = e;
            };
            m.GetOrAdd(1, _ => 10);
            Assert.AreEqual(10, m[1]);
            Assert.AreEqual(m, sender);
            Assert.AreEqual(0, args.Old);
            Assert.AreEqual(10, args.New);
            Assert.AreEqual(ChangeModes.New, args.Mode);

            sender = null;
            args = null;
            m.GetOrAdd(1, _ => 20);
            Assert.AreEqual(10, m[1]);
            Assert.IsNull(sender);
            Assert.IsNull(args);
        }
        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void Remove(bool concurrent)
        {
            var m = new MyNotifyableMap<int, int> { Concurrent = concurrent };
            m.AddOrUpdate(1, 100);
            object sender = null;
            DataChangedEventArgs<int, int> args = null;
            m.DataChanged += (o, e) =>
            {
                sender = o;
                args = e;
            };
            var val=m.Remove(1);
            Assert.IsTrue(val);
            Assert.AreEqual(m, sender);
            Assert.AreEqual(100, args.Old);
            Assert.AreEqual(0, args.New);
            Assert.AreEqual(ChangeModes.Reset, args.Mode);
            
            sender = null;
            args = null;
            val = m.Remove(1);
            Assert.IsFalse(val);
            Assert.IsNull(sender);
            Assert.IsNull(args);
        }
        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void Clear(bool concurrent)
        {
            var m = new MyNotifyableMap<int, int> { Concurrent = concurrent };
            m.AddOrUpdate(1, 100);
            m.AddOrUpdate(1, 100);
            m.AddOrUpdate(1, 100);
            m.AddOrUpdate(1, 100);
            m.AddOrUpdate(1, 100);
            object sender = null;
            EventArgs args = null;
            m.Clean += (o, e) =>
            {
                sender = o;
                args = e;
            };
            m.Clear();
            Assert.AreEqual(m, sender);
            Assert.AreEqual(args, EventArgs.Empty);
            Assert.AreEqual(0, m.Count);
        }
    }
}
