using Ao.ObjectDesign.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign.Test.Data
{
    [TestClass]
    public class DataViewTest
    {
        [TestMethod]
        public void Init()
        {
            var dv = new DataView<string>();
            var comparer = StringComparer.OrdinalIgnoreCase;
            dv = new DataView<string>(comparer);
            dv = new DataView<string>(new Dictionary<string, IVarValue> { ["a"] = VarValue.FalseValue });

            Assert.AreEqual("a", dv.Keys.Single());
            Assert.AreEqual(VarValue.FalseValue, dv.Values.Single());

            dv = new DataView<string>(10, 10);
        }
    }
}
