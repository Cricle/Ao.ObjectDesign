using System;

namespace Ao.ObjectDesign
{
    public interface IObjectDesigner
    {
        IObjectProxy CreateProxy(object instance,Type type);
    }
}
