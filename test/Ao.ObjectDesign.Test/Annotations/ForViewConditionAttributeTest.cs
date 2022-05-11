using Ao.ObjectDesign.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ao.ObjectDesign.Test.Annotations
{
    [TestClass]
    public class ForViewConditionAttributeTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ForViewConditionAttribute((Type)null));
        }
        [TestMethod]
        public void SameTypeMustEquals_NotMustNotEqual()
        {
            ForViewConditionAttribute a = new ForViewConditionAttribute(typeof(object));
            ForViewConditionAttribute b = new ForViewConditionAttribute(typeof(object));
            ForViewConditionAttribute c = new ForViewConditionAttribute(typeof(int));

            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(a));
            Assert.IsFalse(a.Equals(c));
            Assert.IsFalse(a.Equals(null));
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
            Assert.AreNotEqual(a.GetHashCode(), c.GetHashCode());
            Assert.AreEqual(a.ToString(), b.ToString());
            Assert.AreNotEqual(a.ToString(), c.ToString());
        }
    }
}
