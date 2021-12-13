using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf.Store
{
    public interface IDesignInterop:IDesignByteInterop,IDesignStreamInterop,IDesignStringInterop
    {

    }
    public interface IDesignByteInterop
    {
        byte[] SerializeToByte(object val,Type type);

        object DeserializeByByte(byte[] data, Type type);
    }
    public interface IDesignStringInterop
    {
        string SerializeToString(object val, Type type);

        object DeserializeByString(string str, Type type);
    }
    public interface IDesignStreamInterop
    {
        void SerializeToStream(object val, Type type, Stream stream);

        object DeserializeByStream(Stream stream,Type type);
    }
}
