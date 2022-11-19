using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using System.Windows;

namespace Ao.ObjectDesign.Session.Wpf
{
    public interface IBindingCreatorStateCreator<TSetting>
    {
        IBindingCreatorState GetBindingCreatorState(IDesignPair<UIElement, TSetting> unit);
    }
}
