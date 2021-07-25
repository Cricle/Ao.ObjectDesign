using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(FontFamily))]
    public class FontFamilyDesigner : NotifyableObject
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
                    using (InstalledFontCollection coll = new InstalledFontCollection())
                    {
                        installedFontFamilies = coll.Families
                            .Select(x => new FontFamily(x.Name))
                            .ToArray();
                    }
                }
                return installedFontFamilies;
            }
        }
        private string fontName;

        [PlatformTargetProperty]
        public virtual FontFamily FontFamily
        {
            get
            {
                if (string.IsNullOrEmpty(fontName))
                {
                    return new FontFamily();
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
