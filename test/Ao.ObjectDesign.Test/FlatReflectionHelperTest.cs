using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class FlatReflectionHelperTest
    {
        class A
        {
            public int Ax { get; set; }

            public string Bx { get; set; }

            public long Error { get; set; }
        }
        class B
        {
            public int Ax { get; set; }

            public string Bx { get; set; }

            public object Error { get; set; }
        }
        [TestMethod]
        public void FlatClone()
        {
            var a = new A { Ax = 1, Bx = "22", Error = 33 };
            var b = new B();
            var res = FlatReflectionHelper.SpecularMapping(a, b);
            Assert.AreEqual(a.Ax, b.Ax);
            Assert.AreEqual(a.Bx, b.Bx);
            Assert.IsNull(b.Error);
            Assert.AreEqual(3,res.Count );
        }
    }
}
