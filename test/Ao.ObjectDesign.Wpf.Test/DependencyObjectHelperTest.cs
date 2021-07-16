using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Test
{

    [TestClass]
    public class DependencyObjectHelperTest
    {
        [TestMethod]
        public void LookupDependencyPropertyDescriptor()
        {
            System.ComponentModel.PropertyDescriptor[] desc = DependencyObjectHelper.GetPropertyDescriptors(typeof(MyDepObject)).ToArray();
            Assert.AreEqual(1, desc.Length);
            Assert.AreEqual(nameof(MyDepObject.Age), desc[0].Name);
        }
        [TestMethod]
        public void ToDependencyPropertyDescriptors()
        {
            IReadOnlyDictionary<string, System.ComponentModel.DependencyPropertyDescriptor> desc = DependencyObjectHelper.GetDependencyPropertyDescriptors(typeof(MyDepObject));
            Assert.AreEqual(1, desc.Count);
            Assert.IsTrue(desc.ContainsKey(nameof(MyDepObject.Age)));
            Assert.AreEqual(nameof(MyDepObject.Age), desc[nameof(MyDepObject.Age)].Name);
        }
        [TestMethod]
        public void LookupDependencyProperty()
        {
            DependencyProperty[] desc = DependencyObjectHelper.GetDependencyProperties(typeof(MyDepObject)).ToArray();
            Assert.AreEqual(1, desc.Length);
            Assert.AreEqual(MyDepObject.AgeProperty, desc[0]);
        }
        [TestMethod]
        public void IsDependencyProperty_ContainsMustTrue_NotMustFalse()
        {
            bool r = DependencyObjectHelper.IsDependencyProperty(typeof(MyDepObject), nameof(MyDepObject.Age));
            Assert.IsTrue(r);
            r = DependencyObjectHelper.IsDependencyProperty(typeof(MyDepObject), "none");
            Assert.IsFalse(r);
            r = DependencyObjectHelper.IsDependencyProperty(typeof(MyDepObject), nameof(MyDepObject.Age));
            Assert.IsTrue(r);
        }
    }
}
