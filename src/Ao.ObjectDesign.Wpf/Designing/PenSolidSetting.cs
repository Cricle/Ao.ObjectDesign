using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    public class PenSolidSetting : NotifyableObject
    {
        private Color? color;

        public virtual Color? Color
        {
            get { return color; }
            set => Set(ref color, value);
        }
    }
}
