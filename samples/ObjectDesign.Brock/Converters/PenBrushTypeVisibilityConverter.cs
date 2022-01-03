using Ao.ObjectDesign.Wpf.Designing;

namespace ObjectDesign.Brock.Converters
{
    public class PenBrushTypeVisibilityConverter : EnumVisibilityConverter<PenBrushTypes>
    {
        public static readonly PenBrushTypeVisibilityConverter Instance = new PenBrushTypeVisibilityConverter();
    }
}
