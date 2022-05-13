using System;

namespace Ao.ObjectDesign.Sources
{
    public class ProviderFactorySelectContext
    {
        public ProviderFactorySelectContext(Type type)
        {
            Type = type;
        }

        public Type Type { get; }
    }
}
