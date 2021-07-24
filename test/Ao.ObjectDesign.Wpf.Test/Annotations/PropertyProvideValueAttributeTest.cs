using Ao.ObjectDesign.Wpf.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Wpf.Test.Annotations
{
    [TestClass]
    public class PropertyProvideValueAttributeTest
    {

        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            var name = "dsadsa";
            var type = typeof(DependencyObject);
            Assert.ThrowsException<ArgumentException>(() => new PropertyProvideValueAttribute(null));
            Assert.ThrowsException<ArgumentException>(() => new PropertyProvideValueAttribute(null, type));
            Assert.ThrowsException<ArgumentNullException>(() => new PropertyProvideValueAttribute(name,null));
        }
        [TestMethod]
        public void GivenValueInit_PropertyMustEqualInput()
        {
            var name = "dsadsa";
            var t = typeof(Button);
            var attr = new PropertyProvideValueAttribute(name);
            Assert.IsNull(attr.ProvideType);
            Assert.AreEqual(name, attr.PropertyName);

            attr = new PropertyProvideValueAttribute(name,t);
            Assert.AreEqual(t, attr.ProvideType);
            Assert.AreEqual(name, attr.PropertyName);
        }
    }
}
