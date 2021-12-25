using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class DesignPackage<TDesignObject> : IDesignPackage<TDesignObject>
    {
        protected DesignPackage(BindingCreatorFactoryCollection<TDesignObject> bindingCreators, UIDesignMap uIDesinMap)
        {
            BindingCreators = bindingCreators;
            UIDesinMap = uIDesinMap;
        }

        public BindingCreatorFactoryCollection<TDesignObject> BindingCreators { get; }

        public UIDesignMap UIDesinMap { get; }

        public bool SkipWhenTrigger { get; set; }

        public virtual IEnumerable<IBindingCreatorFactory<TDesignObject>> GetBindingCreatorFactorys(IDesignPair<UIElement, TDesignObject> unit, IBindingCreatorState state)
        {
            var res = BindingCreators.FindBindingCreatorFactorys(unit, state);
            if (SkipWhenTrigger)
            {
                res = res.Take(1);
            }
            return res;
        }
        public virtual IEnumerable<IBindingCreator<TDesignObject>> GetBindingCreators(IDesignPair<UIElement, TDesignObject> unit, IBindingCreatorState state)
        {
            return GetBindingCreatorFactorys(unit, state)
                .SelectMany(x => x.Create(unit,state));
        }

        public abstract IBindingCreatorState CreateBindingCreatorState(IDesignPair<UIElement, TDesignObject> unit);
    }
}
