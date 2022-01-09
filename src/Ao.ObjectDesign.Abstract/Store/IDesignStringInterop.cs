using System;

namespace Ao.ObjectDesign.Abstract.Store
{
    public interface IDesignStringInterop
    {
        string SerializeToString(object val, Type type);

        object DeserializeByString(string str, Type type);
    }
}
