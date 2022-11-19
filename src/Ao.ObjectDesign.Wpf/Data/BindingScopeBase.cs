using System;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Data
{
    public abstract class BindingScopeBase<T> : IBindingScope
        where T : BindingBase
    {
        public BindingScopeBase(IBindingMaker<T> creator, Action<T> bindingAction)
        {
            if (bindingAction is null)
            {
                throw new ArgumentNullException(nameof(bindingAction));
            }

            Creator = creator ?? throw new ArgumentNullException(nameof(creator));
            Actions = (Action<T>)bindingAction.Clone();
        }

        public IBindingMaker<T> Creator { get; }

        public Action<T> Actions { get; }

        public DependencyProperty DependencyProperty => Creator.DependencyProperty;

        public Func<DependencyObject> TargetFactory { get; set; }

        public BindingExpressionBase Bind(DependencyObject @object, object source)
        {
            var bd = CreateBinding(source);
            var target = TargetFactory?.Invoke() ?? @object;
            if (target is FrameworkElement fe)
            {
                return fe.SetBinding(Creator.DependencyProperty, bd);
            }
            else if (target is FrameworkContentElement fce)
            {
                return fce.SetBinding(Creator.DependencyProperty, bd);
            }
            return BindingOperations.SetBinding(target, Creator.DependencyProperty, bd);
        }

        public BindingBase CreateBinding(object source)
        {
            var bd = CreateEmptyBinding(source);
            Actions(bd);
            return bd;
        }
        protected abstract T CreateEmptyBinding(object source);
    }

}
