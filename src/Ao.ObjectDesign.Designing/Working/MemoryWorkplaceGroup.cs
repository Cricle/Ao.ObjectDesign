using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing.Working
{
    public class MemoryWorkplaceGroup<TKey,TResource> : WorkplaceGroup<TKey, TResource>
    {
        private readonly IDictionary<TKey, IDictionary<TKey, TResource>> resources;

        public override IEnumerable<TKey> Resources => resources.Keys;

        public MemoryWorkplaceGroup()
        {
            resources = new Dictionary<TKey, IDictionary<TKey, TResource>>();
        }
        public MemoryWorkplaceGroup(TKey key)
            :base(key)
        {
            this.resources = new Dictionary<TKey, IDictionary<TKey, TResource>>();
        }

        public MemoryWorkplaceGroup(IDictionary<TKey, IDictionary<TKey, TResource>> resources)
        {
            this.resources = resources ?? throw new ArgumentNullException(nameof(resources));
        }
        public MemoryWorkplaceGroup(TKey key, IDictionary<TKey, IDictionary<TKey, TResource>> resources)
            :base(key)
        {
            this.resources = resources ?? throw new ArgumentNullException(nameof(resources));
        }

        public override void Clear()
        {
            resources.Clear();
            RaiseClean();
        }

        public override void Copy(TKey key, TKey destkey)
        {
            ThrowIfKeyExists(key);
            var val =new Dictionary<TKey, TResource>(resources[key]);
            resources.Add(destkey, val);
            RaiseCopiedGroup(new CopyResourceResultEventArgs<TKey>(key, destkey));
        }
        protected void ThrowIfKeyExists(TKey key)
        {
            if (resources.ContainsKey(key))
            {
                throw new ArgumentException($"Key {key} exists");
            }
        }

        public override IWithGroupWorkplace<TKey, TResource> Create(TKey key)
        {
            ThrowIfKeyExists(key);
            resources.Add(key, new Dictionary<TKey, TResource>());
            RaiseCreatedGroup(new ActionResouceResultEventArgs<TKey>(key, ResourceActions.Created));
            return Get(key);
        }

        public override IWithGroupWorkplace<TKey, TResource> Get(TKey key)
        {
            return new WithGroupMemoryWorkplace<TKey,TResource>(resources[key], key, this);
        }

        public override bool Has(TKey key)
        {
            return resources.ContainsKey(key);
        }

        public override void Rename(TKey oldKey, TKey newKey)
        {
            ThrowIfKeyExists(newKey);
            var val = resources[oldKey];
            resources.Remove(oldKey);
            resources.Add(newKey, val);
            RaiseRenamedGroup(new CopyResourceResultEventArgs<TKey>(oldKey, newKey));
        }

        public override bool Remove(TKey key)
        {
            var ok= resources.Remove(key);
            if (ok)
            {
                RaiseRemovedGroup(new ActionResouceResultEventArgs<TKey>(key, ResourceActions.Removed));
            }
            return ok;
        }

        public override IWorkplaceGroup<TKey,TResource> Group(TKey key)
        {
            return new MemoryWorkplaceGroup<TKey, TResource>(key, resources);
        }
    }
}
