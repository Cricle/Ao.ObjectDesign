using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing.Working
{
    public interface IWorking<TKey>
    {
        IEnumerable<TKey> Resources { get; }

        bool Remove(TKey key);

        void Copy(TKey sourceKey, TKey destkey);

        void Rename(TKey oldKey, TKey newKey);

        bool Has(TKey key);

        void Clear();

    }
}
