using Ao.ObjectDesign.Designing.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Ao.ObjectDesign.Designing.Data
{
    public abstract class BindingDrawing<TDrawingItem, TDescriptor> : IBindingDrawing<TDrawingItem>
        where TDrawingItem : IBindingDrawingItem
        where TDescriptor: PropertyDescriptor
    {
        protected BindingDrawing(Type clrType)
            : this(clrType, AttributeBindForGetter.Instance)
        {
        }
        protected BindingDrawing(Type clrType, IBindForGetter bindForGetter)
            : this(clrType, null, bindForGetter)
        {
        }
        protected BindingDrawing(Type clrType, Type dependencyObjectType, IBindForGetter bindForGetter)
        {
            ClrType = clrType ?? throw new ArgumentNullException(nameof(clrType));
            DependencyObjectType = dependencyObjectType;
            BindForGetter = bindForGetter ?? throw new ArgumentNullException(nameof(bindForGetter));
        }

        public Type ClrType { get; }

        public Type DependencyObjectType { get; }

        public IBindForGetter BindForGetter { get; }

        public IEnumerable<TDrawingItem> Analysis()
        {
            return Analysis(ClrType, DependencyObjectType, null);
        }
        private IEnumerable<TDrawingItem> Analysis(Type clrType, Type dependencyObjectType, string basePath)
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
        private IEnumerable<TDrawingItem> Analysis(Type clrType, Type mappingForType, Type dependencyObjectType, string basePath)
        {
            if (clrType is null)
            {
                throw new ArgumentNullException(nameof(clrType));
            }

            if (mappingForType is null)
            {
                throw new ArgumentNullException(nameof(mappingForType));
            }


            IReadOnlyDictionary<string, TDescriptor> dep = GetDependencyPropertyDescriptorMap(mappingForType);
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
                        if (CanStepAnalysis(item,dpType))
                        {
                            foreach (TDrawingItem innerItem in Analysis(innerType, dpType, item.Name))
                            {
                                yield return innerItem;
                            }
                        }
                    }
                }
                yield return MakeItem(clrType, dependencyObjectType, item, dep, basePath);
            }
        }
        protected virtual TDrawingItem MakeItem(
            Type clrType,
            Type dependencyObjectType,
            PropertyInfo info,
            IReadOnlyDictionary<string, TDescriptor> descriptorMap,
            string basePath = null)
        {
            BindForAttribute mapFor = BindForGetter.Get(info);
            string name = mapFor?.PropertyName ?? info.Name;
            string depName = name;
            string path = info.Name;
            if (!string.IsNullOrEmpty(basePath))
            {
                path = string.Concat(basePath, ".", path);
            }
            IReadOnlyDictionary<string, TDescriptor> map = descriptorMap;

            if (mapFor?.DependencyObjectType != null)
            {
                map = GetDependencyPropertyDescriptorMap(mapFor.DependencyObjectType);
            }

            Debug.Assert(map != null);
            Debug.Assert(!string.IsNullOrEmpty(name));

            BindingDrawingItem drawingItem = new BindingDrawingItem
            {
                ClrType = clrType,
                DependencyObjectType = dependencyObjectType,
                PropertyInfo = info,
                Path = info.Name,
                ConverterParamter = mapFor?.ConverterParamer,
                ConverterType = mapFor?.ConverterType,
            };

            if (CanStepInnerBind(info))
            {
                DesignForAttribute designFor = info.PropertyType.GetCustomAttribute<DesignForAttribute>();
                if (designFor is null)
                {
                    return CreateDrawingItem(drawingItem);
                }
                PropertyInfo targetProperty = info.PropertyType.GetProperties().FirstOrDefault(x => x.GetCustomAttribute<PlatformTargetPropertyAttribute>() != null);
                if (targetProperty is null)
                {
                    return CreateDrawingItem(drawingItem);
                }
                drawingItem.Path = string.Concat(info.Name, ".", targetProperty.Name);
                if (descriptorMap != map)
                {
                    map = GetDependencyPropertyDescriptorMap(designFor.Type);
                }
            }
            if (map.TryGetValue(depName, out TDescriptor desc))
            {
                drawingItem.HasPropertyBind = true;
                var item = CreateDrawingItem(drawingItem);
                FillItem(item, desc);
                return item;
            }
            return CreateDrawingItem(drawingItem);
        }
        protected abstract bool CanStepAnalysis(PropertyInfo propertyInfo, Type type);
        protected abstract TDrawingItem CreateDrawingItem(BindingDrawingItem drawingItem);
        protected abstract void FillItem(TDrawingItem item, TDescriptor descriptor);
        protected abstract IReadOnlyDictionary<string, TDescriptor> GetDependencyPropertyDescriptorMap(Type type);
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
