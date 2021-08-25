using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Designing.Data;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.Wpf.Designing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Wpf.Test.Data
{
    [TestClass]
    public class WpfBindingDrawingTest
    {
        class Pkg: NotifyableObject
        {
            private BrushDesigner foreground;
            private bool isDefault;

            public bool IsDefault
            {
                get => isDefault;
                set => Set(ref isDefault, value);
            }

            [BindFor("Foreground",VisitPath ="Foreground.Brush")]
            public BrushDesigner Foreground
            {
                get => foreground;
                set => Set(ref foreground, value);
            }
        }
        [MappingFor(typeof(Button))]
        class ButtonElement : NotifyableObject
        {
            private Pkg pkg;
            private string name;
            private double myWidth;

            [BindFor("Width",VisitPath ="Width")]
            public double MyWidth
            {
                get => myWidth;
                set => Set(ref myWidth, value);
            }

            public string Name
            {
                get => name;
                set => Set(ref name, value);
            }

            [UnfoldMapping]
            public Pkg Pkg
            {
                get => pkg;
                set => Set(ref pkg, value);
            }
        }
        [TestMethod]
        public void InitWithNull_MustThrowException()
        {
            var clrType = typeof(ButtonElement);
            var dependencyObjectType = typeof(Button);
            var bindForGetter = AttributeBindForGetter.Instance;

            Assert.ThrowsException<ArgumentNullException>(() => new WpfBindingDrawing(null));
            Assert.ThrowsException<ArgumentNullException>(() => new WpfBindingDrawing(clrType,null));
            Assert.ThrowsException<ArgumentNullException>(() => new WpfBindingDrawing(null,bindForGetter));
            Assert.ThrowsException<ArgumentNullException>(() => new WpfBindingDrawing(null, dependencyObjectType, bindForGetter));
            Assert.ThrowsException<ArgumentNullException>(() => new WpfBindingDrawing(clrType, dependencyObjectType, null));
            Assert.ThrowsException<ArgumentException>(() => new WpfBindingDrawing(typeof(object)));
        }
        [TestMethod]
        public void Analysis()
        {
            var drawing = new WpfBindingDrawing(typeof(ButtonElement));

            var items = drawing.Analysis().ToList();

            Assert.IsFalse(items.Any(x => x.PropertyInfo.Name == nameof(ButtonElement.Pkg)));

            var widthItem = items.Single(x => x.PropertyInfo.Name == nameof(ButtonElement.MyWidth));

            Assert.AreEqual("Width", widthItem.Path);
            Assert.AreEqual(Button.WidthProperty, widthItem.DependencyProperty);
            Assert.AreEqual(typeof(Button), widthItem.DependencyObjectType);
            Assert.AreEqual(typeof(ButtonElement), widthItem.ClrType);
            Assert.IsTrue(widthItem.HasPropertyBind);

            var nameItem= items.Single(x => x.PropertyInfo.Name == nameof(ButtonElement.Name));

            Assert.AreEqual("Name", nameItem.Path);
            Assert.AreEqual(Button.NameProperty, nameItem.DependencyProperty);
            Assert.AreEqual(typeof(Button), nameItem.DependencyObjectType);
            Assert.AreEqual(typeof(ButtonElement), nameItem.ClrType);
            Assert.IsTrue(nameItem.HasPropertyBind);

            var foregroundItem = items.Single(x => x.PropertyInfo.Name == nameof(Pkg.Foreground));

            Assert.AreEqual("Foreground.Brush", foregroundItem.Path);
            Assert.AreEqual(Button.ForegroundProperty, foregroundItem.DependencyProperty);
            Assert.AreEqual(typeof(Button), foregroundItem.DependencyObjectType);
            Assert.AreEqual(typeof(Pkg), foregroundItem.ClrType);
            Assert.IsTrue(foregroundItem.HasPropertyBind);

            var isDefaultItem = items.Single(x => x.PropertyInfo.Name == nameof(Pkg.IsDefault));

            Assert.AreEqual("Pkg.IsDefault", isDefaultItem.Path);
            Assert.AreEqual(Button.IsDefaultProperty, isDefaultItem.DependencyProperty);
            Assert.AreEqual(typeof(Button), isDefaultItem.DependencyObjectType);
            Assert.AreEqual(typeof(Pkg), isDefaultItem.ClrType);
            Assert.IsTrue(isDefaultItem.HasPropertyBind);

            Assert.IsNotNull(isDefaultItem.ToString());
        }
    }
}
