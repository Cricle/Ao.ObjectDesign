using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows.Media;
namespace Ao.ObjectDesign.Designing
{
    [DesignFor(typeof(ScaleTransform))]
    public class ScaleTransformDesigner : TransformDesigner
    {
        private double centerX;
        private double centerY;
        private double scaleX;
        private double scaleY;
        [PlatformTargetGetMethod]
        public virtual ScaleTransform GetScaleTransform()
        {
            return new ScaleTransform(scaleX, scaleY, centerX, centerY);
        }
        [PlatformTargetSetMethod]
        public virtual void SetScaleTransform(ScaleTransform value)
        {
            if (value is null)
            {
                ScaleX = ScaleY = 1;
                CenterY = CenterX = 0;
            }
            else
            {
                ScaleX = value.ScaleX;
                ScaleY = value.ScaleY;
                CenterX = value.CenterX;
                CenterY = value.CenterY;
            }
        }

        [DefaultValue(1d)]
        public double ScaleX
        {
            get => scaleX;
            set
            {
                Set(ref scaleX, value);
                RaiseRotateTransformChanged();
            }
        }
        [DefaultValue(1d)]
        public double ScaleY
        {
            get => scaleY;
            set
            {
                Set(ref scaleY, value);
                RaiseRotateTransformChanged();
            }
        }
        [DefaultValue(0d)]
        public double CenterX
        {
            get => centerX;
            set
            {
                Set(ref centerX, value);
                RaiseRotateTransformChanged();
            }
        }

        [DefaultValue(0d)]
        public double CenterY
        {
            get => centerY;
            set
            {
                Set(ref centerY, value);
                RaiseRotateTransformChanged();
            }
        }
        private static readonly PropertyChangedEventArgs rotateTransformEventArgs = new PropertyChangedEventArgs(nameof(RotateTransform));
        protected void RaiseRotateTransformChanged()
        {
            RaisePropertyChanged(rotateTransformEventArgs);
        }

    }
}
