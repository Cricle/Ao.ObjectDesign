using Ao.ObjectDesign.Wpf.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf.Test.Data
{
    [TestClass]
    public class PropertyWithSourceBindingScopeTest
    {
        [TestMethod]
        public void GivenNullInitOrCall_MustThrowException()
        {
            var source = new Student();
            var scope = new NullBindingScope();
            var member = typeof(Student).GetProperty(nameof(Student.Name));

            Assert.ThrowsException<ArgumentNullException>(() => new PropertyWithSourceBindingScope(source, null, scope));
            
            Assert.ThrowsException<ArgumentNullException>(() => PropertyWithSourceBindingScope.FromExpression<Student>(null,x=>x.Name , scope));
            Assert.ThrowsException<ArgumentNullException>(() => PropertyWithSourceBindingScope.FromExpression<Student>(source, null, scope));
            Assert.ThrowsException<ArgumentNullException>(() => PropertyWithSourceBindingScope.FromExpression<Student>(source, x => x.Name, null));
            Assert.ThrowsException<NotSupportedException>(() => PropertyWithSourceBindingScope.FromExpression<Student>(source, x => x.Age, scope));
        }
        class Student
        {
            public string Name { get; set; }

            public int Age;
        }
        [TestMethod]
        public void GivenValueInit_PropertyMustEqualInpts()
        {
            var source = new Student();
            var scope = new NullBindingScope();
            var member = source.GetType().GetProperty(nameof(Student.Name));

            var bs = new PropertyWithSourceBindingScope(source, member, scope);

            Assert.AreEqual(member, bs.PropertyInfo);
        }
        [TestMethod]
        public void ExpressionToPropertyCreated_PropertyMustEqualsExressionTarget()
        {
            var source = new Student();
            var scope = new NullBindingScope();
            var member = typeof(Student).GetProperty(nameof(Student.Name));

            var bs = PropertyWithSourceBindingScope.FromExpression(source, x => x.Name, scope);

            Assert.IsNotNull(bs);
            Assert.AreEqual(member, bs.PropertyInfo);
        }
    }
}
