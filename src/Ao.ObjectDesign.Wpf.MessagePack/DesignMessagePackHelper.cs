using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Wpf.Designing;
using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;

namespace Ao.ObjectDesign.Wpf.MessagePack
{
    public static class DesignMessagePackHelper
    {
        public static readonly IFormatterResolver DesignResolver = CreateResolver();
        private static readonly MessagePackSerializerOptions options;

        static DesignMessagePackHelper()
        {
            options = MessagePackSerializer.Typeless.DefaultOptions
                .WithCompression(MessagePackCompression.Lz4Block)
                .WithResolver(DesignResolver);
        }

        public static IFormatterResolver CreateResolver()
        {
            var ignores = new IgnoreFormatterResolver(TypelessObjectResolver.Instance, new HashSet<Type>(DesigningHelpers.KnowDesigningTypes));
            ignores.ForceTypeless.Add(typeof(NotifyableObject));
            return ignores;
        }
        public static byte[] Serialize(Type type, object val)
        {
            return MessagePackSerializer.Serialize(type,val, options);
        }
        public static object Deserialize(Type type, ReadOnlyMemory<byte> bytes)
        {
            return MessagePackSerializer.Deserialize(type, bytes, options);
        }
    }
}
