﻿using Ao.ObjectDesign.ForView;
using Ao.ObjectDesign.Wpf;
using System.Windows;

namespace ObjectDesign.Wpf.Views
{
    public class FontSettingCondition : IForViewCondition<DataTemplate, WpfTemplateForViewBuildContext>
    {
        public int Order { get; set; }

        public bool CanBuild(WpfTemplateForViewBuildContext context)
        {
            return false;
            //return context.PropertyProxy.Type == typeof(FontSetting);
        }

        public DataTemplate Create(WpfTemplateForViewBuildContext context)
        {
            return (DataTemplate)Application.Current.FindResource("ObjectDesign.FontSetting");
        }
    }
}
