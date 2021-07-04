using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class PropertyProxyTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new PropertyProxy(null, typeof(Student).GetProperties()[0]));
        }
        [TestMethod]
        public void GivenNotAssignableFromType_MustThrowException()
        {
            var inst = 1;
            var propInfo = typeof(Student).GetProperties()[0];
            Assert.ThrowsException<ArgumentException>(() => new PropertyProxy(inst, propInfo));
        }
        [TestMethod]
        public void GivenValueInit_PropertyValueMustEqualInput()
        {
            var inst = new Student();
            var propInfo = typeof(Student).GetProperty(nameof(Student.Name));
            inst.Name = "hello";
            var proxy = new PropertyProxy(inst, propInfo);
            Assert.AreEqual(inst, proxy.DeclaringInstance);
            Assert.AreEqual(inst.Name, proxy.Instance);
        }
        class InnerProxy
        {
            public int Age { get; set; }

            public InnerProxy Next { get; set; }
        }
        [TestMethod]
        public void GetPropertyProxy_MustGotThem()
        {
            var inst = new InnerProxy { Next=new InnerProxy { Next=new InnerProxy()} };
            var propInfo = typeof(InnerProxy).GetProperty(nameof(InnerProxy.Next));
            var proxy = new PropertyProxy(inst, propInfo);
            var nexts = proxy.GetPropertyProxies().ToArray();
            Assert.AreEqual(2, nexts.Length);
            Assert.IsTrue(nexts.Any(x => x.PropertyInfo.Name == nameof(InnerProxy.Age)));
            Assert.IsTrue(nexts.Any(x => x.PropertyInfo.Name == nameof(InnerProxy.Next)));
            Assert.AreEqual(nexts.First(x=>x.PropertyInfo.Name==nameof(InnerProxy.Next)).Instance, inst.Next.Next);

            var onexts = ((IObjectProxy)proxy).GetPropertyProxies().ToArray();
            Assert.AreEqual(2, onexts.Length);
            Assert.IsTrue(onexts.Any(x => x.PropertyInfo.Name == nameof(InnerProxy.Age)));
            Assert.IsTrue(onexts.Any(x => x.PropertyInfo.Name == nameof(InnerProxy.Next)));
        }
    }
}
