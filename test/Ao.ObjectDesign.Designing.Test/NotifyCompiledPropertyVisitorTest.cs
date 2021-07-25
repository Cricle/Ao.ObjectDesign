using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ao.ObjectDesign.Designing.Test
{
    [TestClass]
    public class NotifyCompiledPropertyVisitorTest
    {
        class Student
        {
            public string Name { get; set; }
        }
        [TestMethod]
        public void SetValue_MustRaiseChangeToEvent()
        {
            Student stu = new Student();
            System.Reflection.PropertyInfo prop = stu.GetType().GetProperties()[0];

            NotifyCompiledPropertyVisitor visitor = new NotifyCompiledPropertyVisitor(stu, prop);

            object o1 = null;
            PropertyChangeToEventArgs e1 = null;
            visitor.PropertyChangeTo += (o, e) =>
            {
                o1 = o;
                e1 = e;
            };

            visitor.SetValue("hello");

            Assert.AreEqual("hello", stu.Name);
            Assert.AreEqual(visitor, o1);
            Assert.IsNull(e1.From);
            Assert.AreEqual("hello", e1.To);
            Assert.AreEqual(nameof(NotifyCompiledPropertyVisitor.Value), e1.PropertyName);
        }
    }
}
