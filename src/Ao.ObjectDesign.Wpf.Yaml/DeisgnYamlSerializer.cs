using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.IO;
using YamlDotNet.Serialization;

namespace Ao.ObjectDesign.Wpf.Yaml
{
    public static class DeisgnYamlSerializer
    {
        public static IgnoresTypeInspector CreateIgnoresTypeInspector(ITypeInspector typeInspector)
        {
            return new IgnoresTypeInspector(typeInspector, DesigningHelpers.KnowDesigningTypes);
        }
        public static string Serialize(object value)
        {
            return Serialize(value, DesigningHelpers.KnowDesigningTypes);
        }
        public static string Serialize(object value, IReadOnlyHashSet<Type> ignoreTypes)
        {
            return Serialize(value, value.GetType(), ignoreTypes);
        }
        public static string Serialize(object value, Type type)
        {
            return Serialize(value, type, DesigningHelpers.KnowDesigningTypes);
        }
        public static string Serialize(object value, Type type, IReadOnlyHashSet<Type> ignoreTypes)
        {
            StringWriter writer = new StringWriter();
            Serialize(writer, value, type, ignoreTypes);
            return writer.ToString();
        }
        public static void Serialize(TextWriter textWriter, object value, Type type)
        {
            Serialize(textWriter, value, type, DesigningHelpers.KnowDesigningTypes);
        }
        public static void Serialize(TextWriter textWriter, object value, Type type, IReadOnlyHashSet<Type> ignoreTypes)
        {
            ISerializer builder = new SerializerBuilder()
                .WithTypeInspector(inspector => new IgnoresTypeInspector(inspector, ignoreTypes))
                .Build();
            builder.Serialize(textWriter, value, type);
        }
        public static object Deserializer(string str, Type type)
        {
            return Deserializer(str, type, DesigningHelpers.KnowDesigningTypes);
        }
        public static object Deserializer(string str, Type type, IReadOnlyHashSet<Type> ignoreTypes)
        {
            return Deserializer(new StringReader(str), type, ignoreTypes);
        }
        public static object Deserializer(TextReader textReader, Type type)
        {
            return Deserializer(textReader, type, DesigningHelpers.KnowDesigningTypes);
        }
        public static object Deserializer(TextReader textReader, Type type, IReadOnlyHashSet<Type> ignoreTypes)
        {
            IDeserializer builder = new DeserializerBuilder()
                .WithTypeInspector(inspector => new IgnoresTypeInspector(inspector, ignoreTypes))
                .Build();
            return builder.Deserialize(textReader, type);
        }
    }
}
