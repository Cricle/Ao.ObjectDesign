using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(GradientStop))]
    public class GradientStopDesigner : NotifyableObject
    {
        private static readonly PropertyChangedEventArgs gradientStopChangedEventArgs = new PropertyChangedEventArgs(nameof(GradientStop));
        private ColorDesigner color;
        private double offset;

        public GradientStopDesigner()
        {
        }
        public GradientStopDesigner(ColorDesigner color, double offset)
        {
            Color = color;
            Offset = offset;
        }

        public GradientStopDesigner(Color color, double offset)
        {
            Color = new ColorDesigner { Color = color };
            Offset = offset;
        }

        [DefaultValue(0d)]
        public virtual double Offset
        {
            get => offset;
            set
            {
                Set(ref offset, value);
                RaiseGradientStopChanged();
            }
        }

        [DefaultValue(null)]
        public virtual ColorDesigner Color
        {
            get => color;
            set
            {
                if (color != null)
                {
                    color.PropertyChanged -= OnColorPropertyChanged;
                }
                if (value != null)
                {
                    value.PropertyChanged += OnColorPropertyChanged;
                }
                Set(ref color, value);
                RaiseGradientStopChanged();
            }
        }
        private void OnColorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaiseGradientStopChanged();
        }

        [PlatformTargetProperty]
        public virtual GradientStop GradientStop
        {
            get => new GradientStop(color?.Color ?? Colors.Transparent, offset);
            set
            {
                if (value is null)
                {
                    Offset = 0;
                    Color = default;
                }
                else
                {
                    Offset = value.Offset;
                    Color = new ColorDesigner { Color = value.Color };
                }
            }
        }
        protected void RaiseGradientStopChanged()
        {
            RaisePropertyChanged(gradientStopChangedEventArgs);
        }
    }
}
