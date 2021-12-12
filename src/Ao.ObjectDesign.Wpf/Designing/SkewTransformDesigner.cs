using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(SkewTransform))]
    public class SkewTransformDesigner : NotifyableObject
    {
        private double angleX;
        private double angleY;
        private double centerX;
        private double centerY;

        [DefaultValue(0d)]
        public virtual double CenterY
        {
            get => centerY;
            set
            {
                Set(ref centerY, value);
                RaiseSkewTransformChanged();
            }
        }

        [DefaultValue(0d)]
        public virtual double CenterX
        {
            get => centerX;
            set
            {
                Set(ref centerX, value);
                RaiseSkewTransformChanged();
            }
        }

        [DefaultValue(0d)]
        public virtual double AngleY
        {
            get => angleY;
            set
            {
                Set(ref angleY, value);
                RaiseSkewTransformChanged();
            }
        }

        [DefaultValue(0d)]
        public virtual double AngleX
        {
            get => angleX;
            set
            {
                Set(ref angleX, value);
                RaiseSkewTransformChanged();
            }
        }
        [PlatformTargetGetMethod]
        public virtual SkewTransform GetSkewTransform()
        {
            return new SkewTransform(angleX, angleY, centerX, centerY);
        }
        [PlatformTargetSetMethod]
        public virtual void SetSkewTransform(SkewTransform value)
        {
            if (value is null)
            {
                AngleX = AngleY = CenterX = CenterY = 0;
            }
            else
            {
                AngleX = value.AngleX;
                AngleY = value.AngleY;
                CenterX = value.CenterX;
                CenterY = value.CenterY;
            }
        }


        private static readonly PropertyChangedEventArgs skewTransformEventArgs = new PropertyChangedEventArgs(nameof(SkewTransform));
        private void RaiseSkewTransformChanged()
        {
            RaisePropertyChanged(skewTransformEventArgs);
        }
    }
}
