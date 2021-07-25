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

        private Point startPoint;
        private Point endPoint;

        public virtual Point EndPoint
        {
            get => endPoint;
            set
            {
                Set(ref endPoint, value);
                RaiseLinearGradientBrushChange();
            }
        }

        public virtual Point StartPoint
        {
            get => startPoint;
            set
            {
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
                    StartPoint = startPoint,
                    EndPoint = endPoint
                };
                WriteTo(brush);
                return brush;
            }
            set
            {
                if (value is null)
                {
                    StartPoint = new Point();
                    EndPoint = new Point();
                }
                else
                {
                    StartPoint = value.StartPoint;
                    EndPoint = value.EndPoint;
                }
                Apply(null);
            }
        }
        protected void RaiseLinearGradientBrushChange()
        {
            RaisePropertyChanged(nameof(LinearGradientBrush));
        }
    }
}
