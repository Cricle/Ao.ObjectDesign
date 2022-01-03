using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDesign.Brock
{
    public class ValueChangedEventArgs<T> : EventArgs
    {
        public ValueChangedEventArgs(T old, T @new)
        {
            Old = old;
            New = @new;
        }

        public T Old { get; }

        public T New { get; }
    }
}
