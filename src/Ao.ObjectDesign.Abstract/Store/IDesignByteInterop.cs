using System;

namespace Ao.ObjectDesign.Store
{
    public interface IDesignByteInterop
    {
        byte[] SerializeToByte(object val, Type type);

        object DeserializeByByte(byte[] data, Type type);
    }
}
