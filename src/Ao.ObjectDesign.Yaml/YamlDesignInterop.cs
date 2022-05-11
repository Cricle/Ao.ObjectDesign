using Ao.ObjectDesign.Store;
using System;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;

namespace Ao.ObjectDesign.Wpf.Yaml
{
    public class YamlDesignInterop : IDesignInterop
    {
        public static readonly YamlDesignInterop Default;

        static YamlDesignInterop()
        {
            var ser = new SerializerBuilder()
                 .EnsureRoundtrip()
                 .Build();
            var desc = new DeserializerBuilder()
                .Build();
            Default = new YamlDesignInterop(Encoding.UTF8, ser, desc);
        }

        public YamlDesignInterop(Encoding encoding, Serializer serializer, Deserializer deserializer)
        {
            Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
            Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            Deserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
        }

        public Encoding Encoding { get; }

        public Serializer Serializer { get; }

        public Deserializer Deserializer { get; }

        public object DeserializeByStream(Stream stream, Type type)
        {
            var sr = new StreamReader(stream, Encoding);
            return Deserializer.Deserialize(sr, type);
        }

        public object DeserializeByString(string str, Type type)
        {
            return Deserializer.Deserialize(str, type);
        }

        public object DeserializeByByte(byte[] data, Type type)
        {
            var str = Encoding.GetString(data, 0, data.Length);
            return DeserializeByString(str, type);
        }

        public void SerializeToStream(object val, Type type, Stream stream)
        {
            var sr = new StreamWriter(stream, Encoding);
            Serializer.Serialize(sr, val);
        }

        public string SerializeToString(object val, Type type)
        {
            return Serializer.Serialize(val);
        }

        public byte[] SerializeToByte(object val, Type type)
        {
            var str = SerializeToString(val, type);
            return Encoding.GetBytes(str);
        }
    }
}
