using Ao.ObjectDesign.Designing;

namespace ObjectDesign.Brock.Converters
{
    public class PenBrushTypeBoolConverter : EnumBoolConverter<PenBrushTypes>
    {
        public static readonly PenBrushTypeBoolConverter Instance = new PenBrushTypeBoolConverter();
    }
}
