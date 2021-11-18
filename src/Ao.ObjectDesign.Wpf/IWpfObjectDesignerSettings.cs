using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.ForView;
using System;
using System.Windows;

namespace Ao.ObjectDesign.Wpf
{
    public interface IWpfObjectDesignerSettings
    {
        IForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext> DataTemplateBuilder { get; }
        IObjectDesigner Designer { get; }
        IActionSequencer<IFallbackable> Sequencer { get; }
        IForViewBuilder<FrameworkElement, WpfForViewBuildContext> UIBuilder { get; }
        IWpfUIGenerator UIGenerator { get; }
    }
}