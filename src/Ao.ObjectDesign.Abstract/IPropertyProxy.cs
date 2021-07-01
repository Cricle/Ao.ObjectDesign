using System;

namespace Ao.ObjectDesign
{
    public interface IPropertyProxy : IPropertyDeclare
    {
        object DeclaringInstance { get; }
    }
}
