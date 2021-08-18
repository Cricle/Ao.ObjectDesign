using System;

namespace Ao.ObjectDesign
{
    public interface IInstanceFactory
    {
        Type TargetType { get; }

        object Create();
    }
}
