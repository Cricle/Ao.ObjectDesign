using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Data
{
    [DebuggerDisplay("Property = {DependencyProperty}")]
    public class BindingCreator : ICloneable, IBindingCreator
    {
        private static Action<Binding> NothingAction => _ => { };

        private Action<Binding> actions;

        public BindingCreator(DependencyProperty dependencyProperty)
        {
            DependencyProperty = dependencyProperty ?? throw new ArgumentNullException(nameof(dependencyProperty));
            actions = NothingAction;
        }
        public BindingCreator(DependencyProperty dependencyProperty, Action<Binding> actions)
        {
            DependencyProperty = dependencyProperty ?? throw new ArgumentNullException(nameof(dependencyProperty));
            this.actions = actions ?? throw new ArgumentNullException(nameof(actions));
        }

        public DependencyProperty DependencyProperty { get; }

        public Action<Binding> Actions => actions;

        public IBindingCreator Add(Action<Binding> doAction)
        {
            if (doAction is null)
            {
                throw new ArgumentNullException(nameof(doAction));
            }

            Debug.Assert(Actions != null);
            actions += doAction;
            return this;
        }
        public IBindingScope Build()
        {
            return new BindingScope(this);
        }
        public IBindingCreator Clone()
        {
            return new BindingCreator(DependencyProperty, (Action<Binding>)actions.Clone());
        }
        object ICloneable.Clone()
        {
            return Clone();
        }

        class BindingScope : IBindingScope
        {
            public BindingScope(BindingCreator creator)
            {
                Debug.Assert(creator.DependencyProperty != null);
                Debug.Assert(creator.actions != null);
                Creator = creator;
                Actions = (Action<Binding>)creator.actions.Clone();
            }

            public BindingCreator Creator { get; }

            public Action<Binding> Actions { get; }

            public BindingExpressionBase Bind(DependencyObject @object, object source)
            {
                var bd = new Binding();
                Actions(bd);
                if (source != null)
                {
                    bd.Source = source;
                }
                if (@object is FrameworkElement fe)
                {
                   return fe.SetBinding(Creator.DependencyProperty, bd);
                }
                else if (@object is FrameworkContentElement fce)
                {
                    return fce.SetBinding(Creator.DependencyProperty, bd);
                }
                return BindingOperations.SetBinding(@object, Creator.DependencyProperty, bd);
            }
        }
    }

}
