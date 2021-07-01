using Ao.ObjectDesign.Wpf.Annotations;
using System.Windows.Media;
namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(TranslateTransform))]
    public class TranslateTransformSetting : NotifyableObject
    {
        private double x;
        private double y;
        [PlatformTargetProperty]
        public virtual TranslateTransform TranslateTransform
        {
            get => new TranslateTransform(x,y);
            set
            {
                if (value is null)
                {
                    x = y = 0;
                }
                else
                {
                    X = value.X;
                    Y= value.Y;
                }
            }
        }

        public double X
        {
            get => x;
            set
            {
                Set(ref x, value);
                RaiseTranslateTransformChanged();
            }
        }

        public double Y
        {
            get => y;
            set
            {
                Set(ref y, value);
                RaiseTranslateTransformChanged();
            }
        }


        private void RaiseTranslateTransformChanged()
        {
            RaisePropertyChanged(nameof(TranslateTransform));
        }
    }
}
