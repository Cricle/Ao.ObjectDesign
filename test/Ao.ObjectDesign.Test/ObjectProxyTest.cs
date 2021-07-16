using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class ObjectProxyTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            object inst = new object();
            Type type = inst.GetType();
            Assert.ThrowsException<ArgumentNullException>(() => new ObjectProxy(null, type));
        }
        [TestMethod]
        public void GivenInstanceNotEqualType_MustThrowException()
        {
            int inst = 1;
            Type type = typeof(Student);

            Assert.ThrowsException<ArgumentException>(() => new ObjectProxy(inst, type));
        }
        [TestMethod]
        public void CreatePropertiesProxy()
        {
            Student stu = new Student();
            Type type = stu.GetType();

            ObjectProxy proxy = new ObjectProxy(stu, type);
            PropertyProxy[] props = proxy.GetPropertyProxies().ToArray();

            Assert.AreEqual(2, props.Length);
            Assert.IsTrue(props.Any(x => x.PropertyInfo.Name == nameof(Student.Name)));
            Assert.IsTrue(props.Any(x => x.PropertyInfo.Name == nameof(Student.Age)));
            Assert.AreEqual(stu, props[0].DeclaringInstance);

            IPropertyProxy[] pprops = ((IObjectProxy)proxy).GetPropertyProxies().ToArray();

            Assert.AreEqual(2, pprops.Length);
            Assert.IsTrue(pprops.Any(x => x.PropertyInfo.Name == nameof(Student.Name)));
            Assert.IsTrue(pprops.Any(x => x.PropertyInfo.Name == nameof(Student.Age)));
            Assert.AreEqual(stu, pprops[0].DeclaringInstance);
        }
    }
}
