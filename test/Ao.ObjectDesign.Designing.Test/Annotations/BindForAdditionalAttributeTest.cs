using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ao.ObjectDesign.Designing.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ao.ObjectDesign.Designing.Test.Annotations
{
    [TestClass]
    public class BindForAdditionalAttributeTest
    {
        [TestMethod]
        public void GivenNull_MustThrowException()
        {
            var name = "asda";
            var type = typeof(object);
            Assert.ThrowsException<ArgumentException>(() => new BindForAdditionalAttribute(null));
            Assert.ThrowsException<ArgumentException>(() => new BindForAdditionalAttribute(type, null));
            Assert.ThrowsException<ArgumentNullException>(() => new BindForAdditionalAttribute(null, name));
        }
        [TestMethod]
        public void GivenValueInit_PropertyValueMustEqualInput()
        {
            var name = "asda";
            var type = typeof(object);

            var attr = new BindForAdditionalAttribute(name);
            Assert.IsNull(attr.DependencyObjectType);
            Assert.AreEqual(name, attr.DependencyPropertyName);

            attr = new BindForAdditionalAttribute(type,name);
            Assert.AreEqual(type,attr.DependencyObjectType);
            Assert.AreEqual(name, attr.DependencyPropertyName);
        }
    }
}
