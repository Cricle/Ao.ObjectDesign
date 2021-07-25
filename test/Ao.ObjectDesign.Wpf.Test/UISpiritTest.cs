using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf.Test
{
    [TestClass]
    public class UISpiritTest
    {
        class A
        {

        }
        class B
        {

        }
        [TestMethod]
        public void Init_PropertyMustEqualInput()
        {
            var a = new A();
            var b = new B();
            var spirit = new UISpirit<A, B>(a,b);
            Assert.AreEqual(a, spirit.View);
            Assert.AreEqual(b, spirit.Context);

            spirit = new UISpirit<A, B>(b);
            Assert.IsNull(spirit.View);
            Assert.AreEqual(b, spirit.Context);
        }
        [TestMethod]
        public void EqualsAndHashCode()
        {
            var a = new A();
            var b = new B();
            var s1 = new UISpirit<A, B>(a, b);
            var s2 = new UISpirit<A, B>(a, b);
            var s3 = new UISpirit<A, B>(b);
            var s4 = new UISpirit<A, B>(b);

            Assert.IsTrue(s1.Equals(s2));
            Assert.IsFalse(s1.Equals(s3));
            Assert.IsFalse(s1.Equals(null));
            Assert.IsTrue(s3.Equals(s4));

            Assert.AreEqual(s1.GetHashCode(), s2.GetHashCode());
            Assert.AreEqual(s3.GetHashCode(), s4.GetHashCode());

            Assert.AreEqual(s1.ToString(), s2.ToString());
            Assert.AreEqual(s3.ToString(), s4.ToString());
        }
    }
}
