using System;

namespace Ao.ObjectDesign.WpfDesign
{
    public class ActionResouceResultEventArgs<TKey> : EventArgs
    {
        public ActionResouceResultEventArgs(TKey key, ResourceActions action)
        {
            Key = key;
            Action = action;
        }

        public TKey Key { get; }

        public ResourceActions Action { get; }
    }
}
