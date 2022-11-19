using Ao.ObjectDesign.Designing.Data;
using System.Windows;

namespace Ao.ObjectDesign.Data
{
    public class WpfBindingDrawingItem : BindingDrawingItem, IWpfBindingDrawingItem
    {
        public DependencyProperty DependencyProperty { get; set; }
    }
}
