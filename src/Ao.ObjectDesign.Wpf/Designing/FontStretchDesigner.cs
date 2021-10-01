using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(FontStretch))]
    public class FontStretchDesigner : NotifyableObject
    {
        public static readonly IReadOnlyDictionary<FontStretchTypes, FontStretch> fontStretchMap = new Dictionary<FontStretchTypes, FontStretch>
        {
            [FontStretchTypes.Normal] = FontStretches.Normal,
            [FontStretchTypes.UltraCondensed] = FontStretches.UltraCondensed,
            [FontStretchTypes.ExtraCondensed] = FontStretches.ExtraCondensed,
            [FontStretchTypes.Condensed] = FontStretches.Condensed,
            [FontStretchTypes.SemiCondensed] = FontStretches.SemiCondensed,
            [FontStretchTypes.SemiExpanded] = FontStretches.SemiExpanded,
            [FontStretchTypes.Expanded] = FontStretches.Expanded,
            [FontStretchTypes.ExtraExpanded] = FontStretches.ExtraExpanded,
            [FontStretchTypes.UltraExpanded] = FontStretches.UltraExpanded,
        };

        public static readonly IReadOnlyDictionary<FontStretch, FontStretchTypes> fontStretchRevMap =
            fontStretchMap.ToDictionary(x => x.Value, x => x.Key);

        private static readonly PropertyChangedEventArgs fontStretchChangedEventArgs = new PropertyChangedEventArgs(nameof(FontStretch));
        private FontStretchTypes fontStretchType;

        [DefaultValue(FontStretchTypes.Normal)]
        public virtual FontStretchTypes FontStretchType
        {
            get => fontStretchType;
            set
            {
                Set(ref fontStretchType, value);
                RaisePropertyChanged(fontStretchChangedEventArgs);
            }
        }


        [PlatformTargetProperty]
        public virtual FontStretch FontStretch
        {
            get => fontStretchMap[fontStretchType];
            set
            {
                FontStretchType = fontStretchRevMap[value];
            }
        }
    }
}
