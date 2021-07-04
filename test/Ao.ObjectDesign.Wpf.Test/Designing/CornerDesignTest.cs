using Ao.ObjectDesign.Wpf.Designing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Test.Designing
{
    [TestClass]
    public class CornerDesignTest
    {
        [TestMethod]
        public void SetFromWpfValue()
        {
            var design = new CornerRadiusDesigner();
            design.CornerRadius = new CornerRadius(1, 2, 3, 4);


            Assert.AreEqual(1, design.Left);
            Assert.AreEqual(2, design.Top);
            Assert.AreEqual(3, design.Right);
            Assert.AreEqual(4, design.Bottom);
        }
        [TestMethod]
        public void MakeWpfValue()
        {
            var design = new CornerRadiusDesigner();
            design.Left = 1;
            design.Top = 2;
            design.Right = 3;
            design.Bottom = 4;

            var val = design.CornerRadius;

            Assert.AreEqual(1, val.TopLeft);
            Assert.AreEqual(2, val.TopRight);
            Assert.AreEqual(3, val.BottomRight);
            Assert.AreEqual(4, val.BottomLeft);

        }
    }
}
