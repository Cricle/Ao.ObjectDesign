using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Designing.Level;

using ObjectDesign.Brock.Controls.BindingCreators;
using ObjectDesign.Brock.Controls.Designing;
using Ao.ObjectDesign.Bindings;
using ObjectDesign.Brock.Components;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace ObjectDesign.Brock.Controls
{
    
        
    
    public class ItemsControlSetting : ControlSetting, IObservableDesignScene<UIElementSetting>
    {
        public ItemsControlSetting()
        {
            DesigningObjects = new SilentObservableCollection<UIElementSetting>();
        }

        private bool isTextSearchEnabled;
        private bool isTextSearchCaseSensitive;
        private ItemsPanelTemplateDesigning itemsPanelTemplate;

        public ItemsPanelTemplateDesigning ItemsPanel
        {
            get => itemsPanelTemplate;
            set => Set(ref itemsPanelTemplate, value);
        }

        public bool IsTextSearchCaseSensitive
        {
            get => isTextSearchCaseSensitive;
            set => Set(ref isTextSearchCaseSensitive, value);
        }

        public bool IsTextSearchEnabled
        {
            get => isTextSearchEnabled;
            set => Set(ref isTextSearchEnabled, value);
        }

        public SilentObservableCollection<UIElementSetting> DesigningObjects { get; }

        IList<UIElementSetting> IDesignScene<UIElementSetting>.DesigningObjects => DesigningObjects;

        public override void SetDefault()
        {
            base.SetDefault();
            IsTextSearchEnabled = true;
            IsTextSearchCaseSensitive = false;
            ItemsPanel = new ItemsPanelTemplateDesigning();
        }
    }
}
