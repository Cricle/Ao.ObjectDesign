using Ao.ObjectDesign;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.WpfDesign;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class MapCdevSceneController<TDesignObject> : DesignSceneController<TDesignObject>
    {
        protected MapCdevSceneController(UIDesignMap designMap, UIElementCollection uiElements)
        {
            DesignMap = designMap;
            UIElements = uiElements;
        }

        public UIDesignMap DesignMap { get; }

        public UIElementCollection UIElements { get; }

        protected override void AddUIElement(IDesignUnit<TDesignObject> unit)
        {
            UIElements.Add(unit.UI);
        }
        protected override void RemoveUIElement(IDesignUnit<TDesignObject> unit)
        {
            UIElements.Remove(unit.UI);
        }

        protected override UIElement CreateUI(TDesignObject designingObject)
        {
            var t = DesignMap.GetUIType(designingObject.GetType());
            if (t is null)
            {
                return null;
            }
            return (UIElement)DesignMap.CreateByFactoryOrEmit(t);
        }
    }
}
