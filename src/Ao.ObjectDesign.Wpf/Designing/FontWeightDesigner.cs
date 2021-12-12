using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(FontWeight))]
    public class FontWeightDesigner : NotifyableObject
    {
        public static readonly IReadOnlyDictionary<PenFontWeights, FontWeight> fontWeightMap =
            FrozenDictionary<PenFontWeights, FontWeight>.Create(new Dictionary<PenFontWeights, FontWeight>
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
            });
        public static readonly IReadOnlyList<PenFontWeights> KnowPenFontWeights = 
            Enum.GetValues(typeof(PenFontWeights))
                .Cast<PenFontWeights>()
                .ToArray();

        private static PenFontWeights GetFontWeight(FontWeight fontWeight)
        {
            return fontWeightMap.FirstOrDefault(x => x.Value == fontWeight)
                .Key;
        }

        private static readonly PropertyChangedEventArgs fontWeightChangedEventArgs = new PropertyChangedEventArgs(nameof(FontWeight));
        private bool isBold;
        private PenFontWeights penFontWeight;

        [PlatformTargetGetMethod]
        public virtual FontWeight GetFontWeight()
        {
            if (fontWeightMap.TryGetValue(PenFontWeight, out FontWeight w))
            {
                return w;
            }
            return FontWeights.Normal;
        }
        [PlatformTargetSetMethod]
        public virtual void SetFontWeight(FontWeight value)
        {
            PenFontWeight = GetFontWeight(value);
            IsBold = PenFontWeight == PenFontWeights.Bold;
        }


        [DefaultValue(false)]
        public bool IsBold
        {
            get => isBold;
            set
            {
                Set(ref isBold, value);
                PenFontWeight = value ? PenFontWeights.Bold : PenFontWeights.Normal;
                RaiseFontWeightChanged();
            }
        }
        [DefaultValue(PenFontWeights.Normal)]
        public PenFontWeights PenFontWeight
        {
            get => penFontWeight;
            set
            {
                Set(ref penFontWeight, value);
                RaiseFontWeightChanged();
            }
        }
        protected void RaiseFontWeightChanged()
        {
            RaisePropertyChanged(fontWeightChangedEventArgs);
        }
    }
}
