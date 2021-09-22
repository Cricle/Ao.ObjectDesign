using Ao.ObjectDesign.ForView;
using System.Windows;

namespace Ao.ObjectDesign.Wpf
{
    public class WpfTemplateForViewBuildContext : WpfForViewBuildContextBase
    {


        public IForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext> ForViewBuilder
        {
            get { return (IForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext>)GetValue(ForViewBuilderProperty); }
            set { SetValue(ForViewBuilderProperty, value); }
        }

        public static readonly DependencyProperty ForViewBuilderProperty =
            DependencyProperty.Register("ForViewBuilder", typeof(IForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext>), typeof(WpfTemplateForViewBuildContext), new PropertyMetadata(null));

    }
}
