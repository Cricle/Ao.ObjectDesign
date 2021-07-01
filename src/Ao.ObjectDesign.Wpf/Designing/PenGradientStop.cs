using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    public class PenGradientStop : NotifyableObject
    {
        private Color color;
        private double offset;

        public PenGradientStop()
        {
        }

        public PenGradientStop(Color color, double offset)
        {
            Color = color;
            Offset = offset;
        }

        public virtual double Offset
        {
            get => offset;
            set => Set(ref offset, value);
        }

        public virtual Color Color
        {
            get => color;
            set => Set(ref color, value);
        }
    }
}
