using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows.Media;
namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(TranslateTransform))]
    public class TranslateTransformDesigner : TransformDesigner
    {
        private double x;
        private double y;
        [PlatformTargetGetMethod]
        public virtual TranslateTransform GetTranslateTransform()
        {
            return new TranslateTransform(x, y);
        }
        [PlatformTargetSetMethod]
        public virtual void SetTranslateTransform(TranslateTransform value)
        {
            if (value is null)
            {
                x = y = 0;
            }
            else
            {
                X = value.X;
                Y = value.Y;
            }
        }

        [DefaultValue(0d)]
        public double X
        {
            get => x;
            set
            {
                Set(ref x, value);
                RaiseTranslateTransformChanged();
            }
        }

        [DefaultValue(0d)]
        public double Y
        {
            get => y;
            set
            {
                Set(ref y, value);
                RaiseTranslateTransformChanged();
            }
        }


        private static readonly PropertyChangedEventArgs translateTransformEventArgs = new PropertyChangedEventArgs(nameof(TranslateTransform));
        protected void RaiseTranslateTransformChanged()
        {
            RaisePropertyChanged(translateTransformEventArgs);
        }
    }
}
