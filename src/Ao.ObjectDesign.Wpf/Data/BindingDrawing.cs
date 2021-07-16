using Ao.ObjectDesign.Wpf.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Data
{
    public class BindingDrawing
    {

        /* 项目“Ao.ObjectDesign.Wpf (net461)”的未合并的更改
        在此之前:
                private readonly static Type TypeDependencyObject = typeof(DependencyObject);
        在此之后:
                private static readonly Type TypeDependencyObject = typeof(DependencyObject);
        */

        /* 项目“Ao.ObjectDesign.Wpf (net5.0-windows)”的未合并的更改
        在此之前:
                private readonly static Type TypeDependencyObject = typeof(DependencyObject);
        在此之后:
                private static readonly Type TypeDependencyObject = typeof(DependencyObject);
        */
        private static readonly Type TypeDependencyObject = typeof(DependencyObject);

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
            Type mapFor = clrType.GetCustomAttribute<MappingForAttribute>()?.Type ?? dependencyObjectType;
            if (mapFor is null)
            {
                throw new InvalidOperationException("Unknow mapping for type!");
            }

            IReadOnlyDictionary<string, DependencyPropertyDescriptor> dep = DependencyObjectHelper.GetDependencyPropertyDescriptors(mapFor);
            IEnumerable<PropertyInfo> clrPropertys = clrType.GetProperties()
                .Where(CanMap);
            foreach (PropertyInfo item in clrPropertys)
            {
                bool isUnfold = item.GetCustomAttribute<UnfoldMappingAttribute>() != null;
                if (isUnfold)
                {
                    Type innerType = item.PropertyType;
                    DesignForAttribute innerDp = innerType.GetCustomAttribute<DesignForAttribute>();
                    if (innerDp != null)
                    {
                        BindForAttribute mFor = item.GetCustomAttribute<BindForAttribute>();
                        Type dpType = mFor?.DependencyObjectType ?? innerDp.Type;
                        if (TypeDependencyObject.IsAssignableFrom(dpType))
                        {
                            foreach (IBindingDrawingItem innerItem in Analysis(innerType, dpType, item.Name))
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
            BindForAttribute mapFor = info.GetCustomAttribute<BindForAttribute>();
            string name = mapFor?.ForName ?? info.Name;
            string depName = name;
            string path = info.Name;
            if (!string.IsNullOrEmpty(basePath))
            {
                path = string.Concat(basePath, ".", path);
            }
            BindingDrawingItem item = new BindingDrawingItem
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
                DesignForAttribute designFor = info.PropertyType.GetCustomAttribute<DesignForAttribute>();
                if (designFor is null)
                {
                    return item;
                }
                PropertyInfo targetProperty = info.PropertyType.GetProperties().FirstOrDefault(x => x.GetCustomAttribute<PlatformTargetPropertyAttribute>() != null);
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

            if (map.TryGetValue(depName, out DependencyPropertyDescriptor desc))
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
