using System;

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
}