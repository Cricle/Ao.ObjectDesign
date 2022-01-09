using Ao.ObjectDesign.Abstract.Store;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Ao.ObjectDesign.Wpf.Json
{
    public class JsonDesignInterop : IDesignInterop
    {
        public static readonly JsonDesignInterop Default = new JsonDesignInterop(Json.settings, Encoding.UTF8);

        public JsonSerializerSettings Settings { get; }

        public Encoding Encoding { get; }

        public JsonDesignInterop(JsonSerializerSettings settings, Encoding encoding)
        {
            Settings = settings;
            Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        public object DeserializeByStream(Stream stream, Type type)
        {
            var sr = new StreamReader(stream);
            return DeserializeByString(sr.ReadToEnd(), type);
        }

        public object DeserializeByString(string str, Type type)
        {
            return JsonConvert.DeserializeObject(str, type, Json.settings);
        }

        public void SerializeToStream(object val, Type type, Stream stream)
        {
            var sw = new StreamWriter(stream);
            var str = SerializeToString(val, type);
            sw.Write(str);
        }

        public string SerializeToString(object val, Type type)
        {
            return JsonConvert.SerializeObject(val, type, Json.settings);
        }

        public byte[] SerializeToByte(object val, Type type)
        {
            var str = SerializeToString(val, type);
            return Encoding.GetBytes(str);
        }

        public object DeserializeByByte(byte[] data, Type type)
        {
            using (var mem = new MemoryStream(data))
            {
                return DeserializeByStream(mem, type);
            }
        }
    }
}
