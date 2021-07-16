using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class PropertyProxyExtensionsTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => PropertyProxyExtensions.CreateVisitor(null));
        }
        [TestMethod]
        public void CreateVisitor_MustGotVisitor()
        {
            Student inst = new Student();
            System.Reflection.PropertyInfo prop = inst.GetType().GetProperties()[0];
            PropertyProxy propProxy = new PropertyProxy(inst, prop);
            PropertyVisitor visitor = PropertyProxyExtensions.CreateVisitor(propProxy);
            Assert.IsNotNull(visitor);
        }
    }
}
