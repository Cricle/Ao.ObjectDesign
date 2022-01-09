using Ao.ObjectDesign.Designing.Level;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ao.ObjectDesign.Designing.Test.Level
{
    [TestClass]
    public class DesignPairTest
    {
        [TestMethod]
        public void InitWithValues()
        {
            var ui = "aaa";
            var obj = "bbb";
            var pair = new DesignPair<string, string>(ui, obj);

            Assert.AreEqual(ui, pair.UI);
            Assert.AreEqual(obj, pair.DesigningObject);
        }
    }
}
