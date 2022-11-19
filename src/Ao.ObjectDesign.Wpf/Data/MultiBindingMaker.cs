using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Data
{
    [DebuggerDisplay("Property = {DependencyProperty}")]
    public class MultiBindingMaker : BindingMakerBase<MultiBinding>
    {
        public MultiBindingMaker(DependencyProperty dependencyProperty)
            : base(dependencyProperty)
        {
        }

        public MultiBindingMaker(DependencyProperty dependencyProperty, Action<MultiBinding> actions)
            : base(dependencyProperty, actions)
        {
        }

        public override IBindingScope Build()
        {
            return new MultiBindingScope(this, Actions);
        }

        public override IBindingMaker<MultiBinding> Clone()
        {
            return new MultiBindingMaker(DependencyProperty, Actions);
        }
    }

}
