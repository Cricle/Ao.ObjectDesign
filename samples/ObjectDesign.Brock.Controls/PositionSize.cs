using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using System;
using System.ComponentModel;
using System.Windows;

namespace ObjectDesign.Brock.Components
{
    public class PositionSize : NotifyableObject
    {
        private double width;
        private double height;

        private double x;
        private double y;


        
        public double Width
        {
            get => width;
            set
            {
                value = Math.Max(0, value);
                value = Math.Round(value, 2);
                Set(ref width, value);
            }
        }
        
        public double Height
        {
            get => height;
            set
            {
                value = Math.Max(0, value);
                value = Math.Round(value, 2);
                Set(ref height, value);
            }
        }
        
        public double X
        {
            get => x;
            set
            {
                value = Math.Round(value, 2);
                Set(ref x, value);
            }
        }
        
        public double Y
        {
            get => y;
            set
            {
                value = Math.Round(value, 2);
                Set(ref y, value);
            }
        }


        public IRect GetBoundsSimple()
        {
            return new DefaultRect(x, y, x + width, y + height);
        }
        public Rect GetBounds()
        {
            return new Rect(x, y, width, height);
        }
        public void SetDefault()
        {
            X = Y = 0;
            Width = Height = 200;
        }

    }
}
