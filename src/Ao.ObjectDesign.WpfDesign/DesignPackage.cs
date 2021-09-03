using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.WpfDesign.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class DesignPackage<TDesignObject> : IDesignPackage<TDesignObject>
    {
        public DesignPackage(BindingCreatorFactoryCollection<TDesignObject> bindingCreators, UIDesignMap uIDesinMap)
        {
            BindingCreators = bindingCreators;
            UIDesinMap = uIDesinMap;
        }

        public BindingCreatorFactoryCollection<TDesignObject> BindingCreators { get; }

        public UIDesignMap UIDesinMap { get; }

        public bool SkipWhenTrigger { get; set; }

        public virtual IEnumerable<IBindingCreatorFactory<TDesignObject>> GetBindingCreatorFactorys(IDesignPair<UIElement, TDesignObject> unit, IBindingCreatorState state)
        {
            var skipWhenTrigger = SkipWhenTrigger;
            foreach (var item in BindingCreators.FindBindingCreatorFactorys(unit, state))
            {
                yield return item;
                if (skipWhenTrigger)
                {
                    break;
                }
            }
        }
        public virtual IEnumerable<IBindingCreator<TDesignObject>> GetBindingCreators(IDesignPair<UIElement, TDesignObject> unit, IBindingCreatorState state)
        {
            return GetBindingCreatorFactorys(unit, state)
                .SelectMany(x => x.Create(unit,state));
        }

        public abstract IBindingCreatorState CreateBindingCreatorState(IDesignPair<UIElement, TDesignObject> unit);
    }
}
