using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.WpfDesign.Working
{
    public class MemoryWorkplace<TKey, TResource> : Workplace<TKey, TResource>
    {
        private readonly IDictionary<TKey, TResource> origin;

        public MemoryWorkplace(IDictionary<TKey, TResource> origin)
        {
            this.origin = origin;
        }

        public override IEnumerable<TKey> Resources => origin.Keys;

        public override void Clear()
        {
            origin.Clear();
            RaiseClean();
        }

        public override void Copy(TKey sourceKey, TKey destKey)
        {
            ThrowIfKeyExists(destKey);
            var val = origin[sourceKey];
            origin.Add(destKey, val);
            RaiseCopiedResource(new CopyResourceResultEventArgs<TKey>(sourceKey, destKey));
        }

        public override TResource Get(TKey key)
        {
            return (TResource)origin[key];
        }

        public override bool Has(TKey key)
        {
            return origin.ContainsKey(key);
        }

        public override bool Remove(TKey key)
        {
            var ok = origin.Remove(key);
            if (ok)
            {
                RaiseRemovedResource(new ActionResouceResultEventArgs<TKey>(key, ResourceActions.Removed));
            }
            return ok;
        }

        public override void Rename(TKey oldKey, TKey newKey)
        {
            ThrowIfKeyExists(newKey);
            var val = origin[oldKey];
            origin.Remove(oldKey);
            origin.Add(newKey, val);
            RaiseRenamedResource(new CopyResourceResultEventArgs<TKey>(oldKey, newKey));
        }

        public override void Store(TKey key, TResource resource)
        {
            ThrowIfKeyExists(key);
            origin.Add(key, resource);
            RaiseStoredResource(new StoredResourceEventArgs<TKey, TResource>(key, resource));
        }

        protected void ThrowIfKeyExists(TKey key)
        {
            if (origin.ContainsKey(key))
            {
                throw new ArgumentException($"Key {key} exists");
            }
        }
    }
}
