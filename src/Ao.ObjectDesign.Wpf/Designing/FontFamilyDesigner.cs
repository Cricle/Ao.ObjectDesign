using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Text;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(FontFamily))]
    public class FontFamilyDesigner : NotifyableObject
    {
        private static FontFamily[] installedFontFamilies;
        public static IReadOnlyDictionary<string, FontFamily> InstalledFontFamilyMap =>
            FrozenDictionary<string, FontFamily>.Create(InstalledFontFamilies,
                x => x.Source, x => x, null);
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

        [PlatformTargetGetMethod]
        public virtual FontFamily GetFontFamily()
        {
            if (string.IsNullOrEmpty(fontName))
            {
                return new FontFamily();
            }
            return new FontFamily(fontName);
        }
        [PlatformTargetSetMethod]
        public virtual void SetFontFamily(FontFamily value)
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

        [DefaultValue(null)]
        public string FontName
        {
            get => fontName;
            set
            {
                Set(ref fontName, value);
                RaiseFontFamilyChanged();
            }
        }
        private static readonly PropertyChangedEventArgs FontFamilyChangedEventArgs = new PropertyChangedEventArgs(nameof(FontFamily));
        protected void RaiseFontFamilyChanged()
        {
            RaisePropertyChanged(FontFamilyChangedEventArgs);
        }

    }
}
