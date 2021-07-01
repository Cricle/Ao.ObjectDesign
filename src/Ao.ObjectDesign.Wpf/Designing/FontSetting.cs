using Ao.ObjectDesign.Wpf.Annotations;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(FontWeight))]
    [DesignFor(typeof(FontStyle))]
    [DesignFor(typeof(FontFamily))]
    public class FontSetting : NotifyableObject
    {
        private static FontFamily[] installedFontFamilies;
        public static IReadOnlyDictionary<string, FontFamily> InstalledFontFamilyMap =>
            InstalledFontFamilies.ToDictionary(x => x.Source);
        public static IReadOnlyCollection<FontFamily> InstalledFontFamilies
        {
            get
            {
                if (installedFontFamilies is null)
                {
                    using (var coll = new InstalledFontCollection())
                    {
                        installedFontFamilies = coll.Families
                            .Select(x => new FontFamily(x.Name))
                            .ToArray();
                    }
                }
                return installedFontFamilies;
            }
        }

        private static readonly IReadOnlyDictionary<PenFontWeights, FontWeight> fontWeightMap = new Dictionary<PenFontWeights, FontWeight>
        {
            [PenFontWeights.Black] = FontWeights.Black,
            [PenFontWeights.Bold] = FontWeights.Bold,
            [PenFontWeights.DemiBold] = FontWeights.DemiBold,
            [PenFontWeights.ExtraBlack] = FontWeights.ExtraBlack,
            [PenFontWeights.ExtraBold] = FontWeights.ExtraBold,
            [PenFontWeights.ExtraLight] = FontWeights.ExtraLight,
            [PenFontWeights.Heavy] = FontWeights.Heavy,
            [PenFontWeights.Light] = FontWeights.Light,
            [PenFontWeights.Medium] = FontWeights.Medium,
            [PenFontWeights.Normal] = FontWeights.Normal,
            [PenFontWeights.Regular] = FontWeights.Regular,
            [PenFontWeights.SemiBold] = FontWeights.SemiBold,
            [PenFontWeights.Thin] = FontWeights.Thin,
            [PenFontWeights.UltraBlack] = FontWeights.UltraBlack,
            [PenFontWeights.UltraBold] = FontWeights.UltraBold,
            [PenFontWeights.UltraLight] = FontWeights.UltraLight,
        };

        private static PenFontWeights GetFontWeight(FontWeight fontWeight)
        {
            return fontWeightMap.FirstOrDefault(x => x.Value == fontWeight)
                .Key;
        }

        private string fontName;
        private double fontSize = 12;
        private bool isBold;
        private bool isItalic;
        private bool isUnerline;
        private PenFontWeights penFontWeight;

        [PlatformTargetProperty]
        public virtual FontWeight FontWeight
        {
            get
            {
                if (fontWeightMap.TryGetValue(PenFontWeight, out var w))
                {
                    return w;
                }
                return FontWeights.Normal;
            }
            set
            {
                PenFontWeight = GetFontWeight(value);
            }

        }

        public PenFontWeights PenFontWeight
        {
            get => penFontWeight;
            set
            {
                Set(ref penFontWeight, value);
                RaisePropertyChanged(nameof(FontWeight));
            }
        }
        [PlatformTargetProperty]
        public virtual FontStyle FontStyle
        {
            get => IsItalic ? FontStyles.Italic : FontStyles.Normal;
            set
            {
                IsItalic = value == FontStyles.Italic;
            }
        }

        public bool IsUnderLine
        {
            get => isUnerline;
            set
            {
                Set(ref isUnerline, value);
            }
        }

        public bool IsItalic
        {
            get => isItalic;
            set
            {
                Set(ref isItalic, value);
                RaisePropertyChanged(nameof(FontStyle));
            }
        }

        public bool IsBold
        {
            get => isBold;
            set
            {
                Set(ref isBold, value);
                PenFontWeight = value ? PenFontWeights.Bold : PenFontWeights.Normal;
            }
        }

        public double FontSize
        {
            get => fontSize;
            set => Set(ref fontSize, value);
        }
        [PlatformTargetProperty]
        public virtual FontFamily FontFamily
        {
            get
            {
                if (string.IsNullOrEmpty(fontName))
                {
                    return null;
                }
                return new FontFamily(fontName);
            }
            set
            {
                if (value is null)
                {
                    FontName = null;
                }
                else
                {
                    FontName = value.Source;
                }
            }
        }

        public string FontName
        {
            get => fontName;
            set
            {
                Set(ref fontName, value);
                RaisePropertyChanged(nameof(FontFamily));
            }
        }
    }
}
