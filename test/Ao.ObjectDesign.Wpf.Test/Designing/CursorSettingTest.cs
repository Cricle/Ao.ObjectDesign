using Ao.ObjectDesign.Designing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Input;

namespace Ao.ObjectDesign.Test.Designing
{
    [TestClass]
    public class CursorSettingTest
    {
        [TestMethod]
        public void MakeWpfValue_None()
        {
            CursorDesigner design = new CursorDesigner
            {
                CursorType = CursorTypes.None
            };

            Assert.IsNull(design.GetCursor());
        }
        [TestMethod]
        public void MakeWpfValue_SystemCursorName()
        {
            CursorDesigner design = new CursorDesigner
            {
                CursorType = CursorTypes.SystemCursorName,
                Name = Cursors.Wait.ToString()
            };

            Assert.AreEqual(Cursors.Wait, design.GetCursor());

        }
        [TestMethod]
        public void MakeWpfValue_SystemCursorName_NotFound()
        {
            CursorDesigner design = new CursorDesigner
            {
                CursorType = CursorTypes.SystemCursorName,
                Name = "dasiuw"
            };

            Assert.IsNull(design.GetCursor());
        }
    }
}
