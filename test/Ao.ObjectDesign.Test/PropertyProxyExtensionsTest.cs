using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var inst = new Student();
            var prop = inst.GetType().GetProperties()[0];
            var propProxy = new PropertyProxy(inst,prop);
            var visitor = PropertyProxyExtensions.CreateVisitor(propProxy);
            Assert.IsNotNull(visitor);
        }
    }
}
