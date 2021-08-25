using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing.Test
{

    [TestClass]
    public class DesignClipboardManagerTest
    {
        [TestMethod]
        public void GivenNullCall_MustReturnDefault()
        { 
            var mgr = new ValueDesignClipboardManager<object>();
            Assert.IsNull(mgr.Clone(null));
        }
        [TestMethod]
        public void SetCopiedObject()
        {
            var mgr = new ValueDesignClipboardManager();

            object sender = null;
            DesignClipboardCopiedResetEventArgs<int> args = null;

            mgr.CopiedObjectChanged += (o, e) =>
            {
                sender = o;
                args = e;
            };

            var values = new List<int> { 1, 23, 4 };

            mgr.SetCopiedObject(values,false);

            Assert.AreEqual(mgr, sender);
            Assert.IsNull(args.OldOriginObjects);
            Assert.IsNull(args.OldCopiedObjects);

            Assert.AreEqual(values, args.NewOriginObjects);
            Assert.AreEqual(values, args.NewCopiedObjects);

            sender = null;
            args = null;

            var otherValues = new List<int>();

            mgr.SetCopiedObject(otherValues, false);

            Assert.AreEqual(mgr, sender);
            Assert.AreEqual(values,args.OldOriginObjects);
            Assert.AreEqual(values, args.OldCopiedObjects);

            Assert.AreEqual(otherValues, args.NewOriginObjects);
            Assert.AreEqual(otherValues, args.NewCopiedObjects);

            sender = null;
            args = null;

            mgr.SetCopiedObject(values, true);

            Assert.AreEqual(mgr, sender);
            Assert.AreEqual(otherValues, args.OldOriginObjects);
            Assert.AreEqual(otherValues, args.OldCopiedObjects);

            Assert.AreEqual(values, args.NewOriginObjects);
            Assert.AreNotEqual(values, args.NewCopiedObjects);

            sender = null;
            args = null;

            mgr.SetCopiedObject(otherValues, true);

            Assert.AreEqual(mgr, sender);
            Assert.AreEqual(values, args.OldOriginObjects);
            Assert.AreNotEqual(values, args.OldCopiedObjects);

            Assert.AreEqual(otherValues, args.NewOriginObjects);
            Assert.AreNotEqual(otherValues, args.NewCopiedObjects);

        }
        [TestMethod]
        public void UpdateFromClipboard()
        {
            var mgr = new ValueDesignClipboardManager();
            var lst = new List<int>();
            mgr.Values = lst;
            mgr.UpdateFromClipboard(true, false);

            Assert.AreEqual(lst, mgr.OriginObjects);

            mgr.Values = null;

            mgr.UpdateFromClipboard(false, false);

            Assert.AreEqual(lst, mgr.OriginObjects);

            mgr.UpdateFromClipboard(true, false);

            Assert.IsNull(mgr.OriginObjects);

        }
        [TestMethod]
        public void Clone()
        {
            var mgr = new ValueDesignClipboardManager<object>();
            var v = new object();
            var val=mgr.Clone(v);
            Assert.AreNotEqual(v, val);
            Assert.IsInstanceOfType(val, typeof(object));
        }
    }
}
