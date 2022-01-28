using System.Reflection;

namespace Ao.ObjectDesign
{
    public interface IPropertyVisitor
    {
        bool CanGet { get; }

        bool CanSet { get; }

        PropertyInfo PropertyInfo { get; }

        object Value { get; set; }
    }
}
