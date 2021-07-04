using Ao.ObjectDesign.Wpf.Designing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ao.ObjectDesign.Wpf.Test.Designing
{
    [TestClass]
    public class CursorSettingTest
    {
        [TestMethod]
        public void MakeWpfValue_None()
        {
            var design = new CursorDesigner();
            design.CursorType = CursorTypes.None;

            Assert.IsNull(design.Cursor);
        }
        [TestMethod]
        public void MakeWpfValue_SystemCursorName()
        {
            var design = new CursorDesigner();
            design.CursorType = CursorTypes.SystemCursorName;
            design.Name = Cursors.Wait.ToString();

            Assert.AreEqual(Cursors.Wait, design.Cursor);

        }
        [TestMethod]
        public void MakeWpfValue_SystemCursorName_NotFound()
        {
            var design = new CursorDesigner();
            design.CursorType = CursorTypes.SystemCursorName;
            design.Name = "dasiuw";

            Assert.IsNull(design.Cursor);
        }
    }
}
