using Ao.ObjectDesign.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Test.Data
{
    [TestClass]
    public class ValidDataViewTest
    {
        [TestMethod]
        public void New()
        {
            new ValidDataView<string,object>();
            new ValidDataView<string, object>(100, 10);
            new ValidDataView<string, object>(StringComparer.OrdinalIgnoreCase);
            var map = new ValidDataView<string, object>(new Dictionary<string, object>
            {
                ["a"] = (byte)0
            });
            Assert.AreEqual((byte)0, map["a"]);
        }
    }
}
