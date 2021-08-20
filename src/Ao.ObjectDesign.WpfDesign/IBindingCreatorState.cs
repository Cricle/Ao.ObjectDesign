using System;
using System.Collections;

namespace Ao.ObjectDesign.WpfDesign
{
    public interface IBindingCreatorState : IServiceProvider
    {
        IDictionary Features { get; }

        DesignRuntimeTypes RuntimeType { get; }
    }
}
