using System;

namespace Ao.ObjectDesign.WpfDesign
{
    public interface IDesignInitContext
    {
        IServiceProvider Provider { get; }
    }
}
