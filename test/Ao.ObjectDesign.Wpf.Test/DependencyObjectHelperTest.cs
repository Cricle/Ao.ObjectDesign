using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Test
{

    [TestClass]
    public class DependencyObjectHelperTest
    {
        [TestMethod]
        public void LookupDependencyPropertyDescriptor()
        {
            var desc=DependencyObjectHelper.GetPropertyDescriptors(typeof(MyDepObject)).ToArray();
            Assert.AreEqual(1, desc.Length);
            Assert.AreEqual(nameof(MyDepObject.Age), desc[0].Name);
        }
        [TestMethod]
        public void ToDependencyPropertyDescriptors()
        {
            var desc = DependencyObjectHelper.GetDependencyPropertyDescriptors(typeof(MyDepObject));
            Assert.AreEqual(1, desc.Count);
            Assert.IsTrue(desc.ContainsKey(nameof(MyDepObject.Age)));
            Assert.AreEqual(nameof(MyDepObject.Age), desc[nameof(MyDepObject.Age)].Name);
        }
        [TestMethod]
        public void LookupDependencyProperty()
        {
            var desc = DependencyObjectHelper.GetDependencyProperties(typeof(MyDepObject)).ToArray();
            Assert.AreEqual(1, desc.Length);
            Assert.AreEqual(MyDepObject.AgeProperty, desc[0]);
        }
        [TestMethod]
        public void IsDependencyProperty_ContainsMustTrue_NotMustFalse()
        {
            var r = DependencyObjectHelper.IsDependencyProperty(typeof(MyDepObject), nameof(MyDepObject.Age));
            Assert.IsTrue(r);
            r = DependencyObjectHelper.IsDependencyProperty(typeof(MyDepObject), "none");
            Assert.IsFalse(r);
            r = DependencyObjectHelper.IsDependencyProperty(typeof(MyDepObject), nameof(MyDepObject.Age));
            Assert.IsTrue(r);
        }
    }
}
