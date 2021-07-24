using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ao.ObjectDesign.Wpf.Annotations;
using Ao.ObjectDesign.Wpf.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ao.ObjectDesign.Wpf.Test.Data
{
    [TestClass]
    public class AttributeBindForGetterTest
    {
        [BindFor("well")]
        class MyClass
        {
            [BindFor("hello")]
            public int Name { get; set; }
        }
        [TestMethod]
        public void InstanceMustAlwaysEquals()
        {
            var a = AttributeBindForGetter.Instance;
            var b = AttributeBindForGetter.Instance;
            Assert.AreEqual(a, b);
            Assert.IsNotNull(a);
        }
        [TestMethod]
        public void GetAttributeFromTypeOrProperty_MustGot()
        {
            var getter = AttributeBindForGetter.Instance;
            var t = typeof(MyClass);
            var prop = t.GetProperty(nameof(MyClass.Name));
            var attr1 = getter.Get(t);
            Assert.IsNotNull(attr1);
            var attr2 = getter.Get(prop);
            Assert.IsNotNull(attr2);
        }
    }
}
