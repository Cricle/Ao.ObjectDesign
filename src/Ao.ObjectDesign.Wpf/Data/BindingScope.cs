using System;
using System.Windows.Data;

namespace Ao.ObjectDesign.Data
{
    public class BindingScope : BindingScopeBase<Binding>
    {
        public BindingScope(IBindingMaker<Binding> creator, Action<Binding> bindingAction) : base(creator, bindingAction)
        {
        }

        protected override Binding CreateEmptyBinding(object source)
        {
            return new Binding { Source = source };
        }
    }

}
