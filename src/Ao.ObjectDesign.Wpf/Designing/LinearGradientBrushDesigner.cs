using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(LinearGradientBrush))]
    public class LinearGradientBrushDesigner : GradientBrushDesigner
    {
        public LinearGradientBrushDesigner()
        {
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (IncludePropertyNames.Contains(e.PropertyName))
            {
                RaiseLinearGradientBrushChange();
            }
        }

        private PointDesigner startPoint;
        private PointDesigner endPoint;

        [DefaultValue(null)]
        public virtual PointDesigner EndPoint
        {
            get => endPoint;
            set
            {
                if (endPoint != null)
                {
                    endPoint.PropertyChanged -= OnPointPropertyChanged;
                }

                if (value != null)
                {
                    value.PropertyChanged -= OnPointPropertyChanged;
                }
                Set(ref endPoint, value);
                RaiseLinearGradientBrushChange();
            }
        }

        [DefaultValue(null)]
        public virtual PointDesigner StartPoint
        {
            get => startPoint;
            set
            {
                if (startPoint != null)
                {
                    startPoint.PropertyChanged -= OnPointPropertyChanged;
                }

                if (value != null)
                {
                    value.PropertyChanged -= OnPointPropertyChanged;
                }
                Set(ref startPoint, value);
                RaiseLinearGradientBrushChange();
            }
        }
        [PlatformTargetProperty]
        public virtual LinearGradientBrush LinearGradientBrush
        {
            get
            {
                LinearGradientBrush brush = new LinearGradientBrush
                {
                    StartPoint = startPoint?.Point ?? default(Point),
                    EndPoint = endPoint?.Point ?? default(Point)
                };
                WriteTo(brush);
                return brush;
            }
            set
            {
                if (value is null)
                {
                    StartPoint = new PointDesigner();
                    EndPoint = new PointDesigner();
                }
                else
                {
                    StartPoint = new PointDesigner { Point = value.StartPoint };
                    EndPoint = new PointDesigner { Point = value.EndPoint };
                }
                Apply(null);
            }
        }
        private void OnPointPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaiseLinearGradientBrushChange();
        }

        private static readonly PropertyChangedEventArgs linearGradientBrushChangedEventArgs = new PropertyChangedEventArgs(nameof(LinearGradientBrush));
        protected void RaiseLinearGradientBrushChange()
        {
            RaisePropertyChanged(linearGradientBrushChangedEventArgs);
        }
    }
}
