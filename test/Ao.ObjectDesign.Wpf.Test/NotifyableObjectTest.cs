using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf.Test
{
    [TestClass]
    public class NotifyableObjectTest
    {
        class StudentObject:NotifyableObject
        {
            private string name;

            public string Name
            {
                get => name;
                set => Set(ref name, value);
            }
        }
        [TestMethod]
        public void SetValue_EventMustRaised()
        {
            var obj = new StudentObject();
            obj.Name = "ooo";
            object o1 = null;
            string p1 = null;
            obj.PropertyChanging += (o, e) =>
            {
                o1 = o;
                p1 = e.PropertyName;
            };
            object o2 = null;
            string p2 = null;
            obj.PropertyChanged += (o, e) =>
            {
                o2 = o;
                p2 = e.PropertyName;
            };
            object o3 = null;
            PropertyChangeToEventArgs eg = null;
            obj.PropertyChangeTo += (o, e) =>
            {
                o3 = o;
                eg = e;
            };
            obj.Name = "hello";

            Assert.AreEqual(obj, o1);
            Assert.AreEqual(nameof(StudentObject.Name), p1);
            Assert.AreEqual(obj, o2);
            Assert.AreEqual(nameof(StudentObject.Name), p2);
            Assert.AreEqual(obj, o3);
            Assert.AreEqual("ooo", eg.From);
            Assert.AreEqual("hello", eg.To);
            Assert.AreEqual(nameof(StudentObject.Name), eg.PropertyName);
        }
    }
}
