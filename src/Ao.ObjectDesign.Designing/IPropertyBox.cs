using System.Reflection;

namespace Ao.ObjectDesign.Designing
{
    public interface IPropertyBox
    {
        PropertyInfo Property { get; }

        PropertyGetter Getter { get; }
        
        PropertySetter Setter { get; }

        string Name { get; }

        bool IsBuilt { get; }

        bool IsVirtualPropery { get; }
    }
}
