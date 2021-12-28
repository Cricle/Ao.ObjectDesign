using System;
using System.Collections;

namespace Ao.ObjectDesign.Bindings
{
    public interface IBindingCreatorState : IServiceProvider
    {
        IDictionary Features { get; }

        DesignRuntimeTypes RuntimeType { get; }
    }
}
