using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing.Test
{
    internal class ValueDesignClipboardManager : DesignClipboardManager<int>
    {
        public IReadOnlyList<int> Values { get; set; }

        protected override IReadOnlyList<int> GetFromClipboard()
        {
            return Values;
        }

        protected override void SetToClipboard(IReadOnlyList<int> @object)
        {
            Values = @object;
        }
        public override int Clone(int @object)
        {
            return @object;
        }
    }
}
