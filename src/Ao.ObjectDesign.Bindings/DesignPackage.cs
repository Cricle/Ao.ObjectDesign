using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign.Bindings
{
    public abstract class DesignPackage<TUI, TDesignObject, TBindingScope> : IDesignPackage<TUI, TDesignObject, TBindingScope>
    {
        protected DesignPackage(BindingCreatorFactoryCollection<TUI, TDesignObject, TBindingScope> bindingCreators, UIDesignMap uIDesinMap)
        {
            BindingCreators = bindingCreators;
            UIDesinMap = uIDesinMap;
        }

        public BindingCreatorFactoryCollection<TUI, TDesignObject, TBindingScope> BindingCreators { get; }

        public UIDesignMap UIDesinMap { get; }

        public bool SkipWhenTrigger { get; set; }

        public virtual IEnumerable<IBindingCreatorFactory<TUI, TDesignObject, TBindingScope>> GetBindingCreatorFactorys(IDesignPair<TUI, TDesignObject> unit, IBindingCreatorState state)
        {
            var res = BindingCreators.FindBindingCreatorFactorys(unit, state);
            if (SkipWhenTrigger)
            {
                res = res.Take(1);
            }
            return res;
        }
        public virtual IEnumerable<IBindingCreator<TUI, TDesignObject, TBindingScope>> GetBindingCreators(IDesignPair<TUI, TDesignObject> unit, IBindingCreatorState state)
        {
            return GetBindingCreatorFactorys(unit, state)
                .SelectMany(x => x.Create(unit, state));
        }

        public abstract IBindingCreatorState CreateBindingCreatorState(IDesignPair<TUI, TDesignObject> unit);
    }
}
