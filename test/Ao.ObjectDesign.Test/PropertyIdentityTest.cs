using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class PropertyIdentityTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            var type = typeof(object);
            var name = "hello";
            Assert.ThrowsException<ArgumentNullException>(() => new PropertyIdentity(null,name));
            Assert.ThrowsException<ArgumentException>(() => new PropertyIdentity(type, null));
        }
        [TestMethod]
        public void GivenIdentityInit_MustCopy()
        {
            var type = typeof(object);
            var name = "hello";
            var identity1 = new PropertyIdentity(type, name);
            var identity2 = new PropertyIdentity(identity1);

            Assert.AreEqual(identity1.Type, identity2.Type);
            Assert.AreEqual(identity1.PropertyName, identity2.PropertyName);
        }
        [TestMethod]
        public void GivenValueInit_PropertyValueMustEqualInput()
        {
            var type = typeof(object);
            var name = "hello";
            var identity = new PropertyIdentity(type, name);
            Assert.AreEqual(type, identity.Type);
            Assert.AreEqual(name, identity.PropertyName);
        }
        [TestMethod]
        public void EqualsAndGetHashCode()
        {
            var type = typeof(object);
            var name = "hello";
            var name2 = "hello2";
            var identity = new PropertyIdentity(type, name);
            var identity2 = new PropertyIdentity(type, name);
            var identity3 = new PropertyIdentity(type, name2);

            Assert.IsTrue(identity.Equals(identity2));
            Assert.IsFalse(identity.Equals(identity3));
            Assert.IsFalse(identity.Equals((object)null));
            Assert.IsFalse(identity.Equals(default));

            Assert.AreEqual(identity.GetHashCode(), identity2.GetHashCode());
            Assert.AreNotEqual(identity.GetHashCode(), identity3.GetHashCode());

            Assert.AreEqual(identity.ToString(), identity2.ToString());
            Assert.AreNotEqual(identity.ToString(), identity3.ToString());
        }
    }
}
