using Ao.ObjectDesign.ForView;
using Ao.ObjectDesign.Wpf.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ao.ObjectDesign.Wpf
{
    public static class WpfForViewBuilderExtensions
    {
        public static void AddWpfCondition(this IForViewBuilder<FrameworkElement, WpfForViewBuildContext> builder)
        {
            builder.Add(new BooleanForViewCondition());
            builder.Add(new ValueTypeForViewCondition());
            builder.Add(new EnumForViewCondition());
        }
    }
}
