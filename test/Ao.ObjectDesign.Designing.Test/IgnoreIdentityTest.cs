using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing.Test
{
    [TestClass]
    public class IgnoreIdentityTest
    {
        [TestMethod]
        public void InitWithValue_FieldsValueMustEqualInput()
        {
            var inst = new object();
            var name = "hello";
            var identity = new IgnoreIdentity(inst, name);
            Assert.AreEqual(inst, identity.Instance);
            Assert.AreEqual(name, identity.PropertyName);
        }
        [TestMethod]
        public void EqualsAndGetHashCode()
        {
            var inst = new object();
            var inst2 = new object();
            var name = "hello";
            var name2 = "world";
            var a1 = new IgnoreIdentity(inst, name);
            var a11 = new IgnoreIdentity(inst, name);
            var a2 = new IgnoreIdentity(inst2, name);
            var a3 = new IgnoreIdentity(inst, name2);
            var a4 = new IgnoreIdentity(inst2, name2);

            Assert.IsTrue(a1.Equals(a11));
            Assert.IsFalse(a1.Equals(a2));
            Assert.IsFalse(a1.Equals(a3));
            Assert.IsFalse(a1.Equals(a4));

            Assert.AreEqual(a1.GetHashCode(), a11.GetHashCode());
            Assert.AreNotEqual(a1.GetHashCode(), a2.GetHashCode());
            Assert.AreNotEqual(a1.GetHashCode(), a3.GetHashCode());
            Assert.AreNotEqual(a1.GetHashCode(), a4.GetHashCode());
        }
        [TestMethod]
        public void AnyNullEqualsAndGetHashCode()
        {
            var inst = new object();
            var name = "h";
            var a1 = new IgnoreIdentity(null, name);
            var a11 = new IgnoreIdentity(null, name);
            var a2 = new IgnoreIdentity(inst, null);
            var a22 = new IgnoreIdentity(inst, null);
            var a3 = new IgnoreIdentity(null, null);
            var a33 = new IgnoreIdentity();

            Assert.IsTrue(a1.Equals(a11));
            Assert.IsTrue(a2.Equals(a22));
            Assert.IsTrue(a3.Equals(a33));

            Assert.IsTrue(a1.Equals((object)a11));
            Assert.IsTrue(a2.Equals((object)a22));
            Assert.IsTrue(a3.Equals((object)a33));
            Assert.IsFalse(a3.Equals(new object()));
            Assert.IsFalse(a3.Equals(null));

            Assert.AreEqual(a1.GetHashCode(), a11.GetHashCode());
            Assert.AreEqual(a2.GetHashCode(), a22.GetHashCode());
            Assert.AreEqual(a3.GetHashCode(), a33.GetHashCode());
            Assert.AreEqual(a3.ToString(), a33.ToString());
        }
    }
}
