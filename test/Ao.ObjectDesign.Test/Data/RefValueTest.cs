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
    public class RefValueTest
    {
        [TestMethod]
        public void Clone()
        {
            var obj = new object();
            var val1 = new RefValue(obj, TypeCode.Object);
            var val2 = val1.Clone();

            Assert.IsInstanceOfType(val2, typeof(RefValue));
            Assert.AreEqual(val1.Value, val2.Value);
            Assert.AreEqual(val1.TypeCode, val2.TypeCode);
        }
    }
}
