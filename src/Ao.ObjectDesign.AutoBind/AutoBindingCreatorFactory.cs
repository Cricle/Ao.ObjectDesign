using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.BindingCreators;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.WpfDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ao.ObjectDesign.AutoBind
{
    public class AutoBindingCreatorFactory<TUI,TSetting,TDesignObject> : BindingCreatorFactory<TSetting>, IBindingCreatorFactory<UIElement, TSetting, IWithSourceBindingScope>
        where TUI : DependencyObject
    {

        public Func<IDesignPair<UIElement, TSetting>, IBindingCreatorState,IBindingCreator<UIElement, TSetting, IWithSourceBindingScope>> ParentCreator { get; set; }

        public override bool IsAccept(IDesignPair<UIElement, TSetting> unit, IBindingCreatorState state)
        {
            return unit.DesigningObject.GetType() == typeof(TDesignObject);
        }

        protected override IEnumerable<IWpfBindingCreator<TSetting>> CreateWpfCreators(IDesignPair<UIElement, TSetting> unit, IBindingCreatorState state)
        {
            yield return new AutoBindingCreator<TUI, TSetting, TDesignObject>(unit, state)
            {
                Parent = ParentCreator?.Invoke(unit, state)
            };
        }
    }
}
