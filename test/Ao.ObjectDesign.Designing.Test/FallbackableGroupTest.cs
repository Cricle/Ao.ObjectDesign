using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing.Test
{
    [TestClass]
    public class FallbackableGroupTest
    {
        [TestMethod]
        public void Init()
        {
            var group = new FallbackableGroup();
            Assert.AreEqual(FallbackModes.Forward, group.Mode);

            group = new FallbackableGroup(FallbackModes.Forward);
            Assert.AreEqual(FallbackModes.Forward, group.Mode);

            group = new FallbackableGroup(FallbackModes.Forward,10);
            Assert.AreEqual(FallbackModes.Forward, group.Mode);
            Assert.AreEqual(10, group.Capacity);

            var val = new NullFallbackable();

            group = new FallbackableGroup(FallbackModes.Forward, new IFallbackable[] { val});
            Assert.AreEqual(FallbackModes.Forward, group.Mode);
            Assert.AreEqual(1, group.Count);
        }
        [TestMethod]
        public void Copy()
        {
            var fallbacks = new IFallbackable[]
            {
                new NullFallbackable(),
                new NullFallbackable(),
                new NullFallbackable(),
                new NullFallbackable(),
            };

            var groups = new FallbackableGroup(FallbackModes.Reverse, fallbacks);

            var groups1 = (FallbackableGroup)groups.Copy(null);

            Assert.AreEqual(groups.Mode, groups1.Mode);

            Assert.AreEqual(groups.Count, groups1.Count);

            for (int i = 0; i < groups.Count; i++)
            {
                Assert.AreNotEqual(groups[i], groups1[i], i.ToString());
            }
        }
        [TestMethod]
        public void Fallback()
        {
            var fallbacks = new IFallbackable[]
            {
                new ValueFallback(),
                new ValueFallback(),
                new ValueFallback(),
                new ValueFallback(),
            };

            var groups = new FallbackableGroup(FallbackModes.Reverse, fallbacks);

            groups.Fallback();

            for (int i = 0; i < fallbacks.Length; i++)
            {
                Assert.IsTrue(((ValueFallback)fallbacks[i]).CallFallback,i.ToString());
            }
        }
        [TestMethod]
        public void IsReverse()
        {
            var fallbacks = new IFallbackable[]
            {
                new ValueFallback(),
                new ValueFallback(),
                new ValueFallback(),
                new ValueFallback(),
            };

            var groups = new FallbackableGroup(FallbackModes.Reverse, fallbacks);

            Assert.IsFalse(groups.IsReverse(null));

            var groups1 = new FallbackableGroup(FallbackModes.Reverse, fallbacks);

            Assert.IsFalse(groups.IsReverse(groups1));

            var groups2 = new FallbackableGroup(FallbackModes.Reverse, fallbacks.Skip(1));

            Assert.IsFalse(groups.IsReverse(groups2));

            var groups3 = new FallbackableGroup(FallbackModes.Forward, fallbacks);

            Assert.IsFalse(groups.IsReverse(groups3));

            var revfallbacks = new IFallbackable[]
            {
                new ValueFallback{ Mode = FallbackModes.Reverse},
                new ValueFallback{ Mode = FallbackModes.Reverse},
                new ValueFallback{ Mode = FallbackModes.Reverse},
                new ValueFallback{ Mode = FallbackModes.Reverse},
            };
            var groups4 = new FallbackableGroup(FallbackModes.Forward, revfallbacks);

            Assert.IsTrue(groups.IsReverse(groups4));
        }
        [TestMethod]
        public void ReverseList()
        {
            var fallbacks = new IFallbackable[]
            {
                new ValueFallback(),
                new ValueFallback(),
                new ValueFallback(),
                new ValueFallback(),
            };

            var groups = new FallbackableGroup(FallbackModes.Reverse, fallbacks);

            groups.ReverseList();

            Assert.AreEqual(fallbacks[0], groups[3]);
            Assert.AreEqual(fallbacks[1], groups[2]);
        }
        [TestMethod]
        public void Reverse()
        {
            var fallbacks = new IFallbackable[]
            {
                new ValueFallback(),
                new ValueFallback(),
                new ValueFallback(),
                new ValueFallback(),
            };

            var groups = new FallbackableGroup(FallbackModes.Forward, fallbacks);

            var revGroup=groups.Reverse();

            Assert.AreEqual(FallbackModes.Reverse, revGroup.Mode);

            for (int i = 0; i < revGroup.Count; i++)
            {
                var f = (ValueFallback)revGroup[i];
                Assert.AreEqual(FallbackModes.Reverse, f.Mode);
            }

            var val = ((IFallbackable)groups).Reverse();

            Assert.IsInstanceOfType(val, typeof(FallbackableGroup));
        }
    }
}
