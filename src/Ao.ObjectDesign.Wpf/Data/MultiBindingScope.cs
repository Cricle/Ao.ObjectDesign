using System;
using System.Windows.Data;

namespace Ao.ObjectDesign.Data
{
    public class MultiBindingScope : BindingScopeBase<MultiBinding>
    {
        public MultiBindingScope(IBindingMaker<MultiBinding> creator, Action<MultiBinding> bindingAction)
            : base(creator, bindingAction)
        {
        }

        protected override MultiBinding CreateEmptyBinding(object source)
        {
            return new MultiBinding();
        }
    }

}
