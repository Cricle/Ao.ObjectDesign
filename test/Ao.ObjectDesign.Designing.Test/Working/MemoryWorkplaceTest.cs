using Ao.ObjectDesign.Designing.Working;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing.Test.Working
{
    [TestClass]
    public class MemoryWorkplaceTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new MemoryWorkplace<string,int>(null));
        }
        [TestMethod]
        public void Clear_WasClean()
        {
            var wp = new MemoryWorkplace<string, int>(new Dictionary<string, int>
            {
                ["a"] = 1,
                ["b"] = 2
            });

            Assert.AreEqual(2, wp.Resources.Count());
            wp.Clear();
            Assert.IsFalse(wp.Resources.Any());
        }
        [TestMethod]
        public void Copy_ValueMustBeCopied()
        {
            var wp = new MemoryWorkplace<string, int>(new Dictionary<string, int>
            {
                ["a"] = 1,
                ["b"] = 2
            });

            Assert.ThrowsException<ArgumentException>(() => wp.Copy("a", "a"));

            wp.Copy("a", "c");

            Assert.IsTrue(wp.Resources.Any(x => x == "c"));

            Assert.AreEqual(1, wp.Get("c"));
            Assert.IsTrue(wp.Has("c"));
        }
        [TestMethod]
        public void Remove_MustBeRemoved()
        {
            var wp = new MemoryWorkplace<string, int>(new Dictionary<string, int>
            {
                ["a"] = 1,
                ["b"] = 2
            });

            Assert.IsTrue(wp.Remove("a"));

            Assert.IsFalse(wp.Resources.Any(x => x == "a"));
            Assert.IsFalse(wp.Has("a"));

            Assert.AreEqual(1, wp.Resources.Count());
            Assert.AreEqual("b", wp.Resources.Single());

            Assert.IsFalse(wp.Remove("a"));
        }
        [TestMethod]
        public void Rename_MustRenamed()
        {
            var wp = new MemoryWorkplace<string, int>(new Dictionary<string, int>
            {
                ["a"] = 1,
                ["b"] = 2
            });

            wp.Rename("a", "q");

            Assert.IsTrue(wp.Resources.Any(x => x == "q"));
            Assert.IsTrue(wp.Has("q"));

            Assert.AreEqual(1, wp.Get("q"));
        }
        [TestMethod]
        public void Store_ResourceMustStored()
        {
            var wp = new MemoryWorkplace<string, int>(new Dictionary<string, int>
            {
            });

            wp.Store("a",1);

            Assert.AreEqual(1, wp.Resources.Count());
            Assert.AreEqual("a", wp.Resources.Single());
            Assert.AreEqual(1, wp.Get("a"));

            wp.Store("a", 2);

            Assert.AreEqual(1, wp.Resources.Count());
            Assert.AreEqual("a", wp.Resources.Single());
            Assert.AreEqual(2, wp.Get("a"));

        }
    }
}
