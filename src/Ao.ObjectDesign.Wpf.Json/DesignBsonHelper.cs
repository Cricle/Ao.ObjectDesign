using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.IO;
using System.Text;

namespace Ao.ObjectDesign.Wpf.Json
{
    public static class DesignBsonHelper
    {
        public static Stream Serialize<T>(T value)
        {
            return Serialize(value, value.GetType());
        }
        public static Stream Serialize(object value, Type type)
        {
            var mem = new MemoryStream();
            Serialize(mem, value, type);
            mem.Flush();
            mem.Seek(0, SeekOrigin.Begin);
            return mem;
        }
        public static void Serialize(Stream stream, object value, Type type)
        {
            var writer = new BsonDataWriter(stream);
            Serialize(writer, value, type);
            writer.Flush();
        }
        public static void Serialize(BsonDataWriter writer, object value, Type type)
        {
            Serialize(writer, value, type, null);
        }
        public static void Serialize(BsonDataWriter writer, object value, Type type, Action<JsonSerializer> action)
        {
            JsonSerializer serializer = new JsonSerializer();
            action?.Invoke(serializer);
            serializer.Serialize(writer, value, type);
        }
        public static T Deserialize<T>(string str)
        {
            var val = Deserialize(str, typeof(T));
            if (val is T t)
            {
                return t;
            }
            return default;
        }
        public static object Deserialize(string str, Type type)
        {
            return Deserialize(str, Encoding.UTF8, type);
        }
        public static object Deserialize(string str, Encoding encoding, Type type)
        {
            using (var mem = new MemoryStream())
            {
                var buffer = Encoding.UTF8.GetBytes(str);
                mem.Write(buffer, 0, buffer.Length);
                mem.Seek(0, SeekOrigin.Begin);
                return Deserialize(mem, type);
            }
        }
        public static T Deserialize<T>(Stream stream)
        {
            var val = Deserialize(stream, typeof(T));
            if (val is T t)
            {
                return t;
            }
            return default;
        }
        public static object Deserialize(Stream stream, Type type)
        {
            return Deserialize(new BsonDataReader(stream), type);
        }
        public static T Deserialize<T>(BsonDataReader reader)
        {
            var val = Deserialize(reader, typeof(T));
            if (val is T t)
            {
                return t;
            }
            return default;
        }
        public static object Deserialize(BsonDataReader reader, Type type)
        {
            return Deserialize(reader, type, null);
        }
        public static object Deserialize(BsonDataReader reader, Type type, Action<JsonSerializer> action)
        {
            JsonSerializer serializer = new JsonSerializer();
            action?.Invoke(serializer);
            return serializer.Deserialize(reader, type);
        }
    }
}
