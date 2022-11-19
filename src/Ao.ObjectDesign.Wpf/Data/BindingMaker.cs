using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Data
{
    [DebuggerDisplay("Property = {DependencyProperty}")]
    public class BindingMaker : BindingMakerBase<Binding>, IBindingMaker
    {
        public BindingMaker(DependencyProperty dependencyProperty)
            : base(dependencyProperty)
        {
        }

        public BindingMaker(DependencyProperty dependencyProperty, Action<Binding> actions)
            : base(dependencyProperty, actions)
        {

        }

        public override IBindingScope Build()
        {
            return new BindingScope(this, Actions);
        }

        public override IBindingMaker<Binding> Clone()
        {
            return new BindingMaker(DependencyProperty, Actions);
        }

        IBindingMaker IBindingMaker.Add(Action<Binding> doAction)
        {
            return (IBindingMaker)Add(doAction);
        }
    }

}
