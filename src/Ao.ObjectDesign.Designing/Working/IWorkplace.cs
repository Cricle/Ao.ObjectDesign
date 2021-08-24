using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing.Working
{
    public interface IWorkplace<TKey, TResource>: IWorking<TKey>
    {
        TResource Get(TKey key);

        void Store(TKey key,TResource resource);

        event EventHandler<ActionResouceResultEventArgs<TKey>> CreatedResource;
        event EventHandler<ActionResouceResultEventArgs<TKey>> RemovedResource;
        event EventHandler<CopyResourceResultEventArgs<TKey>> CopiedResource;
        event EventHandler<CopyResourceResultEventArgs<TKey>> RenamedResource;
        event EventHandler<StoredResourceEventArgs<TKey, TResource>> StoredResource;
        event EventHandler Clean;
    }
}
