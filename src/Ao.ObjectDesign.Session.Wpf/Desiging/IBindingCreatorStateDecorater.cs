using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using System.Windows;

namespace Ao.ObjectDesign.Session.Wpf.Desiging
{
    public interface IBindingCreatorStateDecorater<TSetting>
    {
        void Decorate(IBindingCreatorState state, IDesignPair<UIElement, TSetting> unit);
    }
}
