using Ao.ObjectDesign.Wpf.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(StackPanel))]
    public class StackPanelSetting:PanelSetting,IMiddlewareDesigner<StackPanel>
    {
        private bool canVerticallyScroll;
        private bool canHorizontallyScroll;
        private Orientation orientation;

        public virtual Orientation Orientation
        {
            get => orientation;
            set => Set(ref orientation, value);
        }

        public virtual bool CanHorizontallyScroll
        {
            get => canHorizontallyScroll;
            set => Set(ref canHorizontallyScroll, value);
        }
        public virtual bool CanVerticallyScroll
        {
            get => canVerticallyScroll;
            set => Set(ref canVerticallyScroll, value);
        }

        public override void SetDefault()
        {
            base.SetDefault();
            CanHorizontallyScroll = CanVerticallyScroll = false;
            Orientation = Orientation.Vertical;
        }

        public void Apply(StackPanel value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((Panel)value);
                CanHorizontallyScroll = value.CanHorizontallyScroll;
                CanVerticallyScroll = value.CanVerticallyScroll;
                Orientation = value.Orientation;
            }
        }

        public void WriteTo(StackPanel value)
        {
            if (value != null)
            {
                WriteTo((Panel)value);
                value.CanHorizontallyScroll = CanHorizontallyScroll;
                value.CanVerticallyScroll = CanVerticallyScroll;
                value.Orientation = Orientation;
            }
        }
    }
}
