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
                return null;
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
        public void GivenNullInit_MustTagEmpty()
        {
            var val = new NullVarValue(null);
            Assert.AreEqual(TypeCode.Empty, val.TypeCode);
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

            Assert.AreEqual(v1.GetHashCode(), v2.GetHashCode());
            Assert.AreNotEqual(v1.GetHashCode(), v3.GetHashCode());
            Assert.AreNotEqual(v1.GetHashCode(), v4.GetHashCode());

            Assert.AreEqual(v1.ToString(), v2.ToString());
        }
    }
}
