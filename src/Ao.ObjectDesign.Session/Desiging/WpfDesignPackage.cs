using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.WpfDesign;
using System.Windows;

namespace Ao.ObjectDesign.Session.Desiging
{
    public class WpfDesignPackage<TSetting> : DesignPackage<TSetting>
    {
        public WpfDesignPackage(UIDesignMap uIDesinMap, IBindingCreatorStateCreator<TSetting> bindingCreatorStateCreator)
            : this(new BindingCreatorFactoryCollection<TSetting>(), uIDesinMap, bindingCreatorStateCreator)
        {
        }
        public WpfDesignPackage(BindingCreatorFactoryCollection<TSetting> bindingCreators, UIDesignMap uIDesinMap, IBindingCreatorStateCreator<TSetting> bindingCreatorStateCreator)
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
