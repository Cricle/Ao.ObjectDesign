using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class MapCdevSceneController<TDesignObject> : DesignSceneController<UIElement,TDesignObject>
    {
        protected MapCdevSceneController(UIDesignMap designMap, UIElementCollection uiElements)
        {
            DesignMap = designMap;
            UIElements = uiElements;
        }

        public UIDesignMap DesignMap { get; }

        public UIElementCollection UIElements { get; }

        protected override void AddUIElement(IDesignPair<UIElement, TDesignObject> unit)
        {
            var wpfUnit = (IDesignUnit<TDesignObject>)unit;
            wpfUnit.Build();
            wpfUnit.Bind();
            UIElements.Add(unit.UI);
        }

        protected override void RemoveUIElement(IDesignPair<UIElement,TDesignObject> unit)
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
