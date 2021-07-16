using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class ObjectDesignerTest
    {
        [TestMethod]
        public void GivenNullProxy_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ObjectDesigner.Instance.CreateProxy(null, typeof(object)));
            Assert.ThrowsException<ArgumentNullException>(() => ObjectDesigner.Instance.CreateProxy(new object(), null));
        }
        [TestMethod]
        public void GivenNotClassProxy_MustThrowException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                ObjectDesigner.Instance.CreateProxy(1, typeof(int));
            });
        }
        [TestMethod]
        public void GivenClassProxy_MustProxied()
        {
            IObjectProxy proxy = ObjectDesigner.Instance.CreateProxy(new Student(), typeof(Student));
            Assert.IsNotNull(proxy);

            proxy = ObjectDesigner.CreateDefaultProxy(new Student(), typeof(Student));
            Assert.IsNotNull(proxy);
        }
    }
}
