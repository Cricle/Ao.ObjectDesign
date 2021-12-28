using System;

namespace Ao.ObjectDesign.Bindings
{
    public interface IDesignInitContext
    {
        IServiceProvider Provider { get; }
    }
}
