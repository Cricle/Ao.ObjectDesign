using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing.Working
{
    public abstract class WorkplaceGroup<TKey, TResource> : IWorkplaceGroup<TKey,TResource>
    {
        protected WorkplaceGroup()
        {
        }

        protected WorkplaceGroup(TKey key)
        {
            Key = key;
        }

        public TKey Key { get; }

        public abstract IEnumerable<TKey> Resources { get; }

        public event EventHandler<ActionResouceResultEventArgs<TKey>> CreatedGroup;
        public event EventHandler<ActionResouceResultEventArgs<TKey>> RemovedGroup;
        public event EventHandler<CopyResourceResultEventArgs<TKey>> CopiedGroup;
        public event EventHandler<CopyResourceResultEventArgs<TKey>> RenamedGroup;
        public event EventHandler Clean;

        protected void RaiseClean()
        {
            Clean?.Invoke(this, EventArgs.Empty);
        }

        protected void RaiseRenamedGroup(CopyResourceResultEventArgs<TKey> e)
        {
            RenamedGroup?.Invoke(this, e);
        }

        protected void RaiseCopiedGroup(CopyResourceResultEventArgs<TKey> e)
        {
            CopiedGroup?.Invoke(this, e);
        }

        protected void RaiseRemovedGroup(ActionResouceResultEventArgs<TKey> e)
        {
            RemovedGroup?.Invoke(this, e);
        }

        protected void RaiseCreatedGroup(ActionResouceResultEventArgs<TKey> e)
        {
            CreatedGroup?.Invoke(this, e);
        }

        public abstract IWithGroupWorkplace<TKey, TResource> Get(TKey key);
        public abstract bool Remove(TKey key);
        public abstract void Copy(TKey sourceKey, TKey destkey);
        public abstract IWithGroupWorkplace<TKey, TResource> Create(TKey key);
        public abstract void Rename(TKey oldKey, TKey newKey);
        public abstract bool Has(TKey key);
        public abstract void Clear();
        public abstract IWorkplaceGroup<TKey, TResource> Group(TKey key);
    }
}
