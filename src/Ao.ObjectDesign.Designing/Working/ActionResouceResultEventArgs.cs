using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Ao.ObjectDesign.Designing.Working
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
