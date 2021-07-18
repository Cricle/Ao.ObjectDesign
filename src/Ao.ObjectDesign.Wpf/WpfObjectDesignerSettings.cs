using Ao.ObjectDesign.ForView;
using System;
using System.Windows;

namespace Ao.ObjectDesign.Wpf
{
    public class WpfObjectDesignerSettings : IWpfObjectDesignerSettings
    {
        public IForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext> DataTemplateBuilder { get; set; }

        public IForViewBuilder<FrameworkElement, WpfForViewBuildContext> UIBuilder { get; set; }

        public IObjectDesigner Designer { get; set; }

        public INotifyableSequencer Sequencer { get; set; }

        public IWpfUIGenerator UIGenerator { get; set; }

        internal void Check()
        {
            if (DataTemplateBuilder is null)
            {
                throw new ArgumentNullException(nameof(DataTemplateBuilder));
            }
            if (UIBuilder is null)
            {
                throw new ArgumentNullException(nameof(UIBuilder));
            }
            if (Designer is null)
            {
                throw new ArgumentNullException(nameof(Designer));
            }
            if (Sequencer is null)
            {
                throw new ArgumentNullException(nameof(Sequencer));
            }
            if (UIGenerator is null)
            {
                throw new ArgumentNullException(nameof(UIGenerator));
            }
        }
    }
}
