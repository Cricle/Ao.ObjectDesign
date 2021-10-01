using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows.Media;
namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(TranslateTransform))]
    public class TranslateTransformDesigner : NotifyableObject
    {
        private double x;
        private double y;
        [PlatformTargetProperty]
        public virtual TranslateTransform TranslateTransform
        {
            get => new TranslateTransform(x, y);
            set
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
