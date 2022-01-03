using Ao.ObjectDesign.WpfDesign;
using ObjectDesign.Brock.Components;
using System.Collections.Generic;

namespace ObjectDesign.Brock.InMemory
{
    public class MemoryDesignClipboardManager : WpfDesignClipboardManager<UIElementSetting>
    {
        private IReadOnlyList<UIElementSetting> settings;

        protected override IReadOnlyList<UIElementSetting> GetFromClipboard()
        {
            return settings;
        }

        protected override void SetToClipboard(IReadOnlyList<UIElementSetting> @object)
        {
            settings = @object;
        }
    }
}
