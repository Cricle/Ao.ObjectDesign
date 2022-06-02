using Ao.ObjectDesign.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Sources
{
    public class InMemoryDataProvider: InMemoryDataProvider<object>
    {

    }
    public class InMemoryDataProvider<T> : DataAnyProviderBase<T>
    {
        private T value;

        public T Value
        {
            get => value;
            set
            {
                var old = value;
                if (!EqualityComparer<T>.Default.Equals(old,value))
                {
                    this.value = old;
                    RaiseDataChanged(old, value);
                }
            }
        }

        public override T Get()
        {
            return Value;
        }
    }
}
