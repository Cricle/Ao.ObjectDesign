using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ao.ObjectDesign.Wpf.Test
{
    [TestClass]
    public class ObservableExtensionsTest
    {
        class Student : NotifyableObject
        {
            private string name;
            private int age;
            public int Age
            {
                get => age;
                set => Set(ref age, value);
            }
            public string Name
            {
                get { return name; }
                set => Set(ref name, value);
            }
        }
        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void Subject_Notify(bool expression)
        {
            Student stu = new Student();
            object inst = null;
            IDisposable dis = null;
            if (expression)
            {
                dis = ObservableExtensions.Subscribe(stu, x => x.Name, o =>
                  {
                      inst = o;
                  });
            }
            else
            {
                dis = ObservableExtensions.Subscribe(stu, nameof(Student.Name), o =>
                {
                    inst = o;
                });
            }
            stu.Name = "world";
            Assert.AreEqual(stu, inst);
            inst = null;
            stu.Age = 333;
            Assert.IsNull(inst);
            dis.Dispose();
            stu.Name = "iii";
            Assert.IsNull(inst);
        }
    }
}
