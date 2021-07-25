using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(ListBox))]
    public class ListBoxSetting : SelectorSetting, IMiddlewareDesigner<ListBox>
    {
        private SelectionMode selectionMode;

        [DefaultValue(SelectionMode.Single)]
        public virtual SelectionMode SelectionMode
        {
            get => selectionMode;
            set => Set(ref selectionMode, value);
        }

        public override void SetDefault()
        {
            base.SetDefault();
            SelectionMode = SelectionMode.Single;
        }

        public void Apply(ListBox value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((Selector)value);
                SelectionMode = value.SelectionMode;
            }
        }

        public void WriteTo(ListBox value)
        {
            if (value != null)
            {
                WriteTo((Selector)value);
                value.SelectionMode = selectionMode;
            }
        }
    }
}
