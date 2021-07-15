using Ao.ObjectDesign.Wpf.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Test.Data
{
    [TestClass]
    public class BindingUnitTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            var binding = new Binding();
            var prop = Window.ActualHeightProperty;
            Assert.ThrowsException<ArgumentNullException>(() => new BindingUnit(binding, null));
            Assert.ThrowsException<ArgumentNullException>(() => new BindingUnit(null, prop));
        }
        [TestMethod]
        public void GivenValueInit_FieldMustEquanInput()
        {
            var binding = new Binding();
            var prop = Window.ActualHeightProperty;
            var unit = new BindingUnit(binding, prop);
            Assert.AreEqual(binding, unit.Binding);
            Assert.AreEqual(prop, unit.DependencyProperty);
        }
        [TestMethod]
        public void EqualsAndGetHashCode()
        {
            var binding = new Binding();
            var prop = Window.ActualHeightProperty;
            var prop2 = Window.AllowDropProperty;
            var a = new BindingUnit(binding, prop);
            var b = new BindingUnit(binding, prop);
            var c = new BindingUnit(binding, prop2);
            BindingUnit d=default;

            Assert.IsTrue(a.Equals(b));
            Assert.IsFalse(a.Equals(c));
            Assert.IsFalse(a.Equals(d));

            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
            Assert.AreNotEqual(a.GetHashCode(), c.GetHashCode());
            Assert.AreNotEqual(a.GetHashCode(), d.GetHashCode());
        }
    }
}
