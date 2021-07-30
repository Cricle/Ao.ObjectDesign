using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class PropertyIdentityComparerTest
    {
        [TestMethod]
        public void GetTwice_MustEqual()
        {
            var a = PropertyIdentityComparer.Instance;
            var b = PropertyIdentityComparer.Instance;

            Assert.AreEqual(a, b);
        }
        [TestMethod]
        public void Equals()
        {
            var inst = PropertyIdentityComparer.Instance;

            var a = new PropertyIdentity(typeof(object), "a");
            var b = new PropertyIdentity(typeof(object), "a");

            Assert.IsTrue(inst.Equals(a, b));
            Assert.IsTrue(inst.Equals(null,null));
            Assert.IsFalse(inst.Equals(null, b));
            Assert.IsFalse(inst.Equals(a, null));
        }
        [TestMethod]
        public void GivenNullGetHashCode_MustThrowException()
        {
            var inst = PropertyIdentityComparer.Instance;
            Assert.ThrowsException<ArgumentNullException>(() => inst.GetHashCode(null));
        }
        [TestMethod]
        public void GetHashCode_MustEqualInstGetHashCode()
        {
            var inst = PropertyIdentityComparer.Instance;
            var p = new PropertyIdentity(typeof(object), "a");
            var hash = p.GetHashCode();
            var act = inst.GetHashCode(p);
            Assert.AreEqual(hash, act);
        }
    }
}
