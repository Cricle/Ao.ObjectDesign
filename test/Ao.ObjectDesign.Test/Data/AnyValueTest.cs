using Ao.ObjectDesign.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ao.ObjectDesign.Test.Data
{
    [TestClass]
    public class AnyValueTest
    {
        public void New_PropertyValueMustEqualsInputs()
        {
            object o = 123;
            TypeCode t = TypeCode.DateTime;
            var val = new AnyValue(o, t);
            Assert.AreEqual(o, val.Value);
            Assert.AreEqual(t, val.TypeCode);
        }
        public void Clone_ResultMustAllSame()
        {
            object o = 123;
            TypeCode t = TypeCode.DateTime;
            var val = new AnyValue(o, t);
            var next = val.Clone();
            Assert.AreEqual(val.Value, next.Value);
            Assert.AreEqual(val.TypeCode, next.TypeCode);

            next = (AnyValue)((ICloneable)val).Clone();
            Assert.AreEqual(val.Value, next.Value);
            Assert.AreEqual(val.TypeCode, next.TypeCode);
        }
        public void Equals_HashCodeAndToString()
        {
            object o = 123;
            object o2 = 456;
            TypeCode t = TypeCode.DateTime;
            TypeCode t1 = TypeCode.Int16;
            var val1 = new AnyValue(o, t);
            var val2 = new AnyValue(o2, t);
            var val3 = new AnyValue(o, t1);
            var val4 = new AnyValue(o2, t1);
            var val5 = new AnyValue(o2, t1);

            Assert.IsFalse(val1.Equals(val2));
            Assert.IsFalse(val1.Equals(val3));
            Assert.IsFalse(val1.Equals(val4));
            Assert.IsFalse(val1.Equals(val5));
            Assert.IsTrue(val4.Equals(val5));

            Assert.IsFalse(val1 == val2);
            Assert.IsFalse(val1 == val3);
            Assert.IsFalse(val1 == val4);
            Assert.IsFalse(val1 == val5);
            Assert.IsTrue(val4 == val5);

            Assert.IsTrue(val1 != val2);
            Assert.IsTrue(val1 != val3);
            Assert.IsTrue(val1 != val4);
            Assert.IsTrue(val1 != val5);
            Assert.IsFalse(val4 != val5);

            Assert.AreNotEqual(val1.GetHashCode(), val2.GetHashCode());
            Assert.AreNotEqual(val1.GetHashCode(), val3.GetHashCode());
            Assert.AreNotEqual(val1.GetHashCode(), val4.GetHashCode());
            Assert.AreNotEqual(val1.GetHashCode(), val5.GetHashCode());
            Assert.AreEqual(val4.GetHashCode(), val5.GetHashCode());

            Assert.AreNotEqual(val1.ToString(), val2.ToString());
            Assert.AreNotEqual(val1.ToString(), val3.ToString());
            Assert.AreNotEqual(val1.ToString(), val4.ToString());
            Assert.AreNotEqual(val1.ToString(), val5.ToString());
            Assert.AreEqual(val4.ToString(), val5.ToString());

        }
    }
}
