using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class FrozenDictionaryTest
    {
        [TestMethod]
        public void Init()
        {
            var map = Enumerable.Range(0, 100)
                .ToDictionary(x => new object(), x => new object());
            var fd = FrozenDictionary<object, object>.Create(map);
            Assert.AreEqual(map.Count, fd.Count);
            fd = FrozenDictionary<object, object>.Create(map, EqualityComparer<object>.Default);
            Assert.AreEqual(map.Count, fd.Count);
            fd = FrozenDictionary<object, object>.Create(map,
                x=>x.Key,
                x=>x.Value,
                EqualityComparer<object>.Default);
            Assert.AreEqual(map.Count, fd.Count);
        }
        [TestMethod]
        public void GetEnumerable()
        {
            var map = Enumerable.Range(0, 100)
                .ToDictionary(x => new object(), x => new object());
            var fd = FrozenDictionary<object, object>.Create(map);
            var k = new HashSet<object>(map.Keys);
            Assert.AreEqual(map.Keys.Count(), fd.Keys.Count());
            foreach (var item in fd.Keys)
            {
                if (!k.Contains(item))
                {
                    Assert.Fail();
                }
            }
            var v = new HashSet<object>(map.Values);
            Assert.AreEqual(map.Values.Count(), fd.Values.Count());
            foreach (var item in fd.Values)
            {
                if (!v.Contains(item))
                {
                    Assert.Fail();
                }
            }
            foreach (var item in map)
            {
                if (fd.TryGetValue(item.Key,out var val))
                {
                    Assert.AreEqual(item.Value, val);
                }
                else
                {
                    Assert.Fail();
                }
            }
        }
        [TestMethod]
        public void CountEqualsEnumerableCount()
        {
            var map = Enumerable.Range(0, 100)
                .ToDictionary(x => new object(), x => new object());
            var fd = FrozenDictionary<object, object>.Create(map);
            Assert.AreEqual(fd.Count, fd.Count());
        }
        [TestMethod]
        public void StringFrozen()
        {
            var map = Enumerable.Range(0, 100)
                .ToDictionary(x => "dwiauwbd"+x, x => new object());
            var fd = FrozenDictionary<string, object>.Create(map);
            Assert.AreEqual(map.Keys.Count(), fd.Keys.Count());
            Assert.AreEqual(map.Values.Count(), fd.Values.Count());
        }
    }
}
