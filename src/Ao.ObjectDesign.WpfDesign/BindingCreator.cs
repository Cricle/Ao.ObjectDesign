using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using System.Collections.Generic;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class BindingCreator<TDesignObject> : IBindingCreator<TDesignObject>
    {
        protected BindingCreator(IDesignPair<UIElement, TDesignObject> designUnit, IBindingCreatorState state)
        {
            DesignUnit = designUnit;
            State = state;
        }

        public IDesignPair<UIElement, TDesignObject> DesignUnit { get; }

        public IBindingCreatorState State { get; }

        public virtual IEnumerable<IWithSourceBindingScope> BindingScopes => CreateBindingScopes();

        public virtual void Attack()
        {
            
        }

        public virtual void Detack()
        {
        }

        protected abstract IEnumerable<IWithSourceBindingScope> CreateBindingScopes();
    }
}
