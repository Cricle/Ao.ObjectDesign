using Ao.ObjectDesign.Designing.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ao.ObjectDesign.Designing.Test.Annotations
{
    [TestClass]
    public class HasSettingAttributeTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new HasSettingAttribute(null));
        }
        [TestMethod]
        public void GivenValueInit_PropertyMustEqualInput()
        {
            var t = typeof(object);
            var attr = new HasSettingAttribute(t);
            Assert.AreEqual(t, attr.SettingType);
        }
    }
}
