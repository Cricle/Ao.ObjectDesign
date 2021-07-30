using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;

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
            Type t = GetType();
            ObjectDeclaring declcar = new ObjectDeclaring(t);
            Assert.AreEqual(t, declcar.Type);
            Assert.IsTrue(declcar.ToString().Contains(declcar.Type.FullName));
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
            ObjectDeclaring c = new ObjectDeclaring(type);
            Assert.IsFalse(c.GetPropertyDeclares().Any());
        }

        [TestMethod]
        public void GivenClass_GetPropertyProxy()
        {
            ObjectDeclaring c = new ObjectDeclaring(typeof(Student));
            IPropertyDeclare[] q = c.GetPropertyDeclares().ToArray();
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
        class NullObjectDeclaring : ObjectDeclaring
        {
            public void Throw()
            {
                AsProperties((Func<PropertyInfo, int>)null).ToArray();
            }
        }
    }
}
