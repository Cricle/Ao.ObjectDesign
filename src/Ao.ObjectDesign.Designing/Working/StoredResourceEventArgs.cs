using System;

namespace Ao.ObjectDesign.Designing.Working
{
    public class StoredResourceEventArgs<TKey, TResource> : EventArgs
    {
        public StoredResourceEventArgs(TKey key, TResource scene)
        {
            Key = key;
            Resource = scene;
        }

        public TKey Key { get; }

        public TResource Resource { get; }
    }
}
