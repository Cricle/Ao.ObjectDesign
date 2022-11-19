using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.WpfDesign;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Ao.ObjectDesign.Session.Wpf.BindingCreators
{
    public class DelegateBindingCreatorFactory<TDesignObject> : BindingCreatorFactory<TDesignObject>
    {
        public DelegateBindingCreatorFactory(Type settingType, Func<IDesignPair<UIElement, TDesignObject>, IBindingCreatorState,
            IEnumerable<IWpfBindingCreator<TDesignObject>>> funcCreateWpfCreators)
        {
            if (settingType is null)
            {
                throw new ArgumentNullException(nameof(settingType));
            }

            FuncIsAccept = (a, b) => a.DesigningObject.GetType() == settingType;
            FuncCreateWpfCreators = funcCreateWpfCreators ?? throw new ArgumentNullException(nameof(funcCreateWpfCreators));
        }

        public DelegateBindingCreatorFactory(Func<IDesignPair<UIElement, TDesignObject>, IBindingCreatorState, bool> funcIsAccept, Func<IDesignPair<UIElement, TDesignObject>, IBindingCreatorState, 
            IEnumerable<IWpfBindingCreator<TDesignObject>>> funcCreateWpfCreators)
        {
            FuncIsAccept = funcIsAccept ?? throw new ArgumentNullException(nameof(funcIsAccept));
            FuncCreateWpfCreators = funcCreateWpfCreators ?? throw new ArgumentNullException(nameof(funcCreateWpfCreators));
        }

        public Func<IDesignPair<UIElement, TDesignObject>, IBindingCreatorState, bool> FuncIsAccept { get; }
        
        public Func<IDesignPair<UIElement, TDesignObject>, IBindingCreatorState, IEnumerable<IWpfBindingCreator<TDesignObject>>> FuncCreateWpfCreators { get; }

        public override bool IsAccept(IDesignPair<UIElement, TDesignObject> unit, IBindingCreatorState state)
        {
            return FuncIsAccept(unit, state);
        }

        protected override IEnumerable<IWpfBindingCreator<TDesignObject>> CreateWpfCreators(IDesignPair<UIElement, TDesignObject> unit, IBindingCreatorState state)
        {
            return FuncCreateWpfCreators(unit, state);
        }
    }

}
