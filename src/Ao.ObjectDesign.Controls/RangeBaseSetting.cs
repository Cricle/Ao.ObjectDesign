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
    [MappingFor(typeof(RangeBase))]
    public abstract class RangeBaseSetting : ControlSetting,IMiddlewareDesigner<RangeBase>
    {
        private double largeChange ;
        private double smallChange ;
        private double value;
        private double maximum;
        private double minimum;

        public virtual double Minimum
        {
            get => minimum;
            set => Set(ref minimum, value);
        }
        public virtual double Maximum
        {
            get => maximum;
            set => Set(ref maximum, value);
        }

        public virtual double Value
        {
            get => value;
            set => Set(ref this.value, value);
        }

        public virtual double SmallChange
        {
            get => smallChange;
            set => Set(ref smallChange, value);
        }


        public virtual double LargeChange
        {
            get => largeChange;
            set => Set(ref largeChange, value);
        }

        public override void SetDefault()
        {
            base.SetDefault();
            LargeChange = 1;
            SmallChange = 0.1;
            Minimum = 0;
            Maximum = 100;
            Value = 0;
        }

        public void Apply(RangeBase value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((Control)value);
                LargeChange = value.LargeChange;
                SmallChange =value.SmallChange;
                Minimum = value.Minimum;
                Maximum = value.Maximum;
                Value = value.Value;
            }
        }

        public void WriteTo(RangeBase value)
        {
            if (value != null)
            {
                WriteTo((Control)value);
                value.LargeChange = LargeChange;
                value.SmallChange = SmallChange;
                value.Minimum = Minimum;
                value.Maximum =Maximum;
                value.Value = Value;
            }
        }
    }
}
