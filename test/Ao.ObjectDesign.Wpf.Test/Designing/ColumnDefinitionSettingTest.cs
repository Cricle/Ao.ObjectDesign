using Ao.ObjectDesign.Wpf.Designing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Wpf.Test.Designing
{
    [TestClass]
    public class ColumnDefinitionSettingTest
    {
        [TestMethod]
        public void SetFromWpfValue()
        {
            var design = new ColumnDefinitionDesigner();
            design.ColumnDefinition = new ColumnDefinition
            {
                Width = new GridLength(4, GridUnitType.Star),
                MaxWidth = 10,
                MinWidth = 1
            };

            Assert.AreEqual(10d, design.MaxWidth);
            Assert.AreEqual(1d, design.MinWidth);
            Assert.AreEqual(GridUnitType.Star, design.GridLengthSetting.Type);
            Assert.AreEqual(4d, design.GridLengthSetting.Value);
        }

        [TestMethod]
        public void MakeWpfValue()
        {
            var design = new ColumnDefinitionDesigner();
            design.MaxWidth = 10;
            design.MinWidth = 1;
            design.GridLengthSetting = new GridLengthDesigner { Type = GridUnitType.Star, Value = 4 };

            var val = design.ColumnDefinition;

            Assert.AreEqual(10d, val.MaxWidth);
            Assert.AreEqual(1d, val.MinWidth);
            Assert.AreEqual(GridUnitType.Star, val.Width.GridUnitType);
            Assert.AreEqual(4d, val.Width.Value);
        }
    }
}
