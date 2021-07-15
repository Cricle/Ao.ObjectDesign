using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf.Test
{
    [TestClass]
    public class DesignMappingTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            var type = typeof(object);
            Assert.ThrowsException<ArgumentNullException>(() => new DesignMapping(type, null));
            Assert.ThrowsException<ArgumentNullException>(() => new DesignMapping(null, type));
        }
        [TestMethod]
        public void GivenValueInit_PropertyValueMustEqualInput()
        {
            var t1 = typeof(object);
            var t2 = typeof(int);
            var mapping = new DesignMapping(t1, t2);
            Assert.AreEqual(t1, mapping.ClrType);
            Assert.AreEqual(t2, mapping.UIType);

            mapping.ToString();
        }
        [TestMethod]
        public void EqualsAndGetHashCode()
        {
            var t1 = typeof(object);
            var t2 = typeof(int);
            var mapping1 = new DesignMapping(t1, t2);
            var mapping2 = new DesignMapping(t1, t2);
            var mapping3 = new DesignMapping(t2, typeof(double));

            Assert.IsTrue(mapping1.Equals(mapping2));
            Assert.IsTrue(mapping1.Equals((object)mapping2));
            Assert.IsFalse(mapping1.Equals(mapping3));
            Assert.IsFalse(mapping1.Equals((object)mapping3));
            Assert.IsFalse(mapping1.Equals((object)null));
            Assert.IsFalse(mapping1.Equals((DesignMapping)null));

            Assert.AreEqual(mapping1.GetHashCode(), mapping2.GetHashCode());
            Assert.AreNotEqual(mapping1.GetHashCode(), mapping3.GetHashCode());
        }
    }
}