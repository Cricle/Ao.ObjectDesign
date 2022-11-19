using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class WpfPropertyVisitorTest
    {
        [TestMethod]
        public void GetSetDependencyPropert()
        {
            MyDepObject m = new MyDepObject();
            System.Reflection.PropertyInfo prop = m.GetType().GetProperties()[0];
            WpfPropertyVisitor visitor = new WpfPropertyVisitor(m, prop)
            {
                Value = 123
            };
            object val = m.GetValue(MyDepObject.AgeProperty);
            Assert.AreEqual(123, val);

            Assert.AreEqual(123, (int)visitor.Value);

            m.SetValue(MyDepObject.AgeProperty, 456);
            Assert.AreEqual(456, visitor.Value);

            visitor.Dispose();
        }
        [TestMethod]
        public void RaisePropertyChanged()
        {
            MyDepObject m = new MyDepObject();
            System.Reflection.PropertyInfo prop = m.GetType().GetProperties()[0];
            WpfPropertyVisitor visitor = new WpfPropertyVisitor(m, prop);

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
