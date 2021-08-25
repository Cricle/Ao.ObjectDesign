using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing.Test
{
    [TestClass]
    public class FallbackCommandWaysTest
    {
        class ValueFallBack : IFallbackable
        {
            public FallbackModes Mode { get; set; }

            public IFallbackable Copy(FallbackModes? mode)
            {
                return null;
            }
            public bool IsFallback { get; set; }

            public void Fallback()
            {
                IsFallback = true;
            }
            public bool IsDoReverse { get; set; }
            public IFallbackable Reverse()
            {
                IsDoReverse = true;
                return new ValueFallBack { Mode = Mode == FallbackModes.Forward ? FallbackModes.Reverse : FallbackModes.Forward };
            }

            public bool IsReverse(IFallbackable fallbackable)
            {
                return false;
            }
        }
        class NullFallback : IFallbackable
        {
            public FallbackModes Mode { get; set; }

            public IFallbackable Copy(FallbackModes? mode)
            {
                return null;
            }

            public void Fallback()
            {
            }

            public bool IsReverse(IFallbackable fallbackable)
            {
                return false;
            }

            public IFallbackable Reverse()
            {
                return null;
            }
        }
        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        public void CleanAllRecords_AllMustBeClear(int count)
        {
            var ways = new FallbackCommandWays<NullFallback>();
            for (int i = 0; i < count; i++)
            {
                ways.Push(new NullFallback());
            }
            ways.CleanAllRecords();
            Assert.AreEqual(0, ways.Count);
        }
        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        public void Undo_AllMustFallback(int count)
        {
            var ways = new FallbackCommandWays<ValueFallBack>();
            var fb = Enumerable.Range(0, count).Select(x => new ValueFallBack()).ToArray();
            for (int i = 0; i < count; i++)
            {
                ways.Push(fb[i]);
            }
            ways.Undo();
            for (int i = 0; i < fb.Length; i++)
            {
                Assert.IsTrue(fb[i].IsFallback, "Fail to true fallback {0}",i);
            }
        }
        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        public void Undo_InterfaceMethod_AllMustFallback(int count)
        {
            var ways = new FallbackCommandWays<ValueFallBack>();
            var fb = Enumerable.Range(0, count).Select(x => new ValueFallBack()).ToArray();
            for (int i = 0; i < count; i++)
            {
                ways.Push(fb[i]);
            }
            ((IUndoRedo)ways).Undo(true);
            for (int i = 0; i < fb.Length; i++)
            {
                Assert.IsTrue(fb[i].IsFallback, "Fail to true fallback {0}", i);
            }
        }
        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        public void Redo_AllMustReverseFallback(int count)
        {
            var ways = new FallbackCommandWays<ValueFallBack>();
            var fb = Enumerable.Range(0, count).Select(x => new ValueFallBack()).ToArray();
            for (int i = 0; i < count; i++)
            {
                ways.Push(fb[i]);
            }
            ways.Redo();
            for (int i = 0; i < fb.Length; i++)
            {
                Assert.IsFalse(fb[i].IsFallback, "Fail to false fallback {0}", i);
                Assert.IsTrue(fb[i].IsDoReverse, "Fail to true reverse fallback {0}", i);
            }
        }
        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        public void Redo_InterfaceMethod_AllMustReverseFallback(int count)
        {
            var ways = new FallbackCommandWays<ValueFallBack>();
            var fb = Enumerable.Range(0, count).Select(x => new ValueFallBack()).ToArray();
            for (int i = 0; i < count; i++)
            {
                ways.Push(fb[i]);
            }
            ((IUndoRedo)ways).Redo(true);
            for (int i = 0; i < fb.Length; i++)
            {
                Assert.IsFalse(fb[i].IsFallback, "Fail to false fallback {0}", i);
                Assert.IsTrue(fb[i].IsDoReverse, "Fail to true reverse fallback {0}", i);
            }
        }
    }
}
