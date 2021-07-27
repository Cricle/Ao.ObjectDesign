using Ao.ObjectDesign.Designing.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Data
{
    public static class DesignerManager
    {
        public static IEnumerable<BindingUnit> CreateBindings(object clr,
            DependencyObject @object)
        {
            return CreateBindings(clr, @object, BindingMode.Default, UpdateSourceTrigger.Default);
        }
        public static IEnumerable<BindingUnit> CreateBindings(object clr,
            DependencyObject @object,
            BindingMode mode,
            UpdateSourceTrigger updateSourceTrigger)
        {
            WpfBindingDrawing drawing = new WpfBindingDrawing(clr.GetType(), @object.GetType(), AttributeBindForGetter.Instance);
            return CreateBindings(drawing, clr, mode, updateSourceTrigger);
        }
        public static IEnumerable<BindingUnit> CreateBindings(WpfBindingDrawing drawing,
            object source)
        {
            return CreateBindings(drawing, source, BindingMode.Default, UpdateSourceTrigger.Default);
        }
        public static IEnumerable<BindingUnit> CreateBindings(WpfBindingDrawing drawing,
            object source,
            BindingMode mode,
            UpdateSourceTrigger updateSourceTrigger)
        {
            foreach (var item in drawing.Analysis().Where(x => x.HasPropertyBind))
            {
                Binding binding = new Binding(item.Path)
                {
                    Source = source,
                    Mode = mode,
                    UpdateSourceTrigger = updateSourceTrigger
                };
                if (item.ConverterType != null)
                {
                    var converter = (IValueConverter)ReflectionHelper.Create(item.ConverterType);
                    binding.Converter = converter;
                    binding.ConverterParameter = item.ConverterParamter;
                }
                yield return new BindingUnit(binding, item.DependencyProperty);
            }
        }
    }
}
