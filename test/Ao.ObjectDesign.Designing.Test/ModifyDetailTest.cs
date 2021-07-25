using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ao.ObjectDesign.Designing.Test
{
    [TestClass]
    public class ModifyDetailTest
    {
        [TestMethod]
        public void GivenValueInit_PropertyMustEqualInput()
        {
            object instance = new object();
            string propertyName = "name";
            object from = new object();
            object to = new object();
            ModifyDetail detail = new ModifyDetail(instance, propertyName, from, to);

            Assert.AreEqual(instance, detail.Instance);
            Assert.AreEqual(propertyName, detail.PropertyName);
            Assert.AreEqual(from, detail.From);
            Assert.AreEqual(to, detail.To);
        }
        [TestMethod]
        public void Reverse_MustCopied_FromAndToExchanged()
        {
            object instance = new object();
            string propertyName = "name";
            object from = new object();
            object to = new object();
            ModifyDetail detail = new ModifyDetail(instance, propertyName, from, to);
            ModifyDetail revDetail = detail.Reverse();

            Assert.AreNotEqual(detail, revDetail);
            Assert.AreEqual(detail.From, revDetail.To);
            Assert.AreEqual(detail.To, revDetail.From);

            Assert.AreEqual(detail.Instance, revDetail.Instance);
            Assert.AreEqual(detail.PropertyName, revDetail.PropertyName);
        }
    }
}
