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
        public void GivenValue_MakeWpfValue()
        {
            var design = new CornerDesign();
            design.BorderColor = Colors.Yellow;
            design.BorderMargin = new Thickness(77);
            design.BorderWidth = 44;
            design.Bottom = 4;
        }
    }
}
