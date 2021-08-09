using Ao.ObjectDesign.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Test.Data
{
    [TestClass]
    public class DesignDataBindingTest
    {
        class NullDesignDataBinding : DesignDataBinding
        {
            public override void UpdateSource()
            {
            }

            public override void UpdateTarger()
            {
            }

            protected override void OnBind()
            {
            }

            protected override void OnUnBind()
            {
            }
        }
        [TestMethod]
        public void WhenBindSetProperty_MustThrow()
        {
            var bd = new NullDesignDataBinding();
            bd.Bind();
            Assert.ThrowsException<InvalidOperationException>(() => bd.Converter = null);
            Assert.ThrowsException<InvalidOperationException>(() => bd.ConverterParamter = null);
            Assert.ThrowsException<InvalidOperationException>(() => bd.Mode =  DesignDataBindingModes.Defualt);
            Assert.ThrowsException<InvalidOperationException>(() => bd.UpdateSourceTrigger= DesignUpdateSourceTrigger.Default);
            Assert.ThrowsException<InvalidOperationException>(() => bd.Target= null);
            Assert.ThrowsException<InvalidOperationException>(() => bd.Source = null);
            Assert.ThrowsException<InvalidOperationException>(() => bd.SourcePropertyName = null);
            Assert.ThrowsException<InvalidOperationException>(() => bd.TargetPropertyName = null);
        }

        [TestMethod]
        public void BindAndUnBind()
        {
            var bd = new NullDesignDataBinding();
            bd.Bind();
            Assert.IsTrue(bd.IsBind);
            bd.Bind();
            Assert.IsTrue(bd.IsBind);
            bd.UnBind();
            Assert.IsFalse(bd.IsBind);
            bd.UnBind();
            Assert.IsFalse(bd.IsBind);
        }
    }
}
