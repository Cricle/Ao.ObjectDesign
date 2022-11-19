using Ao.ObjectDesign.Store;
using Dahomey.Json;
using Dahomey.Json.Attributes;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ao.ObjectDesign.TextJson
{
    public class TextJsonDesignInterop : IDesignInterop
    {
        public static readonly TextJsonDesignInterop Default = CreateDefaultOptions();

        private static readonly JsonSerializerOptions settingsWithType = CreateWithType();

        private static TextJsonDesignInterop CreateDefaultOptions()
        {
            return new TextJsonDesignInterop(settingsWithType, Encoding.UTF8);
        }

        public static JsonSerializerOptions CreateWithType()
        {
            var options = new JsonSerializerOptions();
            options.SetupExtensions();
            options.SetReadOnlyPropertyHandling(ReadOnlyPropertyHandling.Read);
            var registry = options.GetDiscriminatorConventionRegistry();
            registry.DiscriminatorPolicy = DiscriminatorPolicy.Auto;
            registry.RegisterConvention(new TypeDiscriminatorConvention());
            options.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
            return options;
        }

        public TextJsonDesignInterop(JsonSerializerOptions options, Encoding encoding)
        {
            Options = options;
            Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        public JsonSerializerOptions Options { get; }

        public Encoding Encoding { get; }

        public object DeserializeByStream(Stream stream, Type type)
        {
            var sr = new StreamReader(stream, Encoding);
            return DeserializeByString(sr.ReadToEnd(), type);
        }

        public object DeserializeByString(string str, Type type)
        {
            return JsonSerializer.Deserialize(str, type, settingsWithType);
        }

        public void SerializeToStream(object val, Type type, Stream stream)
        {
            var writer = new Utf8JsonWriter(stream);
            JsonSerializer.Serialize(writer, val, type, settingsWithType);
        }

        public string SerializeToString(object val, Type type)
        {
            return JsonSerializer.Serialize(val, type, settingsWithType);
        }

        public byte[] SerializeToByte(object val, Type type)
        {
            var str = SerializeToString(val, type);
            return Encoding.GetBytes(str);
        }

        public object DeserializeByByte(byte[] data, Type type)
        {
            return JsonSerializer.Deserialize(data, type, settingsWithType);
        }
    }
}
