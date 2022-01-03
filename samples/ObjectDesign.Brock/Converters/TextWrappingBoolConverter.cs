using System.Windows;

namespace ObjectDesign.Brock.Converters
{
    public class TextWrappingBoolConverter : EnumBoolConverter<TextWrapping>
    {
        public static readonly TextWrappingBoolConverter Instance = new TextWrappingBoolConverter();
    }
}
