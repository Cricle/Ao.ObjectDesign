using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class WpfMapSceneController<TDesignObject> : MapSceneController<UIElement, TDesignObject, IWithSourceBindingScope, BindingExpressionBase>
    {
        protected WpfMapSceneController(IDesignPackage<UIElement, TDesignObject, IWithSourceBindingScope> designMap) : base(designMap)
        {
        }
        protected override BindingExpressionBase Bind(IWithSourceBindingScope scope, IDesignPair<UIElement, TDesignObject> unit)
        {
            return scope.Bind(unit.UI);
        }
    }
}
