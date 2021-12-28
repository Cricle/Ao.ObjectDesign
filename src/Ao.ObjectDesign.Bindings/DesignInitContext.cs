using System;

namespace Ao.ObjectDesign.Bindings
{
    public class DesignInitContext : IDesignInitContext
    {
        public DesignInitContext(IServiceProvider provider)
        {
            Provider = provider;
        }

        public virtual IServiceProvider Provider { get; }
    }
}
