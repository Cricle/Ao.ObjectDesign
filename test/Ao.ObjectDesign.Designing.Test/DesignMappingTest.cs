using Ao.ObjectDesign.Designing.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ao.ObjectDesign.Designing.Test
{
    [TestClass]
    public class DesignMappingTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            Type type = typeof(object);
            Assert.ThrowsException<ArgumentNullException>(() => new DesignMapping(type, null));
            Assert.ThrowsException<ArgumentNullException>(() => new DesignMapping(null, type));
        }
        [TestMethod]
        public void GivenValueInit_PropertyValueMustEqualInput()
        {
            Type t1 = typeof(object);
            Type t2 = typeof(int);
            DesignMapping mapping = new DesignMapping(t1, t2);
            Assert.AreEqual(t1, mapping.ClrType);
            Assert.AreEqual(t2, mapping.UIType);

            mapping.ToString();
        }
        [TestMethod]
        public void EqualsAndGetHashCode()
        {
            Type t1 = typeof(object);
            Type t2 = typeof(int);
            DesignMapping mapping1 = new DesignMapping(t1, t2);
            DesignMapping mapping2 = new DesignMapping(t1, t2);
            DesignMapping mapping3 = new DesignMapping(t2, typeof(double));

            Assert.IsTrue(mapping1.Equals(mapping2));
            Assert.IsTrue(mapping1.Equals((object)mapping2));
            Assert.IsFalse(mapping1.Equals(mapping3));
            Assert.IsFalse(mapping1.Equals((object)mapping3));
            Assert.IsFalse(mapping1.Equals((object)null));
            Assert.IsFalse(mapping1.Equals((DesignMapping)null));

            Assert.AreEqual(mapping1.GetHashCode(), mapping2.GetHashCode());
            Assert.AreNotEqual(mapping1.GetHashCode(), mapping3.GetHashCode());
        }
        [MappingFor(typeof(OtherType))]
        class AnyMappingFor
        {

        }
        class OtherType
        {

        }
        [TestMethod]
        public void FromMapping_WasCreatedByAttribute()
        {
            var val = DesignMapping.FromMapping(typeof(AnyMappingFor));

            Assert.AreEqual(typeof(AnyMappingFor), val.ClrType);
            Assert.AreEqual(typeof(OtherType), val.UIType);

            Assert.ThrowsException<ArgumentException>(() => DesignMapping.FromMapping(typeof(OtherType)));
        }
    }
}