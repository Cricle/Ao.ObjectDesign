using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Data;
using System.Collections.Generic;
using System.Windows;

namespace Ao.ObjectDesign.Wpf
{
    public class WpfBindingCreatorFactoryCollection<TDesignObject> : BindingCreatorFactoryCollection<UIElement, TDesignObject, IWithSourceBindingScope>
    {
        public WpfBindingCreatorFactoryCollection()
        {
        }

        public WpfBindingCreatorFactoryCollection(int capacity) : base(capacity)
        {
        }

        public WpfBindingCreatorFactoryCollection(IEnumerable<IBindingCreatorFactory<UIElement, TDesignObject, IWithSourceBindingScope>> collection) : base(collection)
        {
        }
    }
}
