using Ao.ObjectDesign.Designing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Test.Designing
{
    [TestClass]
    public class ColumnDefinitionSettingTest
    {
        [TestMethod]
        public void SetFromWpfValue()
        {
            ColumnDefinitionDesigner design = new ColumnDefinitionDesigner
            {

            };
            design.SetColumnDefinition(new ColumnDefinition
            {
                Width = new GridLength(4, GridUnitType.Star),
                MaxWidth = 10,
                MinWidth = 1
            });

            Assert.AreEqual(10d, design.MaxWidth);
            Assert.AreEqual(1d, design.MinWidth);
            Assert.AreEqual(GridUnitType.Star, design.GridLengthSetting.Type);
            Assert.AreEqual(4d, design.GridLengthSetting.Value);
        }

        [TestMethod]
        public void MakeWpfValue()
        {
            ColumnDefinitionDesigner design = new ColumnDefinitionDesigner
            {
                MaxWidth = 10,
                MinWidth = 1,
                GridLengthSetting = new GridLengthDesigner { Type = GridUnitType.Star, Value = 4 }
            };

            ColumnDefinition val = design.GetColumnDefinition();

            Assert.AreEqual(10d, val.MaxWidth);
            Assert.AreEqual(1d, val.MinWidth);
            Assert.AreEqual(GridUnitType.Star, val.Width.GridUnitType);
            Assert.AreEqual(4d, val.Width.Value);
        }
    }
}
