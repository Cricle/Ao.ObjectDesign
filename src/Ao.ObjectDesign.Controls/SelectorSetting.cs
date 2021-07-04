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
    [MappingFor(typeof(Selector))]
    public class SelectorSetting:ItemsControlSetting,IMiddlewareDesigner<Selector>
    {
        private int selectedIndex;
        private bool? isSynchronizedWithCurrentItem;
        private string selectedValuePath;

        public virtual string SelectedValuePath
        {
            get => selectedValuePath;
            set => Set(ref selectedValuePath, value);
        }

        public virtual bool? IsSynchronizedWithCurrentItem
        {
            get => isSynchronizedWithCurrentItem;
            set => Set(ref isSynchronizedWithCurrentItem, value);
        }

        public virtual int SelectedIndex
        {
            get => selectedIndex;
            set => Set(ref selectedIndex, value);
        }
        public override void SetDefault()
        {
            base.SetDefault();
            SelectedIndex = -1;
            IsSynchronizedWithCurrentItem = null;
            SelectedValuePath = null;
        }
        public void Apply(Selector value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((ItemsControl)value);
                SelectedIndex = value.SelectedIndex;
                IsSynchronizedWithCurrentItem = value.IsSynchronizedWithCurrentItem;
                SelectedValuePath = value.SelectedValuePath;
            }
        }

        public void WriteTo(Selector value)
        {
            if (value!=null)
            {
                WriteTo((ItemsControl)value);
                value.SelectedIndex = SelectedIndex;
                value.IsSynchronizedWithCurrentItem = IsSynchronizedWithCurrentItem;
                value.SelectedValuePath = SelectedValuePath;

            }
        }
    }
}
