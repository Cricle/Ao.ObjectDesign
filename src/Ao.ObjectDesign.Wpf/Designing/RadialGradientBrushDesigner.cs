using Ao.ObjectDesign.Designing.Annotations;
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

        private PointDesigner center;
        private double radiusX;
        private double radiusY;
        private PointDesigner gradientOrigin;

        public virtual PointDesigner GradientOrigin
        {
            get => gradientOrigin;
            set
            {
                if (gradientOrigin != null)
                {
                    gradientOrigin.PropertyChanged -= OnPointPropertyChanged;
                }
                if (value != null)
                {
                    value.PropertyChanged += OnPointPropertyChanged;
                }
                Set(ref gradientOrigin, value);
                RaiseRadialGradientBrushChange();
            }
        }

        [DefaultValue(0.5d)]
        public virtual double RadiusY
        {
            get => radiusY;
            set
            {
                Set(ref radiusY, value);
                RaiseRadialGradientBrushChange();
            }
        }

        [DefaultValue(0.5d)]
        public virtual double RadiusX
        {
            get => radiusX;
            set
            {
                Set(ref radiusX, value);
                RaiseRadialGradientBrushChange();
            }
        }

        public virtual PointDesigner Center
        {
            get => center;
            set
            {
                if (center != null)
                {
                    center.PropertyChanged -= OnPointPropertyChanged;
                }
                if (value != null)
                {
                    value.PropertyChanged += OnPointPropertyChanged;
                }
                Set(ref center, value);
                RaiseRadialGradientBrushChange();
            }
        }

        private void OnPointPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaiseRadialGradientBrushChange();
        }
        [PlatformTargetGetMethod]
        public virtual RadialGradientBrush GetRadialGradientBrush()
        {
            RadialGradientBrush brush = new RadialGradientBrush
            {
                GradientOrigin = gradientOrigin?.GetPoint() ?? default,
                Center = center?.GetPoint() ?? default,
                RadiusY = radiusY,
                RadiusX = radiusX
            };
            WriteTo(brush);
            return brush;
        }
        [PlatformTargetSetMethod]
        public virtual void SetRadialGradientBrush(RadialGradientBrush value)
        {
            if (value is null)
            {
                GradientOrigin = new PointDesigner();
                Center = new PointDesigner();
                RadiusX = RadiusY = 0;
            }
            else
            {
                GradientOrigin = new PointDesigner();
                GradientOrigin.SetPoint(value.GradientOrigin);
                Center = new PointDesigner();
                Center.SetPoint(value.Center);
                RadiusX = value.RadiusX;
                RadiusY = value.RadiusY;
            }
            Apply(value);
        }

        private static readonly PropertyChangedEventArgs radialGradientBrushEventArgs = new PropertyChangedEventArgs(nameof(RadialGradientBrush));
        protected void RaiseRadialGradientBrushChange()
        {
            RaisePropertyChanged(radialGradientBrushEventArgs);
        }
    }
}
