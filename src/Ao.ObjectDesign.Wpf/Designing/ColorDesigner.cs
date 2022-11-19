using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows.Media;

namespace Ao.ObjectDesign.Designing
{
    [DesignFor(typeof(Color))]
    public class ColorDesigner : NotifyableObject
    {
        private byte r;
        private byte g;
        private byte b;
        private byte a;

        [DefaultValue((byte)0)]
        public virtual byte A
        {
            get => a;
            set
            {
                Set(ref a, value);
                RaiseColorChanged();
            }
        }
        [DefaultValue((byte)0)]
        public virtual byte B
        {
            get => b;
            set
            {
                Set(ref b, value);
                RaiseColorChanged();
            }
        }

        [DefaultValue((byte)0)]
        public virtual byte G
        {
            get => g;
            set
            {
                Set(ref g, value);
                RaiseColorChanged();
            }
        }
        [DefaultValue((byte)0)]
        public virtual byte R
        {
            get => r;
            set
            {
                Set(ref r, value);
                RaiseColorChanged();
            }
        }

        [PlatformTargetGetMethod]
        public virtual Color GetColor()
        {
            return new Color { A = a, R = r, G = g, B = b };
        }
        [PlatformTargetSetMethod]
        public virtual void SetColor(Color value)
        {
            A = value.A;
            R = value.R;
            G = value.G;
            B = value.B;
        }
        private static readonly PropertyChangedEventArgs colorChangedEventArgs = new PropertyChangedEventArgs(nameof(Color));
        protected void RaiseColorChanged()
        {
            RaisePropertyChanged(colorChangedEventArgs);
        }
    }
}
