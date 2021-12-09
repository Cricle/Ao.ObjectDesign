using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class CompiledPropertyVisitorTest
    {
        class PrivateGet
        {
            private string name="hello";
            public string Name
            {
                set { name = value; }
            }
        }
        [TestMethod]
        public void PrivateGet_GetValuMustThrowException()
        {
            PrivateGet obj = new PrivateGet();
            System.Reflection.PropertyInfo prop = obj.GetType().GetProperty(nameof(PrivateGet.Name));
            ExpressionPropertyVisitor c = new ExpressionPropertyVisitor(obj, prop);
            Assert.ThrowsException<InvalidOperationException>(() => c.GetValue());
        }
        [TestMethod]
        public void GetSetValue()
        {
            Student student = new Student();
            System.Reflection.PropertyInfo prop = student.GetType().GetProperty(nameof(Student.Name));
            ExpressionPropertyVisitor c = new ExpressionPropertyVisitor(student, prop);
            c.SetValue("aaa");
            Assert.AreEqual("aaa", student.Name);
            object val = c.GetValue();
            Assert.AreEqual("aaa", val);

            val = c.Value;
            Assert.AreEqual("aaa", val);
            c.Value = "bbb";
            Assert.AreEqual("bbb", student.Name);
            c.SetValue("ddd");
            Assert.AreEqual("ddd", student.Name);
        }
        [TestMethod]
        public void GetSetPrimaryValue()
        {
            ReadOnlyStudent student = new ReadOnlyStudent();
            System.Reflection.PropertyInfo prop = student.GetType().GetProperty(nameof(ReadOnlyStudent.Age));
            ExpressionPropertyVisitor c = new ExpressionPropertyVisitor(student, prop);
            c.SetValue(333d);
            Assert.AreEqual(333d, student.Age);
            object val = c.GetValue();
            Assert.AreEqual(333d, val);

            val = c.Value;
            Assert.AreEqual(333d, val);
            c.Value = 444d;
            Assert.AreEqual(444d, student.Age);
            c.SetValue(555d);
            Assert.AreEqual(555d, student.Age);
            c.SetValue(123.456d);
            Assert.AreEqual(123.456d, student.Age);

        }
        class ReadOnlyStudent
        {
            public string Name { get; } = "aaa";

            public double Age { get; set; } = 123d;
        }
        [TestMethod]
        public void ReadOnlyProperty_GetMustGotValue_SetThrowException()
        {
            ReadOnlyStudent student = new ReadOnlyStudent();
            System.Reflection.PropertyInfo prop = student.GetType().GetProperty(nameof(ReadOnlyStudent.Name));
            ExpressionPropertyVisitor c = new ExpressionPropertyVisitor(student, prop);
            object val = c.GetValue();
            Assert.AreEqual("aaa", val);
            Assert.ThrowsException<InvalidOperationException>(() => c.SetValue("111"));
        }
    }
}
