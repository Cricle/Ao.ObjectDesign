using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Ao.ObjectDesign.Wpf.Annotations;

namespace Ao.ObjectDesign.Wpf.Data
{
    public class BindingDrawing
    {
        private readonly static Type TypeDependencyObject = typeof(DependencyObject);

        public BindingDrawing(Type clrType)
            : this(clrType, null)
        {
        }
        public BindingDrawing(Type clrType, Type dependencyObjectType)
        {
            ClrType = clrType ?? throw new ArgumentNullException(nameof(clrType));
            DependencyObjectType = dependencyObjectType;
        }

        public Type ClrType { get; }

        public Type DependencyObjectType { get; }

        public IEnumerable<IBindingDrawingItem> Analysis()
        {
            return Analysis(ClrType, DependencyObjectType, null);
        }

        private IEnumerable<IBindingDrawingItem> Analysis(Type clrType, Type dependencyObjectType, string basePath)
        {
            var mapFor = clrType.GetCustomAttribute<MappingForAttribute>()?.Type ?? dependencyObjectType;
            if (mapFor is null)
            {
                throw new InvalidOperationException("Unknow mapping for type!");
            }
            var dep = DependencyObjectHelper.GetDependencyPropertyDescriptors(mapFor);
            var clrPropertys = clrType.GetProperties()
                .Where(CanMap);
            foreach (var item in clrPropertys)
            {
                var isUnfold = item.GetCustomAttribute<UnfoldMappingAttribute>() != null;
                if (isUnfold)
                {
                    var innerType = item.PropertyType;
                    var innerDp = innerType.GetCustomAttribute<DesignForAttribute>();
                    if (innerDp != null)
                    {
                        var mFor = item.GetCustomAttribute<BindForAttribute>();
                        var dpType = mFor?.DependencyObjectType ?? innerDp.Type;
                        if (TypeDependencyObject.IsAssignableFrom(dpType))
                        {
                            foreach (var innerItem in Analysis(innerType, dpType, item.Name))
                            {
                                yield return innerItem;
                            }
                        }
                    }
                }
                yield return MakeItem(clrType, dependencyObjectType, item, dep, basePath);
            }
        }
        protected virtual IBindingDrawingItem MakeItem(
            Type clrType,
            Type dependencyObjectType,
            PropertyInfo info,
            IReadOnlyDictionary<string, DependencyPropertyDescriptor> d,
            string basePath = null)
        {
            var mapFor = info.GetCustomAttribute<BindForAttribute>();
            var name = mapFor?.ForName ?? info.Name;
            var depName = name;
            var path = info.Name;
            if (!string.IsNullOrEmpty(basePath))
            {
                path = string.Concat(basePath, ".", path);
            }
            var item = new BindingDrawingItem
            {
                ClrType = clrType,
                DependencyObjectType = dependencyObjectType,
                PropertyInfo = info,
                Path = info.Name
            };
            IReadOnlyDictionary<string, DependencyPropertyDescriptor> map = d;

            if (mapFor?.DependencyObjectType != null)
            {
                map = DependencyObjectHelper.GetDependencyPropertyDescriptors(mapFor.DependencyObjectType);
            }

            Debug.Assert(map != null);
            Debug.Assert(!string.IsNullOrEmpty(name));

            if (info.PropertyType.IsClass &&
                info.PropertyType != typeof(string) &&
                info.GetCustomAttribute<NoInnerBindAttribute>() is null)
            {
                var designFor = info.PropertyType.GetCustomAttribute<DesignForAttribute>();
                if (designFor is null)
                {
                    return item;
                }
                var targetProperty = info.PropertyType.GetProperties().FirstOrDefault(x => x.GetCustomAttribute<PlatformTargetPropertyAttribute>() != null);
                if (targetProperty is null)
                {
                    return item;
                }
                item.Path = string.Concat(info.Name, ".", targetProperty.Name);
                if (d != map)
                {
                    map = DependencyObjectHelper.GetDependencyPropertyDescriptors(designFor.Type);
                }
            }

            if (map.TryGetValue(depName, out var desc))
            {
                item.DependencyProperty = desc.DependencyProperty;
                item.HasPropertyBind = true;
            }
            return item;
        }
        protected virtual bool CanMap(PropertyInfo info)
        {
            return info.CanWrite &&
                info.CanRead &&
                info.GetCustomAttribute<NotMappingPropertyAttribute>() == null;
        }
    }
}
