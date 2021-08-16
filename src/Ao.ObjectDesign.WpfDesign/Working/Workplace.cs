using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign.WpfDesign.Working
{
    public abstract class Workplace<TKey,TResource> : IWorkplace<TKey,TResource>
    {
        public abstract IEnumerable<TKey> Resources { get; }

        public event EventHandler<ActionResouceResultEventArgs<TKey>> CreatedResource;
        public event EventHandler<ActionResouceResultEventArgs<TKey>> RemovedResource;
        public event EventHandler<CopyResourceResultEventArgs<TKey>> CopiedResource;
        public event EventHandler<CopyResourceResultEventArgs<TKey>> RenamedResource;
        public event EventHandler<StoredResourceEventArgs<TKey,TResource>> StoredResource;
        public event EventHandler Clean;

        protected void RaiseClean()
        {
            Clean?.Invoke(this, EventArgs.Empty);
        }

        protected void RaiseStoredResource(StoredResourceEventArgs<TKey,TResource> e)
        {
            StoredResource?.Invoke(this, e);
        }

        protected void RaiseRenamedResource(CopyResourceResultEventArgs<TKey> e)
        {
            RenamedResource?.Invoke(this, e);
        }

        protected void RaiseCopiedResource(CopyResourceResultEventArgs<TKey> e)
        {
            CopiedResource?.Invoke(this, e);
        }

        protected void RaiseRemovedResource(ActionResouceResultEventArgs<TKey> e)
        {
            RemovedResource?.Invoke(this, e);
        }

        protected void RaiseCreatedResource(ActionResouceResultEventArgs<TKey> e)
        {
            CreatedResource?.Invoke(this, e);
        }

        public abstract TResource Get(TKey key);
        public abstract bool Remove(TKey key);
        public abstract void Copy(TKey sourceKey, TKey destKey);
        public abstract void Store(TKey key, TResource resource);
        public abstract void Rename(TKey oldKey, TKey newKey);
        public abstract bool Has(TKey key);
        public abstract void Clear();
    }
}
