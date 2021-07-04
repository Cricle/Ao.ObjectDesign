using Ao.ObjectDesign.Wpf.Designing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Ao.ObjectDesign.Wpf.Test.Designing
{
    [TestClass]
    public class DropShadowEffectSettingTest
    {
        [TestMethod]
        public void SetForWpfValue()
        {
            var design = new DropShadowEffectDesigner();

            design.DropShadowEffect = new DropShadowEffect
            {
                BlurRadius = 1,
                Color = Colors.White,
                Direction = 2,
                Opacity = 3,
                RenderingBias = RenderingBias.Performance,
                ShadowDepth = 4
            };

            Assert.AreEqual(1d, design.BlurRadius);
            Assert.AreEqual(Colors.White, design.Color.Color);
            Assert.AreEqual(2d, design.Direction);
            Assert.AreEqual(3d, design.Opacity);
            Assert.AreEqual(RenderingBias.Performance, design.RenderingBias);
            Assert.AreEqual(4d, design.ShadowDepth);
        }

        [TestMethod]
        public void MakeWpfValue()
        {
            var design = new DropShadowEffectDesigner();
            design.BlurRadius = 1;
            design.Color = new ColorDesigner { Color= Colors.White };
            design.Direction = 2;
            design.Opacity = 3;
            design.RenderingBias = RenderingBias.Performance;
            design.ShadowDepth = 4;

            var val = design.DropShadowEffect;

            Assert.AreEqual(1d, val.BlurRadius);
            Assert.AreEqual(Colors.White, val.Color);
            Assert.AreEqual(2d, val.Direction);
            Assert.AreEqual(3d, val.Opacity);
            Assert.AreEqual(RenderingBias.Performance, val.RenderingBias);
            Assert.AreEqual(4d, val.ShadowDepth);
        }
    }
}
