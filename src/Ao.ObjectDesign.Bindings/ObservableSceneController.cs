using Ao.ObjectDesign.Designing.Level;
using System.Collections.ObjectModel;

namespace Ao.ObjectDesign.Bindings
{
    public abstract class ObservableSceneController<TUI, TDesignObject, TBindingScope, TExpression> : LazyMapSceneController<TUI, TDesignObject, TBindingScope, TExpression>
    {
        protected ObservableSceneController(IDesignPackage<TUI, TDesignObject, TBindingScope> designMap)
            : this(designMap, new SilentObservableCollection<object>())
        {

        }

        protected ObservableSceneController(IDesignPackage<TUI, TDesignObject, TBindingScope> designMap, SilentObservableCollection<object> items)
            : base(designMap)
        {
            Items = items;
        }

        public SilentObservableCollection<object> Items { get; }

        protected override void AddToContainer(IDesignPair<TUI, TDesignObject> unit)
        {
            Items.Add(unit.UI);
        }

        protected override void RemoveFromContainer(IDesignPair<TUI, TDesignObject> unit)
        {
            Items.Remove(unit.UI);
        }
    }
}
