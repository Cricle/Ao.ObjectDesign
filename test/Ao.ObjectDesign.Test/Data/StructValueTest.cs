using Ao.ObjectDesign.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ao.ObjectDesign.Test.Data
{
    [TestClass]
    public class StructValueTest
    {
        [TestMethod]
        public void Clone()
        {
            var v = new StructValue(123);
            var copied = v.Clone();

            Assert.AreEqual(v.Value, copied.Value);

            Assert.IsInstanceOfType(copied, typeof(StructValue));
        }
        [TestMethod]
        public void New()
        {
            var v = new StructValue(123, TypeCode.Byte);

            Assert.AreEqual(TypeCode.Byte, v.TypeCode);
        }
    }
}
