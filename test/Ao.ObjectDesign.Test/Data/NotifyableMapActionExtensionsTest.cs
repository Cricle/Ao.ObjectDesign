using Ao.ObjectDesign.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Test.Data
{
    [TestClass]
    public class NotifyableMapActionExtensionsTest
    {
        [TestMethod]
        public async Task GivenNullCall_MustThrowException()
        {
            var map = new NotifyableMap<string, string>();
            var datas = new Dictionary<string, string>();

            Assert.ThrowsException<ArgumentNullException>(() => NotifyableMapActionExtensions.AddOrUpdateMany(null, datas));
            Assert.ThrowsException<ArgumentNullException>(() => NotifyableMapActionExtensions.AddOrUpdateMany(map, null));

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => NotifyableMapActionExtensions.AddOrUpdateManyAsync(null, datas));
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => NotifyableMapActionExtensions.AddOrUpdateManyAsync(map, null));
        }
        [TestMethod]
        public void AddOrUpdateMany_ValuesMustAdded()
        {
            var map = new NotifyableMap<string, string>();
            var datas = new Dictionary<string, string>
            {
                ["a"] = "1",
                ["b"] = "2"
            };
            NotifyableMapActionExtensions.AddOrUpdateMany(map, datas);
            Assert.AreEqual(2, map.Count);
            Assert.AreEqual("1", map["a"]);
            Assert.AreEqual("2", map["b"]);
        }
        [TestMethod]
        public async Task AddOrUpdateManyAsync_ValuesMustAdded()
        {
            var map = new NotifyableMap<string, string>();
            var datas = new Dictionary<string, string>
            {
                ["a"] = "1",
                ["b"] = "2"
            };
            await NotifyableMapActionExtensions.AddOrUpdateManyAsync(map, datas);
            Assert.AreEqual(2, map.Count);
            Assert.AreEqual("1", map["a"]);
            Assert.AreEqual("2", map["b"]);
        }
    }
}
