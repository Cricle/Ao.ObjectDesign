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
    public class BindForAttributeTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            var name = "dsadsa";
            var type = typeof(DependencyObject);
            Assert.ThrowsException<ArgumentException>(() => new BindForAttribute(null));
            Assert.ThrowsException<ArgumentException>(() => new BindForAttribute(type,null));
            Assert.ThrowsException<ArgumentNullException>(() => new BindForAttribute(null,name));
        }
        [TestMethod]
        public void GivenNotDependencyObjectOrBaseOnType_MustThrowExcpeiton()
        {
            var name = "dsadsa";
            var type = typeof(object);

            Assert.ThrowsException<ArgumentException>(() => new BindForAttribute(type, name));
        }
        [TestMethod]
        public void GivenValueInit_PropertyMustEqualInput()
        {
            var name = "dsadsa";
            var t = typeof(Button);
            var attr = new BindForAttribute(name);
            Assert.IsNull(attr.DependencyObjectType);
            Assert.AreEqual(name, attr.PropertyName);
            
            attr = new BindForAttribute(t, name);
            Assert.AreEqual(t, attr.DependencyObjectType);
            Assert.AreEqual(name, attr.PropertyName);
        }
        [TestMethod]
        public void GivenNotConvertType_MustThrowExcpetion()
        {
            var attr = new BindForAttribute("asd");
            var t = typeof(int);
            Assert.ThrowsException<ArgumentException>(() => attr.ConverterType = t);
        }
    }
}
