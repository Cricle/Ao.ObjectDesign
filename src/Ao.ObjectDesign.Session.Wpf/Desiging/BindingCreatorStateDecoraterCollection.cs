using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using System.Collections.Generic;
using System.Windows;

namespace Ao.ObjectDesign.Session.Wpf.Desiging
{
    public class BindingCreatorStateDecoraterCollection<TSetting> : List<IBindingCreatorStateDecorater<TSetting>>, IBindingCreatorStateDecorater<TSetting>
    {
        public BindingCreatorStateDecoraterCollection()
        {
        }

        public BindingCreatorStateDecoraterCollection(int capacity) : base(capacity)
        {
        }

        public BindingCreatorStateDecoraterCollection(IEnumerable<IBindingCreatorStateDecorater<TSetting>> collection) : base(collection)
        {
        }

        public void Decorate(IBindingCreatorState state, IDesignPair<UIElement, TSetting> unit)
        {
            for (int i = 0; i < Count; i++)
            {
                this[i].Decorate(state, unit);
            }
        }
    }
}
