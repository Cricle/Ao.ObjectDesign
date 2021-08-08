using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(FontStyle))]
    public class FontStyleDesigner : NotifyableObject
    {
        private bool isItalic;
        private bool isUnerline;
        [PlatformTargetProperty]
        public virtual FontStyle FontStyle
        {
            get => IsItalic ? FontStyles.Italic : FontStyles.Normal;
            set
            {
                IsItalic = value == FontStyles.Italic;
            }
        }

        [DefaultValue(false)]
        public bool IsUnderLine
        {
            get => isUnerline;
            set
            {
                Set(ref isUnerline, value);
            }
        }

        [DefaultValue(false)]
        public bool IsItalic
        {
            get => isItalic;
            set
            {
                Set(ref isItalic, value);
                RaisePropertyChanged(nameof(FontStyle));
            }
        }
    }
}
