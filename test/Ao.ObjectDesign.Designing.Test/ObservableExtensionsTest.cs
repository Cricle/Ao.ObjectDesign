using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;

namespace Ao.ObjectDesign.Designing.Test
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
        public void GivenNull_MustThrowException()
        {
            var s = new Student();
            INotifyPropertyChanged n = s;
            Assert.ThrowsException<ArgumentNullException>(() => ObservableExtensions.Subscribe<Student>(null, x => x.Name, x => { }));
            Assert.ThrowsException<ArgumentNullException>(() => ObservableExtensions.Subscribe<Student>(s, null, x => { }));
            Assert.ThrowsException<ArgumentNullException>(() => ObservableExtensions.Subscribe<Student>(s, x => x.Name, null));
            Assert.ThrowsException<ArgumentException>(() => ObservableExtensions.Subscribe(n, (string)null, x => { }));
            Assert.ThrowsException<ArgumentNullException>(() => ObservableExtensions.Subscribe(null, "Name", x => { }));
            Assert.ThrowsException<ArgumentNullException>(() => ObservableExtensions.Subscribe(n, "Name", null));
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
