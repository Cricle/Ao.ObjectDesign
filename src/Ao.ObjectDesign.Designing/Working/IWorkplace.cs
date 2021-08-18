using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing.Working
{
    public interface IWorkplace<TKey, TResource>
    {
        IEnumerable<TKey> Resources { get; }

        TResource Get(TKey key);

        bool Remove(TKey key);

        void Copy(TKey sourceKey, TKey destKey);

        void Store(TKey key,TResource resource);

        void Rename(TKey oldKey, TKey newKey);

        bool Has(TKey key);

        void Clear();

        event EventHandler<ActionResouceResultEventArgs<TKey>> CreatedResource;
        event EventHandler<ActionResouceResultEventArgs<TKey>> RemovedResource;
        event EventHandler<CopyResourceResultEventArgs<TKey>> CopiedResource;
        event EventHandler<CopyResourceResultEventArgs<TKey>> RenamedResource;
        event EventHandler<StoredResourceEventArgs<TKey, TResource>> StoredResource;
        event EventHandler Clean;
    }
}
