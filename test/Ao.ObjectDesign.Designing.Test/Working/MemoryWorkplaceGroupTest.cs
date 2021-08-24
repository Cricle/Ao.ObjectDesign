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
    public class MemoryWorkplaceGroupTest
    {
        [TestMethod]
        public void InitWithNull_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new MemoryWorkplaceGroup<string, int>((IDictionary<string,IDictionary<string,int>>)null));
            Assert.ThrowsException<ArgumentNullException>(() => new MemoryWorkplaceGroup<string, int>("a",null));
        }
        [TestMethod]
        public void Init()
        {
            var group = new MemoryWorkplaceGroup<string, int>();

            group = new MemoryWorkplaceGroup<string, int>("a");

            Assert.AreEqual("a", group.Key);

            group = new MemoryWorkplaceGroup<string, int>(new Dictionary<string, IDictionary<string, int>>
            {
                ["a"] = new Dictionary<string, int> { ["b"] = 1 }
            });

            Assert.IsNotNull(group.Get("a"));
            Assert.AreEqual(1,group.Get("a").Get("b"));

            group = new MemoryWorkplaceGroup<string, int>("q",new Dictionary<string, IDictionary<string, int>>
            {
                ["a"] = new Dictionary<string, int> { ["b"] = 1 }
            });

            Assert.AreEqual("q", group.Key);
            Assert.IsNotNull(group.Get("a"));
            Assert.AreEqual(1, group.Get("a").Get("b"));
        }
        [TestMethod]
        public void Clear_AllMustClean()
        {
            var group = new MemoryWorkplaceGroup<string, int>();
            group.Create("aaa");
            group.Create("bbb");
            group.Create("ccc");

            Assert.AreEqual(3, group.Resources.Count());

            group.Clear();

            Assert.AreEqual(0, group.Resources.Count());
        }
    }
}
