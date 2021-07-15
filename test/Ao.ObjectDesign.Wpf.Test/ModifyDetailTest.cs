using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf.Test
{
    [TestClass]
    public class ModifyDetailTest
    {
        [TestMethod]
        public void GivenValueInit_PropertyMustEqualInput()
        {
            var instance = new object();
            var propertyName = "name";
            var from = new object();
            var to = new object();
            var detail = new ModifyDetail(instance, propertyName, from, to);

            Assert.AreEqual(instance, detail.Instance);
            Assert.AreEqual(propertyName, detail.PropertyName);
            Assert.AreEqual(from, detail.From);
            Assert.AreEqual(to, detail.To);
        }
        [TestMethod]
        public void Reverse_MustCopied_FromAndToExchanged()
        {
            var instance = new object();
            var propertyName = "name";
            var from = new object();
            var to = new object();
            var detail = new ModifyDetail(instance, propertyName, from, to);
            var revDetail = detail.Reverse();

            Assert.AreNotEqual(detail, revDetail);
            Assert.AreEqual(detail.From, revDetail.To);
            Assert.AreEqual(detail.To, revDetail.From);

            Assert.AreEqual(detail.Instance, revDetail.Instance);
            Assert.AreEqual(detail.PropertyName, revDetail.PropertyName);
        }
    }
}
