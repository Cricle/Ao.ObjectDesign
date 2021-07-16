using Ao.ObjectDesign.Wpf.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            Binding binding = new Binding();
            DependencyProperty prop = Window.ActualHeightProperty;
            Assert.ThrowsException<ArgumentNullException>(() => new BindingUnit(binding, null));
            Assert.ThrowsException<ArgumentNullException>(() => new BindingUnit(null, prop));
        }
        [TestMethod]
        public void GivenValueInit_FieldMustEquanInput()
        {
            Binding binding = new Binding();
            DependencyProperty prop = Window.ActualHeightProperty;
            BindingUnit unit = new BindingUnit(binding, prop);
            Assert.AreEqual(binding, unit.Binding);
            Assert.AreEqual(prop, unit.DependencyProperty);
        }
        [TestMethod]
        public void EqualsAndGetHashCode()
        {
            Binding binding = new Binding();
            DependencyProperty prop = Window.ActualHeightProperty;
            DependencyProperty prop2 = Window.AllowDropProperty;
            BindingUnit a = new BindingUnit(binding, prop);
            BindingUnit b = new BindingUnit(binding, prop);
            BindingUnit c = new BindingUnit(binding, prop2);
            BindingUnit d = default;

            Assert.IsTrue(a.Equals(b));
            Assert.IsFalse(a.Equals(c));
            Assert.IsFalse(a.Equals(d));

            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
            Assert.AreNotEqual(a.GetHashCode(), c.GetHashCode());
            Assert.AreNotEqual(a.GetHashCode(), d.GetHashCode());
        }
    }
}
