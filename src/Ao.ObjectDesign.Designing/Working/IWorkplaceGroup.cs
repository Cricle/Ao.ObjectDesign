using System;
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
    public interface IWorkplaceGroup<TKey, TResource>: IWorking<TKey>
    {
        TKey Key { get; }


        IWithGroupWorkplace<TKey, TResource> Get(TKey key);


        IWithGroupWorkplace<TKey, TResource> Create(TKey key);


        IWorkplaceGroup<TKey, TResource> Group(TKey key);

        event EventHandler<ActionResouceResultEventArgs<TKey>> CreatedGroup;
        event EventHandler<ActionResouceResultEventArgs<TKey>> RemovedGroup;
        event EventHandler<CopyResourceResultEventArgs<TKey>> CopiedGroup;
        event EventHandler<CopyResourceResultEventArgs<TKey>> RenamedGroup;
        event EventHandler Clean;
    }
}
