using System;
using System.Diagnostics;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Data
{
    public abstract class BindingMakerBase<T> : ICloneable, IBindingMaker<T>
    {
        private static Action<T> NothingAction => _ => { };

        private Action<T> actions;


        protected BindingMakerBase(DependencyProperty dependencyProperty)
        {
            DependencyProperty = dependencyProperty ?? throw new ArgumentNullException(nameof(dependencyProperty));
            actions = NothingAction;
        }
        protected BindingMakerBase(DependencyProperty dependencyProperty, Action<T> actions)
        {
            DependencyProperty = dependencyProperty ?? throw new ArgumentNullException(nameof(dependencyProperty));
            this.actions = actions ?? throw new ArgumentNullException(nameof(actions));
        }

        public DependencyProperty DependencyProperty { get; }

        public Action<T> Actions => (Action<T>)actions.Clone();

        public IBindingMaker<T> Add(Action<T> doAction)
        {
            if (doAction is null)
            {
                throw new ArgumentNullException(nameof(doAction));
            }

            Debug.Assert(Actions != null);
            actions += doAction;
            return this;
        }
        public abstract IBindingScope Build();

        public abstract IBindingMaker<T> Clone();

        object ICloneable.Clone()
        {
            return Clone();
        }

    }

}
