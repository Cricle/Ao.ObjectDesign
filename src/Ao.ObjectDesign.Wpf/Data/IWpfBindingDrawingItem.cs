using Ao.ObjectDesign.Designing.Data;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Data
{
    public interface IWpfBindingDrawingItem : IBindingDrawingItem
    {
        DependencyProperty DependencyProperty { get; }
    }
}
