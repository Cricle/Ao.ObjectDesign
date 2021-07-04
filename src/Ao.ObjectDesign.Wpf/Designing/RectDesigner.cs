using Ao.ObjectDesign.Wpf.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public virtual double Height
        {
            get => height;
            set => Set(ref height, value);
        }

        public virtual double Width
        {
            get => width;
            set => Set(ref width, value);
        }

        public virtual double Y
        {
            get => y;
            set => Set(ref y, value);
        }

        public virtual double X
        {
            get => x;
            set => Set(ref x, value);
        }
        [PlatformTargetProperty]
        public virtual Rect Rect
        {
            get => new Rect(x, y, width, height);
            set
            {
                X = value.X;
                Y = value.Y;
                Width = value.Width;
                Height = value.Height;
            }
        }
    }
}
