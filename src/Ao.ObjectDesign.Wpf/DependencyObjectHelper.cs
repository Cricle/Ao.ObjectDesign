using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Ao.ObjectDesign.Wpf
{
    public static class DependencyObjectHelper
    {
        private static readonly Attribute[] dependencyAttributes = new Attribute[] { new PropertyFilterAttribute(PropertyFilterOptions.All) };

        private static readonly ConcurrentDictionary<Type, Dictionary<string, DependencyPropertyDescriptor>> dependencyProperties =
            new ConcurrentDictionary<Type, Dictionary<string, DependencyPropertyDescriptor>>();

        private static void ThrowIfTypeNotBaseOnDependencyObject(Type type)
        {
            if (!typeof(DependencyObject).IsAssignableFrom(type))
            {
                throw new InvalidCastException($"Type {type} can't case to DependencyObject");
            }
        }
        public static IEnumerable<PropertyDescriptor> GetPropertyDescriptors(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            ThrowIfTypeNotBaseOnDependencyObject(type);

            IEnumerable<PropertyDescriptor> ret = Enumerable.Empty<PropertyDescriptor>();
            Type depObjType = typeof(DependencyObject);
            Type t = type;
            while (t != depObjType)
            {
                Type e = t;
                ret = ret.Concat(TypeDescriptor.GetProperties(e, dependencyAttributes)
                    .OfType<PropertyDescriptor>()
                    .Where(x => x.ComponentType == e));
                t = t.BaseType;
            }

            return ret;
        }
        public static IReadOnlyDictionary<string, DependencyPropertyDescriptor> GetDependencyPropertyDescriptors(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            ThrowIfTypeNotBaseOnDependencyObject(type);

            return dependencyProperties.GetOrAdd(type,
                x => GetPropertyDescriptors(type)
                .Select(t => DependencyPropertyDescriptor.FromProperty(t))
                .Where(t => t != null).ToDictionary(y => y.Name));
        }
        public static IEnumerable<DependencyProperty> GetDependencyProperties(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            ThrowIfTypeNotBaseOnDependencyObject(type);

            return GetDependencyPropertyDescriptors(type)
                .Values.Select(x => x.DependencyProperty);
        }
        public static bool IsDependencyProperty(Type type, string name)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"“{nameof(name)}”不能为 null 或空。", nameof(name));
            }

            ThrowIfTypeNotBaseOnDependencyObject(type);

            IReadOnlyDictionary<string, DependencyPropertyDescriptor> descMap = GetDependencyPropertyDescriptors(type);
            return descMap.ContainsKey(name);
        }
    }
}
