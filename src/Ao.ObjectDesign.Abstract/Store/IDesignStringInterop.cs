using System;

namespace Ao.ObjectDesign.Store
{
    public interface IDesignStringInterop
    {
        string SerializeToString(object val, Type type);

        object DeserializeByString(string str, Type type);
    }
}
