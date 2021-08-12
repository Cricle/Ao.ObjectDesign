using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.WpfDesign.Test
{
    [TestClass]
    public class InstanceFactoryTest
    {
        class NullInstanceFactory : InstanceFactory
        {
            public NullInstanceFactory(Type targetType) : base(targetType)
            {
            }

            public override object Create()
            {
                return null;
            }
        }
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new NullInstanceFactory(null));
        }
        [TestMethod]
        public void EqualsAndHashCode()
        {
            var f1 = new NullInstanceFactory(typeof(object));
            var f2 = new NullInstanceFactory(typeof(object));
            var f3 = new NullInstanceFactory(typeof(int));
            var f4 = new NullInstanceFactory(typeof(double));

            Assert.IsTrue(f1.Equals(f2));
            Assert.IsFalse(f1.Equals(f3));
            Assert.IsFalse(f1.Equals(f4));
            Assert.IsFalse(f1.Equals(null));

            Assert.AreEqual(f1.GetHashCode(), f2.GetHashCode());
            Assert.AreNotEqual(f1.GetHashCode(), f3.GetHashCode());
            Assert.AreNotEqual(f1.GetHashCode(), f4.GetHashCode());
        }
    }
}
