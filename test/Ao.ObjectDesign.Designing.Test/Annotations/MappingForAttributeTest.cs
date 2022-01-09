using Ao.ObjectDesign.Designing.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ao.ObjectDesign.Designing.Test.Annotations
{
    [TestClass]
    public class MappingForAttributeTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new MappingForAttribute(null));
        }
        [TestMethod]
        public void GivenValueInit_PropertyMustEqualInput()
        {
            var t = typeof(object);
            var attr = new MappingForAttribute(t);
            Assert.AreEqual(t, attr.Type);
        }
    }
}
