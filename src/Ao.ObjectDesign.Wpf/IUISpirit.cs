using System.Windows;

namespace Ao.ObjectDesign.Wpf
{
    public interface IWpfUISpirit : IUISpirit<FrameworkElement, WpfForViewBuildContext>
    {

    }
    public interface IUISpirit<out TView, TContext>
    {
        TContext Context { get; }
        TView View { get; }
    }
}