using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows.Media;

namespace Ao.ObjectDesign.Designing
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
            Color = new ColorDesigner();
            Color.SetColor(color);
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
        [PlatformTargetGetMethod]
        public virtual GradientStop GetGradientStop()
        {
            var stop = new GradientStop(color?.GetColor() ?? Colors.Transparent, offset);

            return stop;
        }
        [PlatformTargetSetMethod]
        public virtual void SetGradientStop(GradientStop value)
        {
            if (value is null)
            {
                Offset = 0;
                Color = default;
            }
            else
            {
                Offset = value.Offset;
                Color = new ColorDesigner();
                Color.SetColor(value.Color);
            }
        }

        protected void RaiseGradientStopChanged()
        {
            RaisePropertyChanged(gradientStopChangedEventArgs);
        }
    }
}
