using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using System.Collections.Generic;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class WpfBindingCreator<TDesignObject> : BindingCreator<UIElement, TDesignObject, IWithSourceBindingScope>, IWpfBindingCreator<TDesignObject>
    {
        protected WpfBindingCreator(IDesignPair<UIElement, TDesignObject> designUnit, IBindingCreatorState state) : base(designUnit, state)
        {
        }
    }
}
