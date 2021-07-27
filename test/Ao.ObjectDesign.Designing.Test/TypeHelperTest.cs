using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing.Test
{
    [TestClass]
    public class TypeHelperTest
    {
        struct MyStruct
        {

        }
        [TestMethod]
        public void GivenNull_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => TypeHelper.ChangeType(1, null));
            Assert.ThrowsException<ArgumentNullException>(() => TypeHelper.IsBaseType(null));
            Assert.ThrowsException<ArgumentNullException>(() => TypeHelper.IsNullable(null));
            Assert.ThrowsException<ArgumentNullException>(() => TypeHelper.SafeChangeType(1, null));
            Assert.ThrowsException<ArgumentNullException>(() => TypeHelper.TryChangeType(1, null, out _, out _));
        }
        [TestMethod]
        [DataRow(typeof(int?))]
        [DataRow(typeof(long?))]
        [DataRow(typeof(double?))]
        [DataRow(typeof(float?))]
        [DataRow(typeof(MyStruct?))]
        public void GivenNullableType_MustReturnTrue(Type type)
        {
            Assert.IsTrue(TypeHelper.IsNullable(type));
        }
        [TestMethod]
        [DataRow(typeof(object))]
        [DataRow(typeof(int))]
        [DataRow(typeof(long))]
        [DataRow(typeof(double))]
        [DataRow(typeof(float))]
        [DataRow(typeof(MyStruct))]
        public void GivenNoNullableType_MustReturnFalse(Type type)
        {
            Assert.IsFalse(TypeHelper.IsNullable(type));
        }
        [TestMethod]
        [DataRow(typeof(int))]
        [DataRow(typeof(double))]
        [DataRow(typeof(object))]
        [DataRow(typeof(MyStruct))]
        public void GivenNullChange_MustReturnNull(Type type)
        {
            var val = TypeHelper.SafeChangeType(null, type);
            Assert.IsNull(val);
        }
        [TestMethod]
        [DataRow((byte)1, typeof(int), 1)]
        [DataRow((byte)1, typeof(double), 1d)]
        [DataRow((int)1, typeof(double), 1d)]
        [DataRow(1L, typeof(int), 1)]
        public void GivenValueConvert_MustPass(object origin,Type type,object dest)
        {
            var val = TypeHelper.SafeChangeType(origin, type);
            Assert.AreEqual(dest,val);
        }
        [TestMethod]
        [DataRow((byte)1, typeof(int?), 1)]
        [DataRow(null, typeof(int?), null)]
        [DataRow(null, typeof(double?), null)]
        [DataRow(11, typeof(double?), 11d)]
        public void GivenNullableValueConvert_MustPass(object origin, Type type, object dest)
        {
            var val = TypeHelper.SafeChangeType(origin, type);
            Assert.AreEqual(dest, val);
        }
        [TestMethod]
        [DataRow(typeof(int))]
        [DataRow(typeof(int?))]
        [DataRow(typeof(double))]
        [DataRow(typeof(double?))]
        public void GivenCantConvert_MustThrow(Type type)
        {
            var obj = new object();
            Assert.ThrowsException<InvalidCastException>(() => TypeHelper.ChangeType(obj, type));
        }
    }
}
