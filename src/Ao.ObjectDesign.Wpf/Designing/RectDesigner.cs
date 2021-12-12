using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(Rect))]
    public class RectDesigner : NotifyableObject
    {
        private double x;
        private double y;
        private double width;
        private double height;

        [DefaultValue(0d)]
        public virtual double Height
        {
            get => height;
            set
            {
                Set(ref height, value);
                RaiseRectChanged();
            }
        }

        [DefaultValue(0d)]
        public virtual double Width
        {
            get => width;
            set
            {
                Set(ref width, value);
                RaiseRectChanged();
            }
        }

        [DefaultValue(0d)]
        public virtual double Y
        {
            get => y;
            set
            {
                Set(ref y, value);
                RaiseRectChanged();
            }
        }

        [DefaultValue(0d)]
        public virtual double X
        {
            get => x;
            set
            {
                Set(ref x, value);
                RaiseRectChanged();
            }
        }
        [PlatformTargetGetMethod]
        public virtual Rect GetRect()
        {
            return new Rect(x, y, width, height);
        }
        [PlatformTargetSetMethod]
        public virtual void SetRect(Rect value)
        {
            X = value.X;
            Y = value.Y;
            Width = value.Width;
            Height = value.Height;
        }

        private static readonly PropertyChangedEventArgs rectEventArgs = new PropertyChangedEventArgs(nameof(Rect));
        protected void RaiseRectChanged()
        {
            RaisePropertyChanged(rectEventArgs);
        }
    }
}
