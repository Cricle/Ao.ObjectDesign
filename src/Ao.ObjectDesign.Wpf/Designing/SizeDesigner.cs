using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(Size))]
    public class SizeDesigner : NotifyableObject
    {
        private double width = double.NaN;
        private double height = double.NaN;
        private double minWidth;
        private double maxWidth = double.PositiveInfinity;
        private double minHeight;
        private double maxHeight = double.PositiveInfinity;

        public virtual double MinHeight
        {
            get => minHeight;
            set
            {
                Set(ref minHeight, value);
                RaiseSizeChanged();
            }
        }

        public virtual double MaxHeight
        {
            get => maxHeight;
            set
            {
                Set(ref maxHeight, value);
                RaiseSizeChanged();
            }
        }


        public virtual double MaxWidth
        {
            get => maxWidth;
            set
            {
                Set(ref maxWidth, value);
                RaiseSizeChanged();
            }
        }

        public virtual double MinWidth
        {
            get => minWidth;
            set
            {
                Set(ref minWidth, value);
                RaiseSizeChanged();
            }
        }

        public virtual double Height
        {
            get => height;
            set
            {
                Set(ref height, value);
                RaiseSizeChanged();
            }
        }
        public virtual double Width
        {
            get => width;
            set
            {
                Set(ref width, value);
                RaiseSizeChanged();
            }
        }
        [PlatformTargetProperty]
        public virtual Size Size
        {
            get
            {
                double w = Clamp(width, minWidth, maxWidth);
                double h = Clamp(height, minHeight, maxHeight);
                return new Size(w, h);
            }
            set
            {
                Width = Clamp(value.Width, minWidth, maxWidth);
                Height = Clamp(value.Height, minHeight, maxHeight);
            }
        }
        protected void RaiseSizeChanged()
        {
            RaisePropertyChanged(nameof(Size));
        }
        protected double Clamp(double value, double min, double max)
        {
            if (min > max)
            {
                return value;
            }
            if (max == double.PositiveInfinity)
            {
                return Math.Max(value, min);
            }
            return Math.Min(Math.Max(value, min), max);
        }
    }
}
