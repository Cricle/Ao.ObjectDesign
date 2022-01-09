using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using System.Collections;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class WpfItemsSceneController<TDesignObject> : ItemsSceneController<UIElement, TDesignObject, IWithSourceBindingScope, BindingExpressionBase>
    {
        protected WpfItemsSceneController(IDesignPackage<UIElement, TDesignObject, IWithSourceBindingScope> designMap, IList uiElements) : base(designMap, uiElements)
        {
        }
        protected override void DoEvent()
        {
            DoEventsHelper.DoEvents();
        }
        protected override BindingExpressionBase Bind(IWithSourceBindingScope scope, IDesignPair<UIElement, TDesignObject> unit)
        {
            return scope.Bind(unit.UI);
        }
    }
}
