using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Diagnostics;

namespace Ao.ObjectDesign.Wpf
{
    internal static class DependencyObjectHelper
    {
        private static readonly Attribute[] dependencyAttributes = new Attribute[] { new PropertyFilterAttribute(PropertyFilterOptions.All) };

        private static readonly ConcurrentDictionary<Type, Dictionary<string, DependencyPropertyDescriptor>> dependencyProperties =
            new ConcurrentDictionary<Type, Dictionary<string, DependencyPropertyDescriptor>>();

        public static IEnumerable<PropertyDescriptor> GetPropertyDescriptors(Type type)
        {
            Debug.Assert(type != null);
            Debug.Assert(typeof(DependencyObject).IsAssignableFrom(type));

            return TypeDescriptor.GetProperties(type, dependencyAttributes)
                .OfType<PropertyDescriptor>();
        }
        public static IReadOnlyDictionary<string, DependencyPropertyDescriptor> GetDependencyPropertyDescriptors(Type type)
        {
            Debug.Assert(type != null);
            Debug.Assert(typeof(DependencyObject).IsAssignableFrom(type));

            return dependencyProperties.GetOrAdd(type,
                x => GetPropertyDescriptors(type)
                .Select(t => DependencyPropertyDescriptor.FromProperty(t))
                .Where(t => t != null).ToDictionary(y => y.Name));
        }
        public static IEnumerable<DependencyProperty> GetDependencyProperties(Type type)
        {
            Debug.Assert(type != null);
            Debug.Assert(typeof(DependencyObject).IsAssignableFrom(type));

            return GetDependencyPropertyDescriptors(type)
                .Values.Select(x => x.DependencyProperty);
        }
        public static bool IsDependencyProperty(Type type, string name)
        {
            Debug.Assert(type != null);
            Debug.Assert(typeof(DependencyObject).IsAssignableFrom(type));

            Debug.Assert(!string.IsNullOrEmpty(name));

            var descMap = GetDependencyPropertyDescriptors(type);
            return descMap.ContainsKey(name);
        }
    }
}
