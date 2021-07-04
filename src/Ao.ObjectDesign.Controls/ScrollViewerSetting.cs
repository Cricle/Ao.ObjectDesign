using Ao.ObjectDesign.Wpf.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(ScrollViewer))]
    public class ScrollViewerSetting : ControlSetting,IMiddlewareDesigner<ScrollViewer>
    {
        private bool canContentScroll;
        private ScrollBarVisibility horizontalScrollBarVisibility= ScrollBarVisibility.Hidden;
        private ScrollBarVisibility verticalScrollBarVisibility= ScrollBarVisibility.Visible;
        private double panningDeceleration = 1 / 96d;
        private bool isDeferredScrollingEnabled;
        private PanningMode panningMode;
        private double panningRatio = 1;

        public virtual double PanningRatio
        {
            get => panningRatio;
            set => Set(ref panningRatio, value);
        }

        public virtual PanningMode PanningMode
        {
            get => panningMode;
            set => Set(ref panningMode, value);
        }

        public virtual bool IsDeferredScrollingEnabled
        {
            get => isDeferredScrollingEnabled;
            set => Set(ref isDeferredScrollingEnabled, value);
        }

        public virtual double PanningDeceleration
        {
            get => panningDeceleration;
            set => Set(ref panningDeceleration, value);
        }


        public virtual ScrollBarVisibility VerticalScrollBarVisibility
        {
            get => verticalScrollBarVisibility;
            set => Set(ref verticalScrollBarVisibility, value);
        }

        public virtual ScrollBarVisibility HorizontalScrollBarVisibility
        {
            get => horizontalScrollBarVisibility;
            set => Set(ref horizontalScrollBarVisibility, value);
        }

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
