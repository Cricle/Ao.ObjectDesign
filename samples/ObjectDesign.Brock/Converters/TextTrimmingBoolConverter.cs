using System.Windows;

namespace ObjectDesign.Brock.Converters
{
    public class TextTrimmingBoolConverter : EnumBoolConverter<TextTrimming>
    {
        public static readonly TextTrimmingBoolConverter Instance = new TextTrimmingBoolConverter();
    }
}
