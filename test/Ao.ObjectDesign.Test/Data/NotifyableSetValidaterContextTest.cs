using Ao.ObjectDesign.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Test.Data
{
    [TestClass]
    public class NotifyableSetValidaterContextTest
    {
        [TestMethod]
        public void SetStatus_StatusChanged()
        {
            var ctx = new NotifyableSetValidaterContext();
            ctx.SkipGlobalValidate();
            Assert.IsTrue(ctx.IsSkipGlobalValidate);

            ctx = new NotifyableSetValidaterContext();
            ctx.SkipWithKeyValidate();
            Assert.IsTrue(ctx.IsSkipWithKeyValidate);

            ctx = new NotifyableSetValidaterContext();
            ctx.StopValidate();
            Assert.IsTrue(ctx.IsStopValidate);
        }

        [TestMethod]
        public void EqualsAndGetHashCode()
        {
            var a = new NotifyableSetValidaterContext();
            var a1 = new NotifyableSetValidaterContext();

            var b = new NotifyableSetValidaterContext();
            b.SkipGlobalValidate();

            var b1 = new NotifyableSetValidaterContext();
            b1.SkipGlobalValidate();

            var c = new NotifyableSetValidaterContext();
            c.SkipWithKeyValidate();

            var c1 = new NotifyableSetValidaterContext();
            c1.SkipWithKeyValidate();

            var d = new NotifyableSetValidaterContext();
            d.StopValidate();

            var d1 = new NotifyableSetValidaterContext();
            d1.StopValidate();

            var e = new NotifyableSetValidaterContext();
            e.SkipWithKeyValidate();
            e.StopValidate();
            e.SkipGlobalValidate();

            var e1 = new NotifyableSetValidaterContext();
            e1.SkipWithKeyValidate();
            e1.StopValidate();
            e1.SkipGlobalValidate();

            Assert.IsTrue(a.Equals(a1));
            Assert.IsTrue(b.Equals(b1));
            Assert.IsTrue(c.Equals(c1));
            Assert.IsTrue(d.Equals(d1));
            Assert.IsTrue(e.Equals(e1));

            Assert.IsFalse(a.Equals(b1));
            Assert.IsFalse(a.Equals(new object()));
            Assert.IsFalse(a.Equals(c1));
            Assert.IsFalse(a.Equals(e1));

            Assert.AreEqual(a.GetHashCode(), a1.GetHashCode());
            Assert.AreEqual(b.GetHashCode(), b1.GetHashCode());
            Assert.AreEqual(c.GetHashCode(), c1.GetHashCode());
            Assert.AreEqual(d.GetHashCode(), d1.GetHashCode());
            Assert.AreEqual(e.GetHashCode(), e1.GetHashCode());

            Assert.AreEqual(a.ToString(), a1.ToString());
            Assert.AreEqual(b.ToString(), b1.ToString());
            Assert.AreEqual(c.ToString(), c1.ToString());
            Assert.AreEqual(d.ToString(), d1.ToString());
            Assert.AreEqual(e.ToString(), e1.ToString());
        }
    }
}
