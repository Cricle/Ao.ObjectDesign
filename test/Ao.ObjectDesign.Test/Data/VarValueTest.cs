using Ao.ObjectDesign.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Test.Data
{
    [TestClass]
    public class VarValueTest
    {
        class NullVarValue : VarValue
        {
            public NullVarValue(object value) : base(value)
            {
            }

            public NullVarValue(object value, TypeCode typeCode) : base(value, typeCode)
            {
            }

            public override VarValue Clone()
            {
                return new NullVarValue(Value,TypeCode);
            }
        }
        [TestMethod]
        public void GivenStructInit_MustTagToStructActualType()
        {
            var i = 1;
            var val = new NullVarValue(i);
            Assert.AreEqual(i, val.Value);
            Assert.AreEqual(TypeCode.Int32, val.TypeCode);
            Assert.IsNotNull(val.Convertible);

            val = new NullVarValue(i, TypeCode.Int64);
            Assert.AreEqual(i, val.Value);
            Assert.AreEqual(TypeCode.Int64, val.TypeCode);
            Assert.IsNotNull(val.Convertible);
        }
        [TestMethod]
        public void Clone()
        {
            var val = new NullVarValue(null);
            var val2 = val.Clone();

            Assert.IsNotNull(val2);
        }
        [TestMethod]
        public void GivenNullInit_MustTagEmpty()
        {
            var val = new NullVarValue(null);
            Assert.AreEqual(TypeCode.Empty, val.TypeCode);
        }
        [TestMethod]
        public void ToObject_NullValue()
        {
            var val1 = new RefValue(null, TypeCode.Empty);
            Assert.IsNull(val1.To(typeof(string)));

            var val2 = new RefValue("hello", TypeCode.String);
            Assert.IsNull(val1.To(typeof(void)));
        }

        [TestMethod]
        [DataRow(typeof(int), 1, 1)]
        [DataRow(typeof(long), 1L, 1L)]
        [DataRow(typeof(double), 1d, 1d)]
        [DataRow(typeof(float), 1f, 1f)]
        [DataRow(typeof(long), 1, 1L)]
        [DataRow(typeof(double), 1f, 1d)]
        public void ToPrimitive(Type type, ValueType val, object dest)
        {
            var v = new StructValue(val);

            var res = v.To(type);

            Assert.AreEqual(dest, res);
        }
        [TestMethod]
        [DataRow(typeof(int?), "a")]
        [DataRow(typeof(long?), "a")]
        [DataRow(typeof(double?), "a")]
        [DataRow(typeof(float?), "a")]
        [DataRow(typeof(decimal?), "a")]
        public void ToNullable_Fail(Type type,string input)
        {
            var obj = new RefValue(input, TypeCode.String);
            var val = obj.To(type);
            Assert.IsNull(val);
        }
        [TestMethod]
        public void ToInvalid()
        {
            var obj = new RefValue(1);
            var res = obj.TryTo(typeof(object), out var ex, out _);
            Assert.IsInstanceOfType(ex, typeof(InvalidCastException));
        }
        [TestMethod]
        public void GivenSameOrNot_EqualsOrNotEquals()
        {
            var v1 = new NullVarValue(1);
            var v2 = new NullVarValue(1, TypeCode.Int32);
            var v3 = new NullVarValue(1f);
            var v4 = new NullVarValue(null);

            Assert.IsTrue(v1.Equals(v2));
            Assert.IsFalse(v1.Equals(v3));
            Assert.IsFalse(v1.Equals(v4));
            Assert.IsFalse(v1.Equals((object)null));
            Assert.IsFalse(v1.Equals((VarValue)null));

            Assert.IsTrue(v1 == v2);
            Assert.IsTrue((VarValue)null == (VarValue)null);
            Assert.IsTrue(v1 != v3);
            Assert.IsTrue(v1 != null);
            Assert.IsTrue(null != v1);

            Assert.IsFalse(v1 == null);
            Assert.IsFalse(null == v1);
            Assert.IsFalse(v1 != v2);
            Assert.IsFalse((VarValue)null != (VarValue)null);

            Assert.AreEqual(v1.GetHashCode(), v2.GetHashCode());
            Assert.AreNotEqual(v1.GetHashCode(), v3.GetHashCode());
            Assert.AreNotEqual(v1.GetHashCode(), v4.GetHashCode());

            Assert.AreEqual(v1.ToString(), v2.ToString());
        }
        [TestMethod]
        public void FromNull()
        {
            var val = VarValue.FromObject(null);
            Assert.IsNull(val.Value);
            Assert.AreEqual(TypeCode.Empty, val.TypeCode);
        }
        [TestMethod]
        public void FromVarValue()
        {
            var val = new StructValue(1);
            var res = VarValue.FromObject(val);
            Assert.AreEqual(val, res);
        }
        [TestMethod]
        public void FromDateTime()
        {
            var dt = DateTime.MinValue;
            var res = VarValue.FromObject(dt);
            Assert.AreEqual(dt, res.Value);
            Assert.AreEqual(TypeCode.DateTime, res.TypeCode);
        }
        [TestMethod]
        public void FromDBNull()
        {
            var dt = DBNull.Value;
            var res = VarValue.FromObject(dt);
            Assert.AreEqual(dt, res.Value);
            Assert.AreEqual(TypeCode.DBNull, res.TypeCode);
        }
        [TestMethod]
        public void FromObject()
        {
            var dt = new object();
            var res = VarValue.FromObject(dt);
            Assert.AreEqual(dt, res.Value);
            Assert.AreEqual(TypeCode.Object, res.TypeCode);
        }
        [TestMethod]
        [DataRow(1)]
        [DataRow(1d)]
        [DataRow(1f)]
        [DataRow(1L)]
        [DataRow((char)1)]
        [DataRow(1u)]
        [DataRow(1ul)]
        public void FromPrimitive(object val)
        {
            var vt = (IConvertible)val;
            var v = VarValue.FromObject(val);
            Assert.AreEqual(vt.GetTypeCode(), v.TypeCode);
            Assert.AreEqual(val, v.Value);
        }
    }
}
