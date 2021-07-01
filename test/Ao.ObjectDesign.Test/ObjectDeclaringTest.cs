using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class ObjectDeclaringTest
    {
        [TestMethod]
        public void GivenNullInitOrCall_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ObjectDeclaring(null));
        }
        [TestMethod]
        public void GivenTypeInit_TypeMustEqualInput()
        {
            var t = GetType();
            var delcar = new ObjectDeclaring(t);
            Assert.AreEqual(t, delcar.Type);
        }
        struct MyStruct
        {
            public int A { get; set; }
        }
        [TestMethod]
        [DataRow(typeof(int))]
        [DataRow(typeof(MyStruct))]
        [DataRow(typeof(double))]
        [DataRow(typeof(long?))]
        public void GivenNoClass_MustGotNohtingPropertyDeclare(Type type)
        {
            var c = new ObjectDeclaring(type);
            Assert.IsFalse(c.GetPropertyDeclares().Any());
        }
        
        [TestMethod]
        public void GivenClass_GetPropertyProxy()
        {
            var c = new ObjectDeclaring(typeof(Student));
            var q = c.GetPropertyDeclares().ToArray();
            Assert.AreEqual(2, q.Length);
            Assert.IsTrue(q.Any(x => x.PropertyInfo.Name == nameof(Student.Name)));
            Assert.IsTrue(q.Any(x => x.PropertyInfo.Name == nameof(Student.Age)));
            Assert.IsTrue(q.All(x => x.PropertyInfo.Name != nameof(Student.Address)));
        }
        [TestMethod]
        public void GivenNullCallAsProperties_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new NullObjectDeclaring().Throw());
        }
        class NullObjectDeclaring:ObjectDeclaring
        {
            public void Throw()
            {
                AsProperties((Func<PropertyInfo,int>)null).ToArray();
            }
        }
    }
}
