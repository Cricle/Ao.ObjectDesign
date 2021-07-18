using Ao.ObjectDesign.ForView;
using System;
using System.Windows;

namespace Ao.ObjectDesign.Wpf
{
    public static class WpfObjectDesignerSettingsExtensions
    {
        public static ForViewDataTemplateSelector CreateTemplateSelector(this IWpfObjectDesignerSettings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return new ForViewDataTemplateSelector(
                settings.DataTemplateBuilder,
                settings.Designer);
        }
    }
    public interface IWpfObjectDesignerSettings
    {
        IForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext> DataTemplateBuilder { get; }
        IObjectDesigner Designer { get; }
        INotifyableSequencer Sequencer { get; }
        IForViewBuilder<FrameworkElement, WpfForViewBuildContext> UIBuilder { get; }
        IWpfUIGenerator UIGenerator { get; }
    }
}