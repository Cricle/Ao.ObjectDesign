using Ao.ObjectDesign.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Test.Data
{
    [TestClass]
    public class DesignPropertyDataBindingTest
    {
        public class NullDesignPropertyDataBinding : DesignPropertyDataBinding
        {
            protected override object GetValue(object instance, PropertyInfo sourceProp)
            {
                return null;
            }

            protected override void SetValue(object instance, PropertyInfo sourceProp, object value)
            {
            }
        }
        private NullDesignPropertyDataBinding CreateBinding()
        {
            return new NullDesignPropertyDataBinding
            {
                Source = new Student(),
                Target = new Student(),
                SourcePropertyName = "Name",
                TargetPropertyName = "Name",
                Mode = DesignDataBindingModes.Defualt,
                UpdateSourceTrigger = DesignUpdateSourceTrigger.Default
            };
        }
        [TestMethod]
        public void SourceOrTargetNull_ThorwException()
        {
            var bd1 = CreateBinding();
            bd1.Source = null;
            Assert.ThrowsException<InvalidOperationException>(() => bd1.Bind());
            var bd2 = CreateBinding();
            bd2.Target = null;
            Assert.ThrowsException<InvalidOperationException>(() => bd2.Bind());
        }
        [TestMethod]
        public void PropertyNameNull_ThrowException()
        {
            var bd1 = CreateBinding();
            bd1.SourcePropertyName = null;
            Assert.ThrowsException<InvalidOperationException>(() => bd1.Bind());
            var bd2 = CreateBinding();
            bd2.TargetPropertyName = null;
            Assert.ThrowsException<InvalidOperationException>(() => bd2.Bind());
        }
        [TestMethod]
        public void SoureEqualTarget_ThrowException()
        {
            var bd1 = CreateBinding();
            bd1.Source = bd1.Target;
            Assert.ThrowsException<InvalidOperationException>(() => bd1.Bind());
        }
        [TestMethod]
        public void NotFoundProperty_ThrowException()
        {
            var bd1 = CreateBinding();
            bd1.SourcePropertyName = "dsadsa";
            Assert.ThrowsException<InvalidOperationException>(() => bd1.Bind());
            var bd2 = CreateBinding();
            bd2.TargetPropertyName = "dasdsaw";
            Assert.ThrowsException<InvalidOperationException>(() => bd2.Bind());
        }
    }
}
