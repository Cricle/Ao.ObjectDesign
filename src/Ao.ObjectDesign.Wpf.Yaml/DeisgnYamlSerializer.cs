using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.TypeInspectors;

namespace Ao.ObjectDesign.Wpf.Yaml
{
    public static class DeisgnYamlSerializer
    {
        public static string Serialize(object value)
        {
            return Serialize(value, value.GetType());
        }
        public static string Serialize(object value, Type type)
        {
            StringWriter writer = new StringWriter();
            Serialize(writer, value, type);
            return writer.ToString();
        }
        public static void Serialize(TextWriter textWriter, object value, Type type)
        {
            var builder = new SerializerBuilder()
             .EnsureRoundtrip()
             .Build();
            builder.Serialize(textWriter, value, type);
        }
        public static object Deserializer(string str, Type type)
        {
            return Deserializer(new StringReader(str), type);
        }
        public static object Deserializer(TextReader textReader, Type type)
        {
            var builder = new DeserializerBuilder()
                .Build();
            return builder.Deserialize(textReader, type);
        }
    }
}
