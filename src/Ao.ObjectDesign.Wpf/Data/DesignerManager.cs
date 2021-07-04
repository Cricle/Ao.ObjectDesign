using Ao.ObjectDesign.ForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf.Data
{
    public static class DesignerManager
    {
        public static IEnumerable<BindingUnit> CreateBindings(object clr,
            DependencyObject @object,
            BindingMode mode = BindingMode.Default,
            UpdateSourceTrigger updateSourceTrigger = UpdateSourceTrigger.Default)
        {
            var drawing = new BindingDrawing(clr.GetType(),@object.GetType());
            return CreateBindings(drawing, clr, mode, updateSourceTrigger);
        }
        public static IEnumerable<BindingUnit> CreateBindings(BindingDrawing drawing,
            object source,
            BindingMode mode = BindingMode.Default, 
            UpdateSourceTrigger updateSourceTrigger = UpdateSourceTrigger.Default)
        {
            return drawing.Analysis().Where(x => x.HasPropertyBind)
                .Select(x => new BindingUnit(new Binding(x.Path)
                {
                    Source = source,
                    Mode = mode,
                    UpdateSourceTrigger = updateSourceTrigger
                }, x.DependencyProperty));
        }
    }
}
