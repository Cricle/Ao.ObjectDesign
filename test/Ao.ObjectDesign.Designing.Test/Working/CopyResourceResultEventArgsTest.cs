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
    public class CopyResourceResultEventArgsTest
    {
        [TestMethod]
        public void GivenValueInit_PropertyMustEqualsInput()
        {
            var val = new CopyResourceResultEventArgs<string>("a", "b");
            Assert.AreEqual("a", val.Key);
            Assert.AreEqual("b", val.ResultKey);
        }
    }
}
