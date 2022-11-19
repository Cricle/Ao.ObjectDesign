using Ao.ObjectDesign.Designing.Data;
using System.Windows;

namespace Ao.ObjectDesign.Data
{
    public interface IWpfBindingDrawingItem : IBindingDrawingItem
    {
        DependencyProperty DependencyProperty { get; }
    }
}
