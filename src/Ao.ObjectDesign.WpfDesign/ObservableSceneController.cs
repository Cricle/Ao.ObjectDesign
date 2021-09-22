using Ao.ObjectDesign.Designing.Level;
using System.Collections.ObjectModel;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class ObservableSceneController<TDesignObject> : LazyMapSceneController<TDesignObject>
    {
        protected ObservableSceneController(IDesignPackage<TDesignObject> designMap)
            : this(designMap, new SilentObservableCollection<object>())
        {

        }

        protected ObservableSceneController(IDesignPackage<TDesignObject> designMap, SilentObservableCollection<object> items)
            : base(designMap)
        {
            Items = items;
        }

        public SilentObservableCollection<object> Items { get; }

        protected override void AddToContainer(IDesignPair<UIElement, TDesignObject> unit)
        {
            Items.Add(unit.UI);
        }

        protected override void RemoveFromContainer(IDesignPair<UIElement, TDesignObject> unit)
        {
            Items.Remove(unit.UI);
        }
    }
}
