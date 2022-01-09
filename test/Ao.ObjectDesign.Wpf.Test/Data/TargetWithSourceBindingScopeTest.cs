using Ao.ObjectDesign.Wpf.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ao.ObjectDesign.Wpf.Test.Data
{
    [TestClass]
    public class TargetWithSourceBindingScopeTest
    {
        class NullTargetWithSourceBindingScope : TargetWithSourceBindingScope
        {
            public NullTargetWithSourceBindingScope(object source, IBindingScope scope) : base(source, scope)
            {
            }

            protected override object GetTargetValue()
            {
                return null;
            }
        }
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            var source = new object();
            var scope = new NullBindingScope();

            Assert.ThrowsException<ArgumentNullException>(() => new NullTargetWithSourceBindingScope(null, scope));
            Assert.ThrowsException<ArgumentNullException>(() => new NullTargetWithSourceBindingScope(source, null));
        }
        [TestMethod]
        public void GivenValueInit_PropertyMustEqualInpts()
        {
            var source = new object();
            var scope = new NullBindingScope();

            var bs = new NullTargetWithSourceBindingScope(source, scope);

            Assert.AreEqual(source, bs.Source);
            Assert.AreEqual(scope, bs.Scope);
        }
        [TestMethod]
        public void BindToProperty()
        {
            var source = new object();
            var scope = new ValueBindingScope();

            var bs = new NullTargetWithSourceBindingScope(source, scope);
            bs.Bind(null);

            Assert.IsTrue(scope.IsBind);

            scope.IsBind = false;
            bs.Bind(null, null);
            Assert.IsTrue(scope.IsBind);
        }
    }
}
