using Ao.ObjectDesign.ForView;
using System.Windows;

namespace Ao.ObjectDesign
{
    public class WpfForViewBuildContext : WpfForViewBuildContextBase
    {


        public IForViewBuilder<FrameworkElement, WpfForViewBuildContext> ForViewBuilder
        {
            get { return (IForViewBuilder<FrameworkElement, WpfForViewBuildContext>)GetValue(ForViewBuilderProperty); }
            set { SetValue(ForViewBuilderProperty, value); }
        }

        public static readonly DependencyProperty ForViewBuilderProperty =
            DependencyProperty.Register("ForViewBuilder", typeof(IForViewBuilder<FrameworkElement, WpfForViewBuildContext>), typeof(WpfForViewBuildContextBase), new PropertyMetadata(null));


    }
}
