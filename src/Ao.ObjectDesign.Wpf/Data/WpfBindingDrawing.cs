using Ao.ObjectDesign.Designing.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;

namespace Ao.ObjectDesign.Data
{
    public class WpfBindingDrawing : BindingDrawing<IWpfBindingDrawingItem, DependencyPropertyDescriptor>
    {
        private static readonly Type TypeDependencyObject = typeof(DependencyObject);

        public WpfBindingDrawing(Type clrType) : base(clrType)
        {
        }

        public WpfBindingDrawing(Type clrType, IBindForGetter bindForGetter) : base(clrType, bindForGetter)
        {
        }

        public WpfBindingDrawing(Type clrType, Type dependencyObjectType, IBindForGetter bindForGetter) : base(clrType, dependencyObjectType, bindForGetter)
        {
        }

        protected override bool CanStepAnalysis(PropertyInfo propertyInfo, Type type)
        {
            return TypeDependencyObject.IsAssignableFrom(type);
        }

        protected override IWpfBindingDrawingItem CreateDrawingItem(BindingDrawingItem drawingItem)
        {
            return new WpfBindingDrawingItem
            {
                ClrType = drawingItem.ClrType,
                ConverterParamter = drawingItem.ConverterParamter,
                ConverterType = drawingItem.ConverterType,
                DependencyObjectType = drawingItem.DependencyObjectType,
                HasPropertyBind = drawingItem.HasPropertyBind,
                Path = drawingItem.Path,
                PropertyInfo = drawingItem.PropertyInfo,
            };
        }

        protected override void FillItem(IWpfBindingDrawingItem item, DependencyPropertyDescriptor descriptor)
        {
            var di = (WpfBindingDrawingItem)item;
            di.DependencyProperty = descriptor.DependencyProperty;
        }

        protected override IReadOnlyDictionary<string, DependencyPropertyDescriptor> GetDependencyPropertyDescriptorMap(Type type)
        {
            return DependencyObjectHelper.GetDependencyPropertyDescriptors(type);
        }

    }
}
