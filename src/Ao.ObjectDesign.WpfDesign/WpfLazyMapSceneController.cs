using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class WpfLazyMapSceneController<TDesignObject> : LazyMapSceneController<UIElement, TDesignObject, IWithSourceBindingScope, BindingExpressionBase>
    {
        protected WpfLazyMapSceneController(IDesignPackage<UIElement, TDesignObject, IWithSourceBindingScope> designMap) : base(designMap)
        {
        }
        protected override BindingExpressionBase Bind(IWithSourceBindingScope scope, IDesignPair<UIElement, TDesignObject> unit)
        {
            return scope.Bind(unit.UI);
        }
        protected override void DoEvent()
        {
            DoEventsHelper.DoEvents();
        }
    }
}
