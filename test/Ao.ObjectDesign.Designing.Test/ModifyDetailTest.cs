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
            IModifyDetail revDetail = detail.Reverse();

            Assert.AreNotEqual(detail, revDetail);
            Assert.AreEqual(detail.From, revDetail.To);
            Assert.AreEqual(detail.To, revDetail.From);

            Assert.AreEqual(detail.Instance, revDetail.Instance);
            Assert.AreEqual(detail.PropertyName, revDetail.PropertyName);

            revDetail = (IModifyDetail)((IFallbackable)detail).Reverse();
            

            Assert.AreNotEqual(detail, revDetail);
            Assert.AreEqual(detail.From, revDetail.To);
            Assert.AreEqual(detail.To, revDetail.From);

            Assert.AreEqual(detail.Instance, revDetail.Instance);
            Assert.AreEqual(detail.PropertyName, revDetail.PropertyName);
        }
        [TestMethod]
        public void Copy_MustAllSame()
        {
            object instance = new object();
            string propertyName = "name";
            object from = new object();
            object to = new object();
            ModifyDetail detail = new ModifyDetail(instance, propertyName, from, to);
            var cloned = detail.Copy(FallbackMode.Forward);
            Assert.AreNotEqual(detail, cloned);
            Assert.AreEqual(instance, cloned.Instance);
            Assert.AreEqual(propertyName, cloned.PropertyName);
            Assert.AreEqual(from, cloned.From);
            Assert.AreEqual(to, cloned.To);
            Assert.AreEqual(FallbackMode.Forward, cloned.Mode);

            cloned = (IModifyDetail)((IFallbackable)detail).Copy(FallbackMode.Forward);
            Assert.AreNotEqual(detail, cloned);
            Assert.AreEqual(instance, cloned.Instance);
            Assert.AreEqual(propertyName, cloned.PropertyName);
            Assert.AreEqual(from, cloned.From);
            Assert.AreEqual(to, cloned.To);
            Assert.AreEqual(FallbackMode.Forward, cloned.Mode);
        }
        [TestMethod]
        public void GetIdentity()
        {
            object instance = new object();
            string propertyName = "name";
            object from = new object();
            object to = new object();
            ModifyDetail detail = new ModifyDetail(instance, propertyName, from, to);
            var identity = detail.GetIgnoreIdentity();
            Assert.IsNotNull(identity);
            Assert.AreEqual(instance, identity.Value.Instance);
            Assert.AreEqual(propertyName, identity.Value.PropertyName);
        }
    }
}
