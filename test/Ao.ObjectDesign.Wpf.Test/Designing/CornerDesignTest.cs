using Ao.ObjectDesign.Wpf.Designing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Test.Designing
{
    [TestClass]
    public class CornerDesignTest
    {
        [TestMethod]
        public void SetFromWpfValue()
        {
            CornerRadiusDesigner design = new CornerRadiusDesigner
            {
                CornerRadius = new CornerRadius(1, 2, 3, 4)
            };


            Assert.AreEqual(1, design.Left);
            Assert.AreEqual(2, design.Top);
            Assert.AreEqual(3, design.Right);
            Assert.AreEqual(4, design.Bottom);
        }
        [TestMethod]
        public void MakeWpfValue()
        {
            CornerRadiusDesigner design = new CornerRadiusDesigner
            {
                Left = 1,
                Top = 2,
                Right = 3,
                Bottom = 4
            };

            CornerRadius val = design.CornerRadius;

            Assert.AreEqual(1, val.TopLeft);
            Assert.AreEqual(2, val.TopRight);
            Assert.AreEqual(3, val.BottomRight);
            Assert.AreEqual(4, val.BottomLeft);

        }
    }
}
