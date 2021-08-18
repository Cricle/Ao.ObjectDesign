using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ao.ObjectDesign.Designing.Test
{
    [TestClass]
    public class UIDesignMapTest
    {
        class UIElement
        {

        }
        [TestMethod]
        public void RegistDesignType_MustRaisedEvent()
        {
            var map = new UIDesignMap();
            var uiType = typeof(UIElement);
            var designType = typeof(object);

            object sender = null;
            UIDesignMapActionDeisgnTypeEventArgs args = null;

            map.ActionDeisgnType += (o, e) =>
            {
                sender = o;
                args = e;
            };

            map.RegistDesignType(uiType, designType);

            Assert.AreEqual(map, sender);
            Assert.AreEqual(uiType, args.UIType);
            Assert.AreEqual(designType, args.DesignType);
            Assert.AreEqual(UIDesignMapActionTypes.New, args.ActionType);

            Assert.AreEqual(1, map.UITypeCount);
        }
        [TestMethod]
        public void ReplaceDesignType_MustRaisedEvent()
        {
            var map = new UIDesignMap();
            var uiType = typeof(UIElement);
            var designType = typeof(object);
            
            map.RegistDesignType(uiType, designType);

            object sender = null;
            UIDesignMapActionDeisgnTypeEventArgs args = null;

            map.ActionDeisgnType += (o, e) =>
            {
                sender = o;
                args = e;
            };

            map.RegistDesignType(uiType, designType);

            Assert.AreEqual(map, sender);
            Assert.AreEqual(uiType, args.UIType);
            Assert.AreEqual(designType, args.DesignType);
            Assert.AreEqual(UIDesignMapActionTypes.Replaced, args.ActionType);

            Assert.AreEqual(1, map.UITypeCount);
        }
        [TestMethod]
        public void UnRegistDesignType_MustRaisedEvent()
        {
            var map = new UIDesignMap();
            var uiType = typeof(UIElement);
            var designType = typeof(object);
            map.RegistDesignType(uiType, designType);
            object sender = null;
            UIDesignMapActionDeisgnTypeEventArgs args = null;

            map.ActionDeisgnType += (o, e) =>
            {
                sender = o;
                args = e;
            };

            var res = map.UnRegistDesignType(uiType, designType);

            Assert.AreEqual(map, sender);
            Assert.AreEqual(uiType, args.UIType);
            Assert.AreEqual(designType, args.DesignType);
            Assert.AreEqual(UIDesignMapActionTypes.Removed, args.ActionType);

            Assert.IsTrue(res);
            Assert.AreEqual(0, map.UITypeCount);
        }
        [TestMethod]
        public void ClearDesignType_MustRaisedEvent()
        {
            var map = new UIDesignMap();
            var uiType = typeof(UIElement);
            var designType = typeof(object);

            object sender = null;
            EventArgs args = null;

            map.CleanDeisgnTypes += (o, e) =>
            {
                sender = o;
                args = e;
            };

            map.RegistDesignType(uiType, designType);

            map.ClearDesignTypeMaps();

            Assert.AreEqual(map, sender);
            Assert.AreEqual(EventArgs.Empty, args);

            Assert.AreEqual(0, map.UITypeCount);
        }

    }
}
