using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System.ComponentModel;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(ScrollViewer))]
    public class ScrollViewerSetting : ControlSetting, IMiddlewareDesigner<ScrollViewer>
    {
        private bool canContentScroll;
        private ScrollBarVisibility horizontalScrollBarVisibility;
        private ScrollBarVisibility verticalScrollBarVisibility;
        private double panningDeceleration;
        private bool isDeferredScrollingEnabled;
        private PanningMode panningMode;
        private double panningRatio;

        [DefaultValue(1d)]
        public virtual double PanningRatio
        {
            get => panningRatio;
            set => Set(ref panningRatio, value);
        }

        [DefaultValue(PanningMode.None)]
        public virtual PanningMode PanningMode
        {
            get => panningMode;
            set => Set(ref panningMode, value);
        }

        [DefaultValue(false)]
        public virtual bool IsDeferredScrollingEnabled
        {
            get => isDeferredScrollingEnabled;
            set => Set(ref isDeferredScrollingEnabled, value);
        }

        [DefaultValue(1/96d)]
        public virtual double PanningDeceleration
        {
            get => panningDeceleration;
            set => Set(ref panningDeceleration, value);
        }


        [DefaultValue(ScrollBarVisibility.Visible)]
        public virtual ScrollBarVisibility VerticalScrollBarVisibility
        {
            get => verticalScrollBarVisibility;
            set => Set(ref verticalScrollBarVisibility, value);
        }

        [DefaultValue(ScrollBarVisibility.Hidden)]
        public virtual ScrollBarVisibility HorizontalScrollBarVisibility
        {
            get => horizontalScrollBarVisibility;
            set => Set(ref horizontalScrollBarVisibility, value);
        }

        [DefaultValue(false)]
        public virtual bool CanContentScroll
        {
            get => canContentScroll;
            set => Set(ref canContentScroll, value);
        }

        public override void SetDefault()
        {
            base.SetDefault();
            CanContentScroll = false;
            HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            PanningDeceleration = 1 / 96d;
            IsDeferredScrollingEnabled = false;
            PanningMode = PanningMode.None;
            PanningRatio = 1;

        }

        public void Apply(ScrollViewer value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((Control)value);
                CanContentScroll = value.CanContentScroll;
                HorizontalScrollBarVisibility = value.HorizontalScrollBarVisibility;
                VerticalScrollBarVisibility = value.VerticalScrollBarVisibility;
                PanningDeceleration = value.PanningDeceleration;
                IsDeferredScrollingEnabled = value.IsDeferredScrollingEnabled;
                PanningMode = value.PanningMode;
                PanningRatio = value.PanningRatio;
            }
        }

        public void WriteTo(ScrollViewer value)
        {
            if (value != null)
            {
                WriteTo((Control)value);
                value.CanContentScroll = CanContentScroll;
                value.HorizontalScrollBarVisibility = HorizontalScrollBarVisibility;
                value.VerticalScrollBarVisibility = VerticalScrollBarVisibility;
                value.PanningDeceleration = PanningDeceleration;
                value.IsDeferredScrollingEnabled = IsDeferredScrollingEnabled;
                value.PanningMode = PanningMode;
                value.PanningRatio = PanningRatio;
            }
        }
    }
}
