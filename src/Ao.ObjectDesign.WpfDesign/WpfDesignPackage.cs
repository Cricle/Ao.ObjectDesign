using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Wpf.Data;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class WpfDesignPackage<TDesignObject> : DesignPackage<UIElement, TDesignObject, IWithSourceBindingScope>,IWpfDesignPackage<TDesignObject>
    {
        protected WpfDesignPackage(BindingCreatorFactoryCollection<UIElement, TDesignObject, IWithSourceBindingScope> bindingCreators, UIDesignMap uIDesinMap) : base(bindingCreators, uIDesinMap)
        {
        }
    }
}
