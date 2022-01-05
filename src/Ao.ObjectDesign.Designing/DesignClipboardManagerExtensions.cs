using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing
{
    public static partial class DesignClipboardManagerActionsExtensions
    {
        public static IReadOnlyList<TDesignObject> GetFromCopied<TDesignObject>(this DesignClipboardManager<TDesignObject> manager,
            Func<TDesignObject, TDesignObject> copyFactory)
        {
            return GetFromCopied(manager, copyFactory, true, true);
        }

        public static IReadOnlyList<TDesignObject> GetFromCopied<TDesignObject>(this DesignClipboardManager<TDesignObject> manager)
        {
            return GetFromCopied(manager,true, true);
        }
        public static IReadOnlyList<TDesignObject> GetFromCopied<TDesignObject>(this DesignClipboardManager<TDesignObject> manager,
            Func<TDesignObject, TDesignObject> copyFactory,
            bool canNull)
        {
            return GetFromCopied(manager, copyFactory, canNull, true);
        }
        public static IReadOnlyList<TDesignObject> GetFromCopied<TDesignObject>(this DesignClipboardManager<TDesignObject> manager,
            bool canNull)
        {
            return GetFromCopied(manager, canNull, true);
        }
        public static IReadOnlyList<TDesignObject> GetFromCopied<TDesignObject>(this DesignClipboardManager<TDesignObject> manager,
            bool canNull,
            bool copy)
        {
            return GetFromCopied(manager, x => manager.Clone(x), canNull, copy);
        }
        public static IReadOnlyList<TDesignObject> GetFromCopied<TDesignObject>(this DesignClipboardManager<TDesignObject> manager,
            Func<TDesignObject, TDesignObject> copyFactory,
            bool canNull,
            bool copy)
        {
            if (manager is null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            if (copyFactory is null)
            {
                throw new ArgumentNullException(nameof(copyFactory));
            }

            manager.UpdateFromClipboard(canNull, copy);
            IReadOnlyList<TDesignObject> obj = manager.CopiedObjects;
            if (obj != null)
            {
                return obj.Select(copyFactory).ToList();
            }

            return ArrayHelper<TDesignObject>.Empty;
        }
    }
}
