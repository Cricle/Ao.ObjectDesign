using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf.Test
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
            var stu = new Student();
            var prop = stu.GetType().GetProperties()[0];

            var visitor = new NotifyCompiledPropertyVisitor(stu, prop);

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
