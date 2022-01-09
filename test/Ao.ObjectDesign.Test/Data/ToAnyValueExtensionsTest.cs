using Ao.ObjectDesign.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ao.ObjectDesign.Test.Data
{
    [TestClass]
    public class ToAnyValueExtensionsTest
    {
        [TestMethod]
        public void GivenNullCall_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ToAnyValueExtensions.ToAny(null));
        }
        [TestMethod]
        public void ToAny_MustReturnCopy()
        {
            var val = VarValue.Long0Value;
            var res = ToAnyValueExtensions.ToAny(val);
            Assert.AreEqual(val.Value, res.Value);
            Assert.AreEqual(val.TypeCode, res.TypeCode);
        }
    }
}
