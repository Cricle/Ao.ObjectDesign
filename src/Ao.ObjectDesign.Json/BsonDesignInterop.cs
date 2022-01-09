using Ao.ObjectDesign.Abstract.Store;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.IO;
using System.Text;

namespace Ao.ObjectDesign.Wpf.Json
{
    public class BsonDesignInterop : IDesignByteInterop, IDesignStreamInterop
    {
        public static readonly BsonDesignInterop Default = new BsonDesignInterop(new JsonSerializer { TypeNameHandling = TypeNameHandling.Auto }, Encoding.UTF8);

        public BsonDesignInterop(JsonSerializer serializer, Encoding encoding)
        {
            Serializer = serializer;
            Encoding = encoding;
        }

        public JsonSerializer Serializer { get; }

        public Encoding Encoding { get; }

        public object DeserializeByStream(Stream stream, Type type)
        {
            var reader = new BsonDataReader(stream);
            return Serializer.Deserialize(reader, type);
        }
        public object DeserializeByByte(byte[] data, Type type)
        {
            using (var mem = new MemoryStream(data, true))
            {
                var reader = new BsonDataReader(mem);
                return Serializer.Deserialize(reader, type);
            }
        }

        public void SerializeToStream(object val, Type type, Stream stream)
        {
            var writer = new BsonDataWriter(stream);
            Serializer.Serialize(writer, val, type);
        }

        public byte[] SerializeToByte(object val, Type type)
        {
            using (var mem = new MemoryStream())
            {
                SerializeToStream(val, type, mem);
                return mem.ToArray();
            }
        }
    }
}
