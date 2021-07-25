using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.Windows.Media;
namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(ScaleTransform))]
    public class ScaleTransformDesigner : NotifyableObject
    {
        private double centerX;
        private double centerY;
        private double scaleX;
        private double scaleY;

        [PlatformTargetProperty]
        public virtual ScaleTransform ScaleTransform
        {
            get => new ScaleTransform(scaleX, scaleY, centerX, centerY);
            set
            {
                if (value is null)
                {
                    scaleX = scaleY = centerY = centerX = 0;
                }
                else
                {
                    ScaleX = value.ScaleX;
                    ScaleY = value.ScaleY;
                    CenterX = value.CenterX;
                    CenterY = value.CenterY;
                }
            }
        }

        public double ScaleX
        {
            get => scaleX;
            set
            {
                Set(ref scaleX, value);
                RaiseRotateTransformChanged();
            }
        }
        public double ScaleY
        {
            get => scaleY;
            set
            {
                Set(ref scaleY, value);
                RaiseRotateTransformChanged();
            }
        }
        public double CenterX
        {
            get => centerX;
            set
            {
                Set(ref centerX, value);
                RaiseRotateTransformChanged();
            }
        }

        public double CenterY
        {
            get => centerY;
            set
            {
                Set(ref centerY, value);
                RaiseRotateTransformChanged();
            }
        }
        protected void RaiseRotateTransformChanged()
        {
            RaisePropertyChanged(nameof(RotateTransform));
        }

    }
}
