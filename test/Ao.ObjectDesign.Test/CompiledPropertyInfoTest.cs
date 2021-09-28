using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class CompiledPropertyInfoTest
    {
        public class Student
        {
            public string Name { get; set; }

            public static readonly PropertyInfo NameProperty = typeof(Student).GetProperty(nameof(Name));
        }
        public abstract class AbstractStudent
        {

        }
        public class NoEmptyConstuct
        {
            public NoEmptyConstuct(int a) { }
        }
        [TestMethod]
        public void GivenNullCall_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => CompiledPropertyInfo.GetCreator(null));
        }
        [TestMethod]
        public void GivenAbstractOrNoEmptyConstruct_MustThrowException()
        {
            Assert.ThrowsException<NotSupportedException>(() => CompiledPropertyInfo.GetCreator(typeof(AbstractStudent)));
            Assert.ThrowsException<NotSupportedException>(() => CompiledPropertyInfo.GetCreator(typeof(NoEmptyConstuct)));
        }
        [TestMethod]
        public void GivenType_MustGotCreator()
        {
            var type = typeof(Student);
            var creator = CompiledPropertyInfo.GetCreator(type);
            Assert.IsNotNull(creator);
            var inst = creator();
            Assert.IsNotNull(inst);
            Assert.IsInstanceOfType(inst, type);

            var creator1 = CompiledPropertyInfo.GetCreator(type);
            Assert.AreEqual(creator, creator1);
        }
        [TestMethod]
        public void GivenPropertyInfo_MustGotGetterSetter()
        {
            var stu = new Student { Name="hello"};
            var identity = new PropertyIdentity(typeof(Student),nameof(Student.Name));
            var getter = CompiledPropertyInfo.GetGetter(identity);
            var val = getter(stu);
            Assert.AreEqual(stu.Name, val);
            var getter1= CompiledPropertyInfo.GetGetter(identity);
            Assert.AreEqual(getter, getter1);

            var setter = CompiledPropertyInfo.GetSetter(identity);
            setter(stu, "world");
            Assert.AreEqual("world", stu.Name);
            var setter1 = CompiledPropertyInfo.GetSetter(identity);
            Assert.AreEqual(setter, setter1);

        }
    }
}
