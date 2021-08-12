using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Wpf.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.WpfDesign.Test
{
    [TestClass]
    public class DesignUnitTest
    {
        class NullDesignUnit : DesignUnit<object>
        {
            public NullDesignUnit(UIElement ui, object designingObject) : base(ui, designingObject)
            {
            }

            public NullDesignUnit(UIElement ui, object designingObject, DesignMapping mapping) : base(ui, designingObject, mapping)
            {
            }

            protected override IEnumerable<IWithSourceBindingScope> CreateWithSourceBindingScope()
            {
                return null;
            }
        }
        class ValueDesignUnit : DesignUnit<object>
        {
            public ValueDesignUnit(UIElement ui, object designingObject) : base(ui, designingObject)
            {
            }

            public ValueDesignUnit(UIElement ui, object designingObject, DesignMapping mapping) : base(ui, designingObject, mapping)
            {
            }

            public IEnumerable<IWithSourceBindingScope> Scopes { get; set; }

            protected override IEnumerable<IWithSourceBindingScope> CreateWithSourceBindingScope()
            {
                return Scopes;
            }
        }
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            ThreadRunner.STA(() =>
            {
                var ui = new FrameworkElement();
                var obj = new object();
                var map = new DesignMapping(typeof(FrameworkElement), typeof(object));
                Assert.ThrowsException<ArgumentNullException>(() => new NullDesignUnit(ui, null));
                Assert.ThrowsException<ArgumentNullException>(() => new NullDesignUnit(null, obj));

                Assert.ThrowsException<ArgumentNullException>(() => new NullDesignUnit(ui, null, map));
                Assert.ThrowsException<ArgumentNullException>(() => new NullDesignUnit(null, obj, map));
                Assert.ThrowsException<ArgumentNullException>(() => new NullDesignUnit(ui, obj, null));
            });
        }
        [TestMethod]
        public void AddScopes()
        {
            ThreadRunner.STA(() =>
            {
                var ui = new FrameworkElement();
                var obj = new object();
                var unit = new NullDesignUnit(ui, obj);
                var scope1 = new NullWithSourceBindingScope();
                var scope2 = new NullWithSourceBindingScope();
                unit.AddScopes(scope1, scope2);
                Assert.AreEqual(2, unit.BindingScopes.Count);
                Assert.AreEqual(scope1, unit.BindingScopes[0]);
                Assert.AreEqual(scope2, unit.BindingScopes[1]);
            });
        }
        [TestMethod]
        public void Build()
        {
            ThreadRunner.STA(() =>
            {
                var ui = new FrameworkElement();
                var obj = new object();
                var unit = new ValueDesignUnit(ui, obj);
                var scope1 = new NullWithSourceBindingScope();
                unit.Scopes = new IWithSourceBindingScope[] { scope1 };
                unit.Build();
                Assert.AreEqual(scope1, unit.BindingScopes[0]);
            });
        }
        [TestMethod]
        public void Bind()
        {
            ThreadRunner.STA(() =>
            {
                var ui = new FrameworkElement();
                var obj = new object();
                var unit = new ValueDesignUnit(ui, obj);
                var scope1 = new NullWithSourceBindingScope();
                unit.Scopes = new IWithSourceBindingScope[] { scope1 };
                unit.Build();
                var lst=unit.Bind();
                Assert.AreEqual(1, lst.Count);
                Assert.IsNull(lst[0]);
            });
        }
        [TestMethod]
        public void Property_MustEqualsInput()
        {
            ThreadRunner.STA(() =>
            {
                var ui = new FrameworkElement();
                var obj = new object();
                var map = new DesignMapping(obj.GetType(), ui.GetType());
                var unit = new ValueDesignUnit(ui, obj);
                Assert.AreEqual(map.UIType, unit.Mapping.UIType);
                Assert.AreEqual(map.ClrType, unit.Mapping.ClrType);

                unit = new ValueDesignUnit(ui, obj, map);
                Assert.AreEqual(map, unit.Mapping);
            });
        }
    }
}
