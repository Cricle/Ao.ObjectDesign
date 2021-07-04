using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf.Test
{
    [TestClass]
    public class WpfPropertyVisitorTest
    {
        [TestMethod]
        public void GetSetDependencyPropert()
        {
            var m = new MyDepObject();
            var prop = m.GetType().GetProperties()[0];
            var visitor = new WpfPropertyVisitor(m, prop);

            visitor.Value = 123;
            var val = m.GetValue(MyDepObject.AgeProperty);
            Assert.AreEqual(123, val);

            Assert.AreEqual(123, (int)visitor.Value);

            m.SetValue(MyDepObject.AgeProperty,456);
            Assert.AreEqual(456, visitor.Value);

            visitor.Dispose();
        }
        [TestMethod]
        public void RaisePropertyChanged()
        {
            var m = new MyDepObject();
            var prop = m.GetType().GetProperties()[0];
            var visitor = new WpfPropertyVisitor(m, prop);

            object obj = null;
            string name = null;
            visitor.PropertyChanged += (o, e) =>
            {
                obj = o;
                name = e.PropertyName;
            };
            m.SetValue(MyDepObject.AgeProperty, 222);
            Assert.AreEqual(m, obj);
            Assert.AreEqual(nameof(MyDepObject.Age), name);

            visitor.Dispose();

            obj = null;
            name = null;
            m.SetValue(MyDepObject.AgeProperty, 222);
            Assert.IsNull(obj);
            Assert.IsNull(name);
        }
    }
}
