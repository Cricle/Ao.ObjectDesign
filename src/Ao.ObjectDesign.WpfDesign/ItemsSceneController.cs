using Ao.ObjectDesign.Designing.Level;
using System;
using System.Collections;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class ItemsSceneController<TDesignObject> : LazyMapSceneController<TDesignObject>
    {
        public ItemsSceneController(IDesignPackage<TDesignObject> designMap, IList uiElements) 
            : base(designMap)
        {
            UIElements = uiElements ?? throw new ArgumentNullException(nameof(uiElements));
        }

        public IList UIElements { get; }

        protected override void RemoveFromContainer(IDesignPair<UIElement, TDesignObject> unit)
        {
            UIElements.Remove(unit.UI);
        }
        protected override void AddToContainer(IDesignPair<UIElement, TDesignObject> unit)
        {
            UIElements.Add(unit.UI);
        }
    }
}
