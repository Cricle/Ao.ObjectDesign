using Ao.ObjectDesign.ForView;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows;

namespace Ao.ObjectDesign.Test
{
    [TestClass]
    public class UIGeneratorTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            var designer = new ObjectDesigner();
            var builder = new ForViewBuilder<FrameworkElement, WpfForViewBuildContext>();

            Assert.ThrowsException<ArgumentNullException>(() => new UIGenerator(null));
            Assert.ThrowsException<ArgumentNullException>(() => new UIGenerator(null, builder));
            Assert.ThrowsException<ArgumentNullException>(() => new UIGenerator(designer, null));
        }
        [TestMethod]
        public void Init_PropertyMustDefaultOrInput()
        {
            var designer = new ObjectDesigner();
            var builder = new ForViewBuilder<FrameworkElement, WpfForViewBuildContext>();

            var gen = new UIGenerator();
            Assert.IsNotNull(gen.Builder);
            Assert.IsNotNull(gen.Designer);

            gen = new UIGenerator(builder);
            Assert.IsNotNull(gen.Builder);
            Assert.IsNotNull(gen.Designer);
            Assert.AreEqual(builder, gen.Builder);

            gen = new UIGenerator(designer, builder);
            Assert.IsNotNull(gen.Builder);
            Assert.IsNotNull(gen.Designer);
            Assert.AreEqual(builder, gen.Builder);
            Assert.AreEqual(designer, gen.Designer);
        }
    }
}
