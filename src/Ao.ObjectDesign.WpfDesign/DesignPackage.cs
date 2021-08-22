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
        public DesignPackage(BindingCreatorCollection<TDesignObject> bindingCreators, UIDesignMap uIDesinMap)
        {
            BindingCreators = bindingCreators;
            UIDesinMap = uIDesinMap;
        }

        public BindingCreatorCollection<TDesignObject> BindingCreators { get; }

        public UIDesignMap UIDesinMap { get; }

        public bool SkipWhenTrigger { get; set; }

        public virtual IEnumerable<IWithSourceBindingScope> CreateBindingScopes(IDesignPair<UIElement, TDesignObject> unit)
        {
            var skipWhenTrigger = SkipWhenTrigger;
            var state = CreateBindingCreatorState(unit);
            foreach (var item in BindingCreators)
            {
                if (item.IsAccept(unit, state))
                {
                    foreach (var scope in item.Create(unit, state))
                    {
                        yield return scope;
                    }
                    if (skipWhenTrigger)
                    {
                        break;
                    }
                }
            }
        }

        public abstract IBindingCreatorState CreateBindingCreatorState(IDesignPair<UIElement, TDesignObject> unit);
    }
}
