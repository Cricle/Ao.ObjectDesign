using Ao.ObjectDesign.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign.Test.Data
{
    [TestClass]
    public class ExternalReadOnlyDictionaryTest
    {
        class MyExternalReadOnlyDictionary : ExternalReadOnlyDictionary<string, string>
        {
            public MyExternalReadOnlyDictionary(IEqualityComparer<string> comparer) : base(comparer)
            {
            }
            public void Add(string key, string value)
            {
                if (originMap.ContainsKey(key))
                {
                    originMap[key] = value;
                }
                else
                {
                    originMap.Add(key, value);
                }
            }
        }
        [TestMethod]
        public void New()
        {
            new ExternalReadOnlyDictionary<string, string>();
            var res = new MyExternalReadOnlyDictionary(StringComparer.OrdinalIgnoreCase);
            res.Add("a", "1");
            res.Add("A", "2");
            Assert.AreEqual(1, res.Count);
            Assert.AreEqual("a", res.Keys.Single());
            Assert.AreEqual("2", res.Values.Single());
            new ExternalReadOnlyDictionary<string, string>(100, 10);
            var res1 = new ExternalReadOnlyDictionary<string, string>(new Dictionary<string, string> { ["a"] = "1" });
            Assert.AreEqual(1, res1.Count);
            Assert.AreEqual("a", res1.Keys.Single());
            Assert.AreEqual("1", res1.Values.Single());
        }
        [TestMethod]
        public void ReadOnlyMethods()
        {
            var map = new ExternalReadOnlyDictionary<string, string>(new Dictionary<string, string>
            {
                ["a"] = "a1",
                ["b"] = "b1",
                ["c"] = "c1",
                ["d"] = "d1"
            });

            Assert.AreEqual(4, map.Count);
            Assert.IsTrue(map.ContainsKey("a"));
            Assert.IsFalse(map.ContainsKey("w"));
            Assert.AreEqual("a1", map["a"]);
            Assert.AreEqual(4, map.Keys.Count());
            Assert.AreEqual(4, map.Values.Count());
            Assert.AreEqual(4, map.Count);
            Assert.IsTrue(map.TryGetValue("a", out var aval));
            Assert.AreEqual("a1", aval);
            Assert.IsNotNull(map.ToString());
        }
        [TestMethod]
        public void GetEnumerator()
        {
            var map = new ExternalReadOnlyDictionary<string, string>(new Dictionary<string, string>
            {
                ["a"] = "a1"
            });

            var enu = map.GetEnumerator();
            Assert.IsTrue(enu.MoveNext());
            Assert.AreEqual("a", enu.Current.Key);
            Assert.AreEqual("a1", enu.Current.Value);
            Assert.IsFalse(enu.MoveNext());

            var enu1 = ((IEnumerable)map).GetEnumerator();
            Assert.IsTrue(enu1.MoveNext());
            var val = (KeyValuePair<string, string>)enu1.Current;
            Assert.AreEqual("a", val.Key);
            Assert.AreEqual("a1", val.Value);
            Assert.IsFalse(enu1.MoveNext());
        }
    }
}
