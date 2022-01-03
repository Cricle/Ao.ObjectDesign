using ObjectDesign.Brock.Controls;

namespace ObjectDesign.Brock.Converters
{
    public class ResourceTypesBoolConverter : EnumBoolConverter<ResourceTypes>
    {
        public static readonly ResourceTypesBoolConverter Instance = new ResourceTypesBoolConverter();
    }
}
