using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Data
{
    public struct BindingUnit : IEquatable<BindingUnit>
    {
        public readonly Binding Binding;

        public readonly DependencyProperty DependencyProperty;

        public BindingUnit(Binding binding, DependencyProperty dependencyProperty)
        {
            Binding = binding ?? throw new ArgumentNullException(nameof(binding));
            DependencyProperty = dependencyProperty ?? throw new ArgumentNullException(nameof(dependencyProperty));
        }

        public override int GetHashCode()
        {
            if (Binding is null || DependencyProperty is null)
            {
                return 0;
            }
            return Binding.GetHashCode() ^ DependencyProperty.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is BindingUnit unit)
            {
                return Equals(unit);
            }
            return false;
        }

        public bool Equals(BindingUnit other)
        {
            return other.DependencyProperty == DependencyProperty &&
                other.Binding == Binding;
        }
        public BindingUnit TransferProperty(DependencyProperty prop)
        {
            return new BindingUnit(Binding, prop);
        }
    }
}
