using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class DelegateInstanceFactoryTest
    {
        [TestMethod]
        public void GivenNullDelegate_MustThrowException()
        {
            var type = typeof(object);
            Assert.ThrowsException<ArgumentNullException>(() => new DelegateInstanceFactory(type, null));
        }
        [TestMethod]
        public void Create()
        {
            var obj = new object();
            var factory = new DelegateInstanceFactory(typeof(object), () => obj);
            var inst = factory.Create();
            Assert.AreEqual(obj, inst);
        }
    }
}
