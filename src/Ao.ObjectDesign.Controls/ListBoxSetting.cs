using Ao.ObjectDesign.Wpf.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(ListBox))]
    public class ListBoxSetting : SelectorSetting,IMiddlewareDesigner<ListBox>
    {
        private SelectionMode selectionMode;

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
            if (value !=null)
            {
                WriteTo((Selector)value);
                value.SelectionMode = selectionMode;
            }
        }
    }
}
