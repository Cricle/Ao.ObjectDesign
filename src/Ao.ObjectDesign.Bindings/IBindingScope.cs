using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Bindings
{
    public interface IBindingScope<TBinding,TExpression,TObject>
    {
        TBinding CreateBinding(object source);

        TExpression Bind(TObject @object, object source);
    }
}
