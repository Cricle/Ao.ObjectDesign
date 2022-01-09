using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            var a = new PropertyIdentity(typeof(A), "Name");
            var b = new PropertyIdentity(typeof(A), "Name");

            Assert.IsTrue(inst.Equals(a, b));
            Assert.IsTrue(inst.Equals(default, default));
            Assert.IsFalse(inst.Equals(default, b));
            Assert.IsFalse(inst.Equals(a, default));
        }
        [TestMethod]
        public void GetHashCode_MustEqualInstGetHashCode()
        {
            var inst = PropertyIdentityComparer.Instance;
            var p = new PropertyIdentity(typeof(A), "Name");
            var hash = p.GetHashCode();
            var act = inst.GetHashCode(p);
            Assert.AreEqual(hash, act);
        }
    }
}
