using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ao.ObjectDesign.Designing.Test
{
    [TestClass]
    public class CompiledModifyDetailTest
    {
        [TestMethod]
        public void Copy()
        {
            var obj = new object();
            var name = "name";
            var from = new object();
            var to = new object();
            var detail = new CompiledModifyDetail(obj, name, from, to);

            var copied = detail.Copy(null);

            Assert.AreEqual(detail.Instance, copied.Instance);
            Assert.AreEqual(detail.PropertyName, copied.PropertyName);
            Assert.AreEqual(detail.From, copied.From);
            Assert.AreEqual(detail.To, copied.To);
            Assert.AreEqual(detail.Mode, copied.Mode);

            copied = detail.Copy(FallbackModes.Forward);

            Assert.AreEqual(detail.Instance, copied.Instance);
            Assert.AreEqual(detail.PropertyName, copied.PropertyName);
            Assert.AreEqual(detail.From, copied.From);
            Assert.AreEqual(detail.To, copied.To);
            Assert.AreEqual(FallbackModes.Forward, copied.Mode);

            copied = detail.Copy(FallbackModes.Reverse);

            Assert.AreEqual(detail.Instance, copied.Instance);
            Assert.AreEqual(detail.PropertyName, copied.PropertyName);
            Assert.AreEqual(detail.From, copied.From);
            Assert.AreEqual(detail.To, copied.To);
            Assert.AreEqual(FallbackModes.Reverse, copied.Mode);
        }
    }
}
