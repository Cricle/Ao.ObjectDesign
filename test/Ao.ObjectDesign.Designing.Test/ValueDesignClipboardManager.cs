using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing.Test
{
    internal class ValueDesignClipboardManager : ValueDesignClipboardManager<int>
    {
        public override int Clone(int @object)
        {
            return @object;
        }
    }
    internal class ValueDesignClipboardManager<T> : DesignClipboardManager<T>
    {
        public IReadOnlyList<T> Values { get; set; }

        protected override IReadOnlyList<T> GetFromClipboard()
        {
            return Values;
        }

        protected override void SetToClipboard(IReadOnlyList<T> @object)
        {
            Values = @object;
        }
    }
}
