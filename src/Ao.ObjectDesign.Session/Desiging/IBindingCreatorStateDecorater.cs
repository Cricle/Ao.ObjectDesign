using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.WpfDesign;
using System.Windows;

namespace Ao.ObjectDesign.Session.Desiging
{
    public interface IBindingCreatorStateDecorater<TSetting>
    {
        void Decorate(IBindingCreatorState state, IDesignPair<UIElement, TSetting> unit);
    }
}
