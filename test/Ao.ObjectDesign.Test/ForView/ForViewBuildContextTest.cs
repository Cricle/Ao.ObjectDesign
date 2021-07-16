using Ao.ObjectDesign.ForView;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ao.ObjectDesign.Test.ForView
{
    [TestClass]
    public class ForViewBuildContextTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ForViewBuildContext(null));
        }
        [TestMethod]
        public void GivenValueInit_PropertyMustEqualInput()
        {
            Student inst = new Student();
            System.Reflection.PropertyInfo prop = inst.GetType().GetProperties()[0];
            PropertyProxy propProxy = new PropertyProxy(inst, prop);
            ForViewBuildContext ctx = new ForViewBuildContext(propProxy);
            Assert.AreEqual(propProxy, ctx.PropertyProxy);
        }
    }
}
