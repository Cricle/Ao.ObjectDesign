using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class ObjectProxyTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            var inst = new object();
            var type = inst.GetType();
            Assert.ThrowsException<ArgumentNullException>(() => new ObjectProxy(null, type));
        }
        [TestMethod]
        public void GivenInstanceNotEqualType_MustThrowException()
        {
            var inst = 1;
            var type = typeof(Student);

            Assert.ThrowsException<ArgumentException>(() => new ObjectProxy(inst, type));
        }
        [TestMethod]
        public void CreatePropertiesProxy()
        {
            var stu = new Student();
            var type = stu.GetType();

            var proxy = new ObjectProxy(stu, type);
            var props = proxy.GetPropertyProxies().ToArray();

            Assert.AreEqual(2, props.Length);
            Assert.IsTrue(props.Any(x => x.PropertyInfo.Name == nameof(Student.Name)));
            Assert.IsTrue(props.Any(x => x.PropertyInfo.Name == nameof(Student.Age)));
            Assert.AreEqual(stu, props[0].DeclaringInstance);

            var pprops =((IObjectProxy)proxy).GetPropertyProxies().ToArray();

            Assert.AreEqual(2, pprops.Length);
            Assert.IsTrue(pprops.Any(x => x.PropertyInfo.Name == nameof(Student.Name)));
            Assert.IsTrue(pprops.Any(x => x.PropertyInfo.Name == nameof(Student.Age)));
            Assert.AreEqual(stu,pprops[0].DeclaringInstance);
        }
    }
}
