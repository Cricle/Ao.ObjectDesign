using System.Windows;

namespace Ao.ObjectDesign.Wpf.Designing
{
    public class PenRadialSetting : PenGradientSetting
    {
        private Point center;
        private double radiusX;
        private double radiusY;
        private Point gradientOrigin;

        public virtual Point GradientOrigin
        {
            get => gradientOrigin;
            set => Set(ref gradientOrigin, value);
        }

        public virtual double RadiusY
        {
            get => radiusY;
            set => Set(ref radiusY, value);
        }

        public virtual double RadiusX
        {
            get => radiusX;
            set => Set(ref radiusX, value);
        }

        public virtual Point Center
        {
            get => center;
            set => Set(ref center, value);
        }
    }
}
