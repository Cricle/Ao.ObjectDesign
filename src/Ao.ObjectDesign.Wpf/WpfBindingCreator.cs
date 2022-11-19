using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Data;
using System.Windows;

namespace Ao.ObjectDesign.Wpf
{
    public abstract class WpfBindingCreator<TDesignObject> : BindingCreator<UIElement, TDesignObject, IWithSourceBindingScope>, IWpfBindingCreator<TDesignObject>
    {
        protected WpfBindingCreator(IDesignPair<UIElement, TDesignObject> designUnit, IBindingCreatorState state) : base(designUnit, state)
        {
        }
    }
}
