using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class MapSceneController<TDesignObject> : DesignSceneController<UIElement, TDesignObject>
    {
        protected MapSceneController(IDesignPackage<TDesignObject> designMap, UIElementCollection uiElements)
        {
            DesignPackage = designMap ?? throw new ArgumentNullException(nameof(designMap));
            UIElements = uiElements ?? throw new ArgumentNullException(nameof(uiElements));
            DesignMap = designMap.UIDesinMap;
            UseUnitDesignAttribute = true;
        }

        public UIDesignMap DesignMap { get; }

        public IDesignPackage<TDesignObject> DesignPackage { get; }

        public UIElementCollection UIElements { get; }

        public bool UseUnitDesignAttribute { get; set; }

        protected override void AddUIElement(IDesignPair<UIElement, TDesignObject> unit)
        {
            BindDesignUnit(unit);
            UIElements.Add(unit.UI);
        }

        protected override void RemoveUIElement(IDesignPair<UIElement, TDesignObject> unit)
        {
            UIElements.Remove(unit.UI);
        }
        protected virtual void BindDesignUnit(IDesignPair<UIElement, TDesignObject> unit)
        {
            IEnumerable<IWithSourceBindingScope> bindingScopes;
            if (UseUnitDesignAttribute&& unit.HasCreateAttributes())
            {
                var state = DesignPackage.CreateBindingCreatorState(unit);
                bindingScopes = unit.CreateFromAttribute(state);
            }
            else
            {
                bindingScopes = DesignPackage.CreateBindingScopes(unit);
            }
            Debug.Assert(bindingScopes != null);
            foreach (var item in bindingScopes)
            {
                item.Bind(unit.UI);
            }
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
