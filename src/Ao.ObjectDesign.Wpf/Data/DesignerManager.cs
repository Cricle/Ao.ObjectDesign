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
            BindingDrawing drawing = new BindingDrawing(clr.GetType(), @object.GetType(), AttributeBindForGetter.Instance);
            return CreateBindings(drawing, clr, mode, updateSourceTrigger);
        }
        public static IEnumerable<BindingUnit> CreateBindings(BindingDrawing drawing,
            object source)
        {
            return CreateBindings(drawing, source, BindingMode.Default, UpdateSourceTrigger.Default);
        }
        public static IEnumerable<BindingUnit> CreateBindings(BindingDrawing drawing,
            object source,
            BindingMode mode,
            UpdateSourceTrigger updateSourceTrigger)
        {
            return drawing.Analysis().Where(x => x.HasPropertyBind)
                .Select(x =>
                {
                    var binding = new Binding(x.Path)
                    {
                        Source = source,
                        Mode = mode,
                        UpdateSourceTrigger = updateSourceTrigger
                    };
                    if (x.ConverterType != null)
                    {
                        var converter = (IValueConverter)ReflectionHelper.Create(x.ConverterType);
                        binding.Converter = converter;
                        binding.ConverterParameter = x.ConverterParamter;
                    }
                    var unit = new BindingUnit(binding, x.DependencyProperty);
                    return unit;
                });
        }
    }
}
