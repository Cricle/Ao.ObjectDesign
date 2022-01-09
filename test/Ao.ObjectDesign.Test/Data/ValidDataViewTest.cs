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
            new ValidDataView<string>();
            new ValidDataView<string>(100, 10);
            new ValidDataView<string>(StringComparer.OrdinalIgnoreCase);
            var map = new ValidDataView<string>(new Dictionary<string, IVarValue>
            {
                ["a"] = VarValue.Byte0Value
            });
            Assert.AreEqual(VarValue.Byte0Value, map["a"]);
        }
    }
}
