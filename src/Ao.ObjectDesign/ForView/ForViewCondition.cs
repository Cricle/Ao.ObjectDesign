using System;

namespace Ao.ObjectDesign.ForView
{
    public class ForViewBuildContext : IForViewBuildContext
    {
        public ForViewBuildContext(IPropertyProxy propertyProxy)
        {
            PropertyProxy = propertyProxy ?? throw new ArgumentNullException(nameof(propertyProxy));
        }

        public IPropertyProxy PropertyProxy { get; }
    }
}
