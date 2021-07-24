using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Data
{
    public static class BindingSetExtensions
    {
        public static IReadOnlyList<BindingExpressionBase> SetBindings(this DependencyObject @object, IEnumerable<BindingUnit> bindings)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            if (bindings is null)
            {
                throw new ArgumentNullException(nameof(bindings));
            }
            var bindingResults = new List<BindingExpressionBase>();
            foreach (BindingUnit item in bindings)
            {
                bindingResults.Add(BindingOperations.SetBinding(@object, item.DependencyProperty, item.Binding));
            }
            return bindingResults;
        }
    }
}
