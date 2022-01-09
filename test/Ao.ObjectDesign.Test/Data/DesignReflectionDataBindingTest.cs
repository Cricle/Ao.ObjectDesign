using Ao.ObjectDesign.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ao.ObjectDesign.Test.Data
{
    [TestClass]
    public class DesignReflectionDataBindingTest
    {
        private DesignReflectionDataBinding CreateBinding()
        {
            return new DesignReflectionDataBinding
            {
                Source = new Student(),
                Target = new Student(),
                SourcePropertyName = "Name",
                TargetPropertyName = "Name"
            };
        }
        [TestMethod]
        [DataRow(DesignDataBindingModes.Defualt)]
        [DataRow(DesignDataBindingModes.OneWay)]
        public void OneWayOrDefaultBind(DesignDataBindingModes mode)
        {
            var bd = CreateBinding();
            bd.Mode = mode;
            var source = (Student)bd.Source;
            var target = (Student)bd.Target;

            bd.Bind();
            source.Name = "dsagdkisab";
            Assert.AreEqual(source.Name, target.Name);
            bd.UnBind();
            source.Name = "yuyuyu";
            Assert.AreNotEqual(source.Name, target.Name);
        }
        [TestMethod]
        public void TwoWayBind()
        {
            var bd = CreateBinding();
            bd.Mode = DesignDataBindingModes.TwoWay;
            var source = (Student)bd.Source;
            var target = (Student)bd.Target;

            bd.Bind();
            source.Name = "dsagdkisab";
            Assert.AreEqual(source.Name, target.Name);
            target.Name = "oopopopopo";
            Assert.AreEqual(source.Name, target.Name);

            bd.UnBind();
            source.Name = "yuyuyu";
            Assert.AreNotEqual(source.Name, target.Name);
        }
        [TestMethod]
        public void OneTimeBind()
        {
            var bd = CreateBinding();
            bd.Mode = DesignDataBindingModes.OneTime;
            var source = (Student)bd.Source;
            var target = (Student)bd.Target;

            source.Name = "dhqiuwgdiu";

            bd.Bind();
            Assert.AreEqual(source.Name, target.Name);
            target.Name = "oopopopopo";
            Assert.AreNotEqual(source.Name, target.Name);

            bd.UnBind();
        }
    }
}
