using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing.Test
{
    [TestClass]
    public class FlatReflectionHelperTest
    {
        class A
        {
            public string Name { get; set; }

            public int Age { get; set; }

            public double Q { get; set; }
        }
        class B
        {
            public string Name { get; set; }

            public int Age { get; set; }

            public int Q { get; set; }
        }
        [TestMethod]
        public void FlatMap()
        {
            var a = new A();
            var b = new B();
            var res = FlatReflectionHelper.SpecularMapping(a, b);
            var map = res.ToDictionary(x => x.SourceProperty.Name);
            var succeeds = new HashSet<string>();
            succeeds.Add(nameof(B.Name));
            succeeds.Add(nameof(B.Age));
            foreach (var item in map)
            {
                Assert.AreEqual(item.Value.SourceProperty.Name,
                    item.Value.TargetProperty.Name,
                    "Fail to equals name {0} {1}",
                    item.Value.SourceProperty.Name,
                    item.Value.TargetProperty.Name);
                if (item.Value.Succeed)
                {
                    Assert.AreEqual(item.Value.SourceProperty.PropertyType,
                        item.Value.TargetProperty.PropertyType,
                        "Fail to equals type {0} {1}",
                        item.Value.SourceProperty.PropertyType,
                        item.Value.TargetProperty.PropertyType);
                }
                var source = item.Value.SourceProperty.GetValue(a);
                var target = item.Value.TargetProperty.GetValue(b);
                if (succeeds.Contains(item.Key))
                {
                    Assert.IsTrue(item.Value.Succeed,
                        "Fail to succeed flat map {0}",
                        item.Value.SourceProperty.Name);
                    Assert.AreEqual(source, target);
                    Assert.AreEqual(source, item.Value.SourceValue);
                }
                else
                {
                    Assert.IsFalse(item.Value.Succeed,
                        "Fail to fail flat map {0}",
                        item.Value.SourceProperty.Name);
                    Assert.AreNotEqual(source, target);
                }
            }
        }
    }
}
