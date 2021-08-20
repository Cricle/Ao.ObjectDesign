using Ao.ObjectDesign.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Test.Data
{
    [TestClass]
    public class ExternalReadOnlyDictionaryTest
    {
        [TestMethod]
        public void New()
        {
            new ExternalReadOnlyDictionary<string, string>();
            new ExternalReadOnlyDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            new ExternalReadOnlyDictionary<string, string>(100,10);
            new ExternalReadOnlyDictionary<string, string>(new Dictionary<string,string>());
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
    }
}
