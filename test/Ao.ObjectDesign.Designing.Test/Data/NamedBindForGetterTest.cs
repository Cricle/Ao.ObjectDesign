using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Designing.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ao.ObjectDesign.Designing.Test.Data
{
    [TestClass]
    public class NamedBindForGetterTest
    {
        [BindFor("miao")]
        class A
        {
            public string Name { get; set; }
        }
        [TestMethod]
        public void InstanceMustEqualsAnyTimes()
        {
            var a = NamedBindForGetter.Instance;
            var b = NamedBindForGetter.Instance;

            Assert.AreEqual(a, b);
        }
        [TestMethod]
        public void Get_ValueMustNewed()
        {
            var prop = typeof(A).GetProperty(nameof(A.Name));

            var attr = NamedBindForGetter.Instance.Get(prop);
            Assert.IsNotNull(attr);
            Assert.AreEqual(prop.Name, attr.PropertyName);
        }
        [TestMethod]
        public void GetFromType_MustGotAttribute()
        {
            var t = typeof(A);

            var attr = NamedBindForGetter.Instance.Get(t);
            Assert.IsNotNull(attr);
            Assert.AreEqual("miao", attr.PropertyName);
        }
    }
}
