using System.Windows.Media;

namespace ObjectDesign.Brock.Converters
{
    public class StretchBoolConverter : EnumBoolConverter<Stretch>
    {
        public static readonly StretchBoolConverter Instance = new StretchBoolConverter();
    }
}
