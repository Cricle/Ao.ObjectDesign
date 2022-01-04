using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using System.Windows;

namespace Ao.ObjectDesign.Session.Desiging
{
    public class WpfDesignPackage<TSetting> : DesignPackage<UIElement, TSetting, IWithSourceBindingScope>
    {
        public WpfDesignPackage(UIDesignMap uIDesinMap, IBindingCreatorStateCreator<TSetting> bindingCreatorStateCreator)
            : this(new BindingCreatorFactoryCollection<UIElement, TSetting, IWithSourceBindingScope>(), uIDesinMap, bindingCreatorStateCreator)
        {
        }
        public WpfDesignPackage(BindingCreatorFactoryCollection<UIElement, TSetting, IWithSourceBindingScope> bindingCreators, UIDesignMap uIDesinMap, IBindingCreatorStateCreator<TSetting> bindingCreatorStateCreator)
            : base(bindingCreators, uIDesinMap)
        {
            BindingCreatorStateCreator = bindingCreatorStateCreator;
        }

        public IBindingCreatorStateCreator<TSetting> BindingCreatorStateCreator { get; }

        public override IBindingCreatorState CreateBindingCreatorState(IDesignPair<UIElement, TSetting> unit)
        {
            return BindingCreatorStateCreator?.GetBindingCreatorState(unit);
        }
    }
}
