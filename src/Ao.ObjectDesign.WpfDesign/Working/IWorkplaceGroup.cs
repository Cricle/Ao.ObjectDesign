using System;

namespace Ao.ObjectDesign.WpfDesign.Working
{
    public interface IWorkplaceGroup<TKey, TResource>
    {
        TKey Key { get; }

        IWithGroupWorkplace<TKey, TResource> Get(TKey key);

        bool Remove(TKey key);

        void Copy(TKey key, TKey destkey);

        IWithGroupWorkplace<TKey, TResource> Create(TKey key);

        void Rename(TKey oldKey, TKey newKey);

        bool Has(TKey key);

        void Clear();

        IWorkplaceGroup<TKey, TResource> Group(TKey key);

        event EventHandler<ActionResouceResultEventArgs<TKey>> CreatedGroup;
        event EventHandler<ActionResouceResultEventArgs<TKey>> RemovedGroup;
        event EventHandler<CopyResourceResultEventArgs<TKey>> CopiedGroup;
        event EventHandler<CopyResourceResultEventArgs<TKey>> RenamedGroup;
        event EventHandler Clean;
    }
}
