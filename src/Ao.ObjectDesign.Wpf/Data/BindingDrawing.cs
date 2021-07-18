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
    public class BindingDrawing : IBindingDrawing
    {
        private static readonly Type TypeDependencyObject = typeof(DependencyObject);

        public BindingDrawing(Type clrType)
            : this(clrType, AttributeBindForGetter.Instance)
        {
        }
        public BindingDrawing(Type clrType, IBindForGetter bindForGetter)
            : this(clrType, null, bindForGetter)
        {
        }
        public BindingDrawing(Type clrType, Type dependencyObjectType, IBindForGetter bindForGetter)
        {
            ClrType = clrType ?? throw new ArgumentNullException(nameof(clrType));
            DependencyObjectType = dependencyObjectType;
            BindForGetter = bindForGetter ?? throw new ArgumentNullException(nameof(bindForGetter));
        }

        public Type ClrType { get; }

        public Type DependencyObjectType { get; }

        public IBindForGetter BindForGetter { get; }

        public IEnumerable<IBindingDrawingItem> Analysis()
        {
            return Analysis(ClrType, DependencyObjectType, null);
        }
        private IEnumerable<IBindingDrawingItem> Analysis(Type clrType, Type dependencyObjectType, string basePath)
        {
            if (clrType is null)
            {
                throw new ArgumentNullException(nameof(clrType));
            }

            Type mapFor = clrType.GetCustomAttribute<MappingForAttribute>()?.Type ?? dependencyObjectType;
            if (mapFor is null)
            {
                throw new InvalidOperationException($"Unknow mapping for type from root type {clrType.FullName}!");
            }

            return Analysis(clrType, mapFor, dependencyObjectType, basePath);
        }
        private IEnumerable<IBindingDrawingItem> Analysis(Type clrType, Type mappingForType, Type dependencyObjectType, string basePath)
        {
            if (clrType is null)
            {
                throw new ArgumentNullException(nameof(clrType));
            }

            if (mappingForType is null)
            {
                throw new ArgumentNullException(nameof(mappingForType));
            }


            IReadOnlyDictionary<string, DependencyPropertyDescriptor> dep = GetDependencyPropertyDescriptorMap(mappingForType);
            IEnumerable<PropertyInfo> clrPropertys = clrType.GetProperties().Where(CanMap);
            foreach (PropertyInfo item in clrPropertys)
            {
                bool isUnfold = item.GetCustomAttribute<UnfoldMappingAttribute>() != null;
                if (isUnfold)
                {
                    Type innerType = item.PropertyType;
                    DesignForAttribute innerDp = innerType.GetCustomAttribute<DesignForAttribute>();
                    if (innerDp != null)
                    {
                        BindForAttribute mFor = BindForGetter.Get(item);
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
            IReadOnlyDictionary<string, DependencyPropertyDescriptor> descriptorMap,
            string basePath = null)
        {
            BindForAttribute mapFor = BindForGetter.Get(info);
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
            IReadOnlyDictionary<string, DependencyPropertyDescriptor> map = descriptorMap;

            if (mapFor?.DependencyObjectType != null)
            {
                map = GetDependencyPropertyDescriptorMap(mapFor.DependencyObjectType);
            }

            Debug.Assert(map != null);
            Debug.Assert(!string.IsNullOrEmpty(name));

            if (CanStepInnerBind(info))
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
                if (descriptorMap != map)
                {
                    map = GetDependencyPropertyDescriptorMap(designFor.Type);
                }
            }

            if (map.TryGetValue(depName, out DependencyPropertyDescriptor desc))
            {
                item.DependencyProperty = desc.DependencyProperty;
                item.HasPropertyBind = true;
            }
            return item;
        }
        protected virtual IReadOnlyDictionary<string, DependencyPropertyDescriptor> GetDependencyPropertyDescriptorMap(Type type)
        {
            return DependencyObjectHelper.GetDependencyPropertyDescriptors(type);
        }
        protected virtual bool CanStepInnerBind(PropertyInfo info)
        {
            return info.PropertyType.IsClass &&
                info.PropertyType != typeof(string) &&
                info.GetCustomAttribute<NoInnerBindAttribute>() is null;
        }
        protected virtual bool CanMap(PropertyInfo info)
        {
            return info.CanWrite &&
                info.CanRead &&
                info.GetCustomAttribute<NotMappingPropertyAttribute>() == null;
        }
    }
}
