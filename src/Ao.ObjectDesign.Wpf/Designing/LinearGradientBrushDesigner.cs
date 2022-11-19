using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Ao.ObjectDesign.Designing
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
        [PlatformTargetGetMethod]
        public virtual LinearGradientBrush GetLinearGradientBrush()
        {
            LinearGradientBrush brush = new LinearGradientBrush
            {
                StartPoint = startPoint?.GetPoint() ?? default(Point),
                EndPoint = endPoint?.GetPoint() ?? default(Point)
            };
            WriteTo(brush);
            return brush;
        }
        [PlatformTargetSetMethod]
        public virtual void SetLinearGradientBrush(LinearGradientBrush value)
        {
            if (value is null)
            {
                StartPoint = new PointDesigner();
                EndPoint = new PointDesigner();
            }
            else
            {
                StartPoint = new PointDesigner();
                StartPoint.SetPoint(value.StartPoint);
                EndPoint = new PointDesigner();
                EndPoint.SetPoint(value.EndPoint);
            }
            Apply(null);
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
