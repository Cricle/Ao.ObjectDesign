using System;
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
            Binding = binding;
            DependencyProperty = dependencyProperty;
        }
        public override int GetHashCode()
        {
            return Binding.GetHashCode() + DependencyProperty.GetHashCode();
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
            return other.DependencyProperty == DependencyProperty&&
                other.Binding==Binding;
        }
    }
}
