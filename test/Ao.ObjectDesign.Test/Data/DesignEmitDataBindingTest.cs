using Ao.ObjectDesign.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ao.ObjectDesign.Test.Data
{
    [TestClass]
    public class DesignEmitDataBindingTest
    {

        [TestMethod]
        public void OneWay_SourceChanged_TargetMustChanged()
        {
            var source = new Student();
            var target = new Student();
            var bd = new DesignEmitDataBinding
            {
                Source = source,
                Target = target,
                Mode = DesignDataBindingModes.OneWay,
                SourcePropertyName = "Name",
                TargetPropertyName = "Name"
            };
            bd.Bind();
            source.Name = "hello";
            Assert.AreEqual(source.Name, target.Name);
            bd.UnBind();
        }
        [TestMethod]
        public void TwoWay_BothChange_OtherMustChanged()
        {
            var source = new Student();
            var target = new Student();
            var bd = new DesignEmitDataBinding
            {
                Source = source,
                Target = target,
                Mode = DesignDataBindingModes.TwoWay,
                SourcePropertyName = "Name",
                TargetPropertyName = "Name"
            };
            bd.Bind();
            source.Name = "hello";
            Assert.AreEqual(source.Name, target.Name);
            target.Name = "ioioj";
            Assert.AreEqual(source.Name, target.Name);
            bd.UnBind();
        }
    }
}
