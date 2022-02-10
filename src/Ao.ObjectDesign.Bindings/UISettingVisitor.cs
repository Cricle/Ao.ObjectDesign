using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Bindings
{
    public interface IUIVisitor<TObject>
    {
        object GetValue(TObject @object);

        void SetValue(TObject @object, object value);
    }
}
