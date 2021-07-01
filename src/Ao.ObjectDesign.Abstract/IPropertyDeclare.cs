using System.Reflection;

namespace Ao.ObjectDesign
{
    public interface IPropertyDeclare : IObjectDeclaring
    {
        PropertyInfo PropertyInfo { get; }
    }
}
