using Ao.ObjectDesign.WpfDesign.Level;
using System;

namespace Ao.ObjectDesign.WpfDesign
{
    public class StoredResourceEventArgs<TKey,TResource> : EventArgs
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
