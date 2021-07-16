using Ao.ObjectDesign.Wpf.Annotations;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(RadialGradientBrush))]
    public class RadialGradientBrushDesigner : GradientBrushDesigner
    {
        public RadialGradientBrushDesigner()
        {
            PropertyChanged += OnPropertyChanged;
        }


        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (IncludePropertyNames.Contains(e.PropertyName))
            {
                RaiseRadialGradientBrushChange();
            }
        }

        private Point center;
        private double radiusX;
        private double radiusY;
        private Point gradientOrigin;

        public virtual Point GradientOrigin
        {
            get => gradientOrigin;
            set
            {
                Set(ref gradientOrigin, value);
                RaiseRadialGradientBrushChange();
            }
        }

        public virtual double RadiusY
        {
            get => radiusY;
            set
            {
                Set(ref radiusY, value);
                RaiseRadialGradientBrushChange();
            }
        }

        public virtual double RadiusX
        {
            get => radiusX;
            set
            {
                Set(ref radiusX, value);
                RaiseRadialGradientBrushChange();
            }
        }

        public virtual Point Center
        {
            get => center;
            set
            {
                Set(ref center, value);
                RaiseRadialGradientBrushChange();
            }
        }

        [PlatformTargetProperty]
        public virtual RadialGradientBrush RadialGradientBrush
        {
            get
            {
                RadialGradientBrush brush = new RadialGradientBrush
                {
                    GradientOrigin = gradientOrigin,
                    Center = center,
                    RadiusY = radiusY,
                    RadiusX = radiusX
                };
                WriteTo(brush);
                return brush;
            }
            set
            {
                if (value is null)
                {
                    GradientOrigin = new Point();
                    Center = new Point();
                    RadiusX = RadiusY = 0;
                }
                else
                {
                    GradientOrigin = value.GradientOrigin;
                    Center = value.Center;
                    RadiusX = value.RadiusX;
                    RadiusY = value.RadiusY;
                }
                Apply(null);
            }
        }
        protected void RaiseRadialGradientBrushChange()
        {
            RaisePropertyChanged(nameof(RadialGradientBrush));
        }
    }
}
