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
        public static readonly IReadOnlyDictionary<FontStretchTypes, FontStretch> fontStretchMap =
            FrozenDictionary<FontStretchTypes, FontStretch>.Create(new Dictionary<FontStretchTypes, FontStretch>
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
            });

        public static readonly IReadOnlyDictionary<FontStretch, FontStretchTypes> fontStretchRevMap =
            FrozenDictionary<FontStretch, FontStretchTypes>.Create(fontStretchMap,
                x => x.Value,
                x => x.Key,
                null);

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


        [PlatformTargetGetMethod]
        public virtual FontStretch GetFontStretch()
        {
            return fontStretchMap[fontStretchType];
        }
        [PlatformTargetSetMethod]
        public virtual void SetFontStretch(FontStretch value)
        {
            FontStretchType = fontStretchRevMap[value];
        }
    }
}
