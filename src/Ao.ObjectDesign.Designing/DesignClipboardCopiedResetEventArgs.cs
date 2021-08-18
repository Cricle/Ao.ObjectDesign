using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing
{
    public class DesignClipboardCopiedResetEventArgs<TDesignObject> : EventArgs
    {
        public DesignClipboardCopiedResetEventArgs(IReadOnlyList<TDesignObject> oldCopiedObjects,
            IReadOnlyList<TDesignObject> oldOriginObjects,
            IReadOnlyList<TDesignObject> newCopiedObjects,
            IReadOnlyList<TDesignObject> newOriginObjects)
        {
            OldCopiedObjects = oldCopiedObjects;
            OldOriginObjects = oldOriginObjects;
            NewCopiedObjects = newCopiedObjects;
            NewOriginObjects = newOriginObjects;
        }

        public IReadOnlyList<TDesignObject> OldCopiedObjects { get; }

        public IReadOnlyList<TDesignObject> OldOriginObjects { get; }

        public IReadOnlyList<TDesignObject> NewCopiedObjects { get; }

        public IReadOnlyList<TDesignObject> NewOriginObjects { get; }
    }
}
