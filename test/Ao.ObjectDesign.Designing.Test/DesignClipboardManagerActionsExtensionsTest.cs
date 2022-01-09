using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing.Test
{
    [TestClass]
    public class DesignClipboardManagerActionsExtensionsTest
    {
        [TestMethod]
        public void GivenNullCall_MustThrowException()
        {
            var mgr = new ValueDesignClipboardManager();
            Func<int, int> fc = x => x;
            Assert.ThrowsException<ArgumentNullException>(() => DesignClipboardManagerActionsExtensions.GetFromCopied(mgr, null, false, false));
            Assert.ThrowsException<ArgumentNullException>(() => DesignClipboardManagerActionsExtensions.GetFromCopied(null, fc, false, false));
        }
        [TestMethod]
        public void GetFromCopied()
        {
            var mgr = new ValueDesignClipboardManager();
            var val = new List<int> { 1, 2, 3 };
            mgr.SetCopiedObject(val, false);

            var res = DesignClipboardManagerActionsExtensions.GetFromCopied(mgr);
            Assert.AreNotEqual(val, res);

            res = DesignClipboardManagerActionsExtensions.GetFromCopied(mgr, true);
            Assert.AreNotEqual(val, res);

            res = DesignClipboardManagerActionsExtensions.GetFromCopied(mgr, true, true);
            Assert.AreNotEqual(val, res);

            res = DesignClipboardManagerActionsExtensions.GetFromCopied(mgr, x => x, true, true);
            Assert.AreNotEqual(val, res);

            res = DesignClipboardManagerActionsExtensions.GetFromCopied(mgr, x => x);
            Assert.AreNotEqual(val, res);

            res = DesignClipboardManagerActionsExtensions.GetFromCopied(mgr, x => x, true);
            Assert.AreNotEqual(val, res);

            mgr.SetCopiedObject(null, false);

            res = DesignClipboardManagerActionsExtensions.GetFromCopied(mgr, true);
            Assert.AreEqual(0, res.Count);
        }
    }
}
