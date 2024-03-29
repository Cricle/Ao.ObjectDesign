﻿using Ao.ObjectDesign.ForView;
using Ao.ObjectDesign.Wpf.Conditions;
using System;
using System.Windows;

namespace Ao.ObjectDesign.Wpf
{
    public static class WpfForViewBuilderExtensions
    {
        public static void AddWpfCondition(this IForViewBuilder<FrameworkElement, WpfForViewBuildContext> builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Add(new BooleanForViewCondition());
            builder.Add(new ValueTypeForViewCondition());
            builder.Add(new EnumForViewCondition());
        }
    }
}
