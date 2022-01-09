using Ao.ObjectDesign.Designing.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ao.ObjectDesign.Designing.Test.Annotations
{
    [TestClass]
    public class TransferOriginAttributeTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new TransferOriginAttribute(null));
        }
        [TestMethod]
        public void GivenValueInit_PropertyMustEqualInput()
        {
            var t = typeof(object);
            var attr = new TransferOriginAttribute(t);
            Assert.AreEqual(t, attr.Origin);
        }
    }
}
