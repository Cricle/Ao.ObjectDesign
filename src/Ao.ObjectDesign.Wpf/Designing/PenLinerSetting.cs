using System.Windows;

namespace Ao.ObjectDesign.Wpf.Designing
{
    public class PenLinerSetting : PenGradientSetting
    {
        private Point startPoint;
        private Point endPoint;

        public virtual Point EndPoint
        {
            get => endPoint;
            set => Set(ref endPoint, value);
        }

        public virtual Point StartPoint
        {
            get => startPoint;
            set => Set(ref startPoint, value);
        }
    }
}
