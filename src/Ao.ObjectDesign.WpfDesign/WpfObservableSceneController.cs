using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Data;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class WpfObservableSceneController<TDesignObject> : ObservableSceneController<UIElement, TDesignObject, IWithSourceBindingScope, BindingExpressionBase>
    {
        protected WpfObservableSceneController(IDesignPackage<UIElement, TDesignObject, IWithSourceBindingScope> designMap) : base(designMap)
        {
        }

        protected WpfObservableSceneController(IDesignPackage<UIElement, TDesignObject, IWithSourceBindingScope> designMap, SilentObservableCollection<object> items) : base(designMap, items)
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
