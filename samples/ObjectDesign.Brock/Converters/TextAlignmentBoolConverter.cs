using System.Windows;

namespace ObjectDesign.Brock.Converters
{
    public class TextAlignmentBoolConverter : EnumBoolConverter<TextAlignment>
    {
        public static readonly TextAlignmentBoolConverter Instance = new TextAlignmentBoolConverter();
    }
}
