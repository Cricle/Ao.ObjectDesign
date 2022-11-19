using Ao.ObjectDesign.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class BindingToTypeCreatorBaseTest
    {
        class NullBindingToTypeCreatorBase : BindingToTypeCreatorBase
        {
            public NullBindingToTypeCreatorBase(IEnumerable<Type> supportCreateBindTypes) : base(supportCreateBindTypes)
            {
            }

            public NullBindingToTypeCreatorBase(IReadOnlyHashSet<Type> supportCreateBindTypes) : base(supportCreateBindTypes)
            {
            }

            public override IEnumerable<BindingUnit> CreateBindings(Type type)
            {
                return null;
            }
        }
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            IEnumerable<Type> supports = new Type[] { typeof(object) };

            Assert.ThrowsException<ArgumentNullException>(() => new NullBindingToTypeCreatorBase(null));
            Assert.ThrowsException<ArgumentNullException>(() => new NullBindingToTypeCreatorBase(supports).IsSupportCreateBind(null));
        }
        [TestMethod]
        public void IsSupports()
        {
            IEnumerable<Type> supports = new Type[] { typeof(object) };

            var val = new NullBindingToTypeCreatorBase(supports);

            Assert.AreEqual(1, val.SupportCreateBindTypes.Count);
            Assert.AreEqual(typeof(object), val.SupportCreateBindTypes.Single());

            IReadOnlyHashSet<Type> set = new ReadOnlyHashSet<Type>(new Type[] { typeof(object) });
            val = new NullBindingToTypeCreatorBase(set);
            Assert.AreEqual(1, val.SupportCreateBindTypes.Count);
            Assert.AreEqual(set, val.SupportCreateBindTypes);

            Assert.IsFalse(val.IsSupportCreateBind(typeof(int)));
            Assert.IsTrue(val.IsSupportCreateBind(typeof(object)));
        }
    }
}
