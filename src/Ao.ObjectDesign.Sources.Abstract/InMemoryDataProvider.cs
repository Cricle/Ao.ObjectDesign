using Ao.ObjectDesign.Data;
using System.Collections.Generic;

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
                var old = this.value;
                if (!EqualityComparer<T>.Default.Equals(old,value))
                {
                    this.value = value;
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
