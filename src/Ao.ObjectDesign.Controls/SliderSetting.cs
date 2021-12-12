using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(Slider))]
    public class SliderSetting : RangeBaseSetting, IMiddlewareDesigner<Slider>
    {
        private bool isSnapToTickEnabled;
        private int autoToolTipPrecision;
        private AutoToolTipPlacement autoToolTipPlacement;
        private int interval;
        private int delay;
        private bool isDirectionReversed;
        private Orientation orientation = Orientation.Horizontal;
        private double tickFrequency = 1.0;
        private DoubleCollectionDesigner ticks;
        private double selectionStart;
        private TickPlacement tickPlacement;
        private bool isSelectionRangeEnabled;
        private bool isMoveToPointEnabled;
        private double selectionEnd;

        [DefaultValue(0d)]
        public virtual double SelectionEnd
        {
            get => selectionEnd;
            set => Set(ref selectionEnd, value);
        }

        [DefaultValue(false)]
        public virtual bool IsMoveToPointEnabled
        {
            get => isMoveToPointEnabled;
            set => Set(ref isMoveToPointEnabled, value);
        }

        [DefaultValue(false)]
        public virtual bool IsSelectionRangeEnabled
        {
            get => isSelectionRangeEnabled;
            set => Set(ref isSelectionRangeEnabled, value);
        }

        [DefaultValue(TickPlacement.None)]
        public virtual TickPlacement TickPlacement
        {
            get => tickPlacement;
            set => Set(ref tickPlacement, value);
        }

        [DefaultValue(0d)]
        public virtual double SelectionStart
        {
            get => selectionStart;
            set => Set(ref selectionStart, value);
        }

        public virtual DoubleCollectionDesigner Ticks
        {
            get => ticks;
            set => Set(ref ticks, value);
        }

        [DefaultValue(1d)]
        public virtual double TickFrequency
        {
            get => tickFrequency;
            set => Set(ref tickFrequency, value);
        }

        [DefaultValue(Orientation.Horizontal)]
        public virtual Orientation Orientation
        {
            get => orientation;
            set => Set(ref orientation, value);
        }

        [DefaultValue(false)]
        public virtual bool IsDirectionReversed
        {
            get => isDirectionReversed;
            set => Set(ref isDirectionReversed, value);
        }

        [DefaultValue(1)]
        public virtual int Delay
        {
            get => delay;
            set => Set(ref delay, value);
        }

        [DefaultValue(1)]
        public virtual int Interval
        {
            get => interval;
            set => Set(ref interval, value);
        }


        [DefaultValue(AutoToolTipPlacement.None)]
        public virtual AutoToolTipPlacement AutoToolTipPlacement
        {
            get => autoToolTipPlacement;
            set => Set(ref autoToolTipPlacement, value);
        }

        [DefaultValue(0)]
        public virtual int AutoToolTipPrecision
        {
            get => autoToolTipPrecision;
            set => Set(ref autoToolTipPrecision, value);
        }

        [DefaultValue(false)]
        public virtual bool IsSnapToTickEnabled
        {
            get => isSnapToTickEnabled;
            set => Set(ref isSnapToTickEnabled, value);
        }

        public override void SetDefault()
        {
            base.SetDefault();
            IsSnapToTickEnabled = false;
            AutoToolTipPrecision = 0;
            AutoToolTipPlacement = AutoToolTipPlacement.None;
            Interval = 1;
            Delay = 1;
            IsDirectionReversed = false;
            Orientation = Orientation.Horizontal;
            TickFrequency = 1;
            Ticks = null;
            SelectionStart = 0;
            TickPlacement = TickPlacement.None;
            IsSelectionRangeEnabled = false;
            IsMoveToPointEnabled = false;
            SelectionEnd = 0;
        }

        public void Apply(Slider value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((RangeBase)value);
                IsSnapToTickEnabled = value.IsSnapToTickEnabled;
                AutoToolTipPrecision = value.AutoToolTipPrecision;
                AutoToolTipPlacement = value.AutoToolTipPlacement;
                Interval = value.Interval;
                Delay = value.Delay;
                IsDirectionReversed = value.IsDirectionReversed;
                Orientation = value.Orientation;
                TickFrequency = value.TickFrequency;
                Ticks = new DoubleCollectionDesigner();
                Ticks.SetDoubleCollection(value.Ticks);
                SelectionStart = value.SelectionStart;
                TickPlacement = value.TickPlacement;
                IsSelectionRangeEnabled = value.IsSelectionRangeEnabled;
                IsMoveToPointEnabled = value.IsMoveToPointEnabled;
                SelectionEnd = value.SelectionEnd;
            }
        }

        public void WriteTo(Slider value)
        {
            if (value != null)
            {
                WriteTo((RangeBase)value);
                value.IsSnapToTickEnabled = isSnapToTickEnabled;
                value.AutoToolTipPrecision = autoToolTipPrecision;
                value.AutoToolTipPlacement = autoToolTipPlacement;
                value.Interval = interval;
                value.Delay = delay;
                value.IsDirectionReversed = isDirectionReversed;
                value.Orientation = orientation;
                value.TickFrequency = tickFrequency;
                value.Ticks = ticks?.GetDoubleCollection();
                value.SelectionStart = selectionStart;
                value.TickPlacement = tickPlacement;
                value.IsSelectionRangeEnabled = isSelectionRangeEnabled;
                value.IsMoveToPointEnabled = isMoveToPointEnabled;
                value.SelectionEnd = selectionEnd;
            }
        }
    }
}
