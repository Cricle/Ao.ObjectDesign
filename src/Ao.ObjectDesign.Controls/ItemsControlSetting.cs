using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Designing;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(ItemsControl))]
    public class ItemsControlSetting : FrameworkElementSetting, IMiddlewareDesigner<ItemsControl>
    {
        private int alternationCount;
        private string itemStringFormat;
        private string displayMemberPath;
        private bool isTextSearchCaseSensitive;
        private bool isTextSearchEnabled;

        [DefaultValue(false)]
        public virtual bool IsTextSearchEnabled
        {
            get => isTextSearchEnabled;
            set => Set(ref isTextSearchEnabled, value);
        }

        [DefaultValue(false)]
        public virtual bool IsTextSearchCaseSensitive
        {
            get => isTextSearchCaseSensitive;
            set => Set(ref isTextSearchCaseSensitive, value);
        }

        [DefaultValue(null)]
        public virtual string DisplayMemberPath
        {
            get => displayMemberPath;
            set => Set(ref displayMemberPath, value);
        }

        [DefaultValue(null)]
        public virtual string ItemStringFormat
        {
            get => itemStringFormat;
            set => Set(ref itemStringFormat, value);
        }

        [DefaultValue(0)]
        public virtual int AlternationCount
        {
            get => alternationCount;
            set => Set(ref alternationCount, value);
        }

        public override void SetDefault()
        {
            base.SetDefault();
            AlternationCount = 0;
            ItemStringFormat = null;
            DisplayMemberPath = null;
            IsTextSearchCaseSensitive = false;
            IsTextSearchEnabled = false;
        }

        public void Apply(ItemsControl value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((FrameworkElement)value);
                AlternationCount = value.AlternationCount;
                ItemStringFormat = value.ItemStringFormat;
                DisplayMemberPath = value.DisplayMemberPath;
                IsTextSearchCaseSensitive = value.IsTextSearchCaseSensitive;
                IsTextSearchEnabled = value.IsTextSearchEnabled;
            }
        }

        public void WriteTo(ItemsControl value)
        {
            if (value != null)
            {
                WriteTo((FrameworkElement)value);
                value.AlternationCount = AlternationCount;
                value.ItemStringFormat = ItemStringFormat;
                value.DisplayMemberPath = DisplayMemberPath;
                value.IsTextSearchCaseSensitive = IsTextSearchCaseSensitive;
                value.IsTextSearchEnabled = IsTextSearchEnabled;

            }
        }
    }
}
