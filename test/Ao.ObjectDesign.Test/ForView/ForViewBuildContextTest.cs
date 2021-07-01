using Ao.ObjectDesign.ForView;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

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
            var inst = new Student();
            var prop = inst.GetType().GetProperties()[0];
            var propProxy = new PropertyProxy(inst, prop);
            var ctx = new ForViewBuildContext(propProxy);
            Assert.AreEqual(propProxy, ctx.PropertyProxy);
        }
    }
}
