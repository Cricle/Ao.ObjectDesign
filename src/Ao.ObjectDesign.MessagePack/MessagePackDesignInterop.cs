using Ao.ObjectDesign.Abstract.Store;
using MessagePack;
using MessagePack.Resolvers;
using System;
using System.IO;
using System.Text;

namespace Ao.ObjectDesign.Wpf.MessagePack
{
    public class MessagePackDesignInterop : IDesignByteInterop, IDesignStreamInterop
    {
        public static readonly MessagePackDesignInterop Default;

        private static readonly TypelessFormatterResolver designResolver;
        private static readonly MessagePackSerializerOptions options;

        static MessagePackDesignInterop()
        {
            designResolver = new TypelessFormatterResolver(TypelessObjectResolver.Instance);
            var type = Type.GetType("Ao.ObjectDesign.Designing.NotifyableObject");
            if (type != null)
            {
                designResolver.ForceTypeless.Add(type);
            }
            options = MessagePackSerializer.Typeless.DefaultOptions
                .WithCompression(MessagePackCompression.Lz4Block)
                .WithResolver(designResolver);
            Default = new MessagePackDesignInterop(options, Encoding.ASCII);
        }

        public MessagePackDesignInterop(MessagePackSerializerOptions options, Encoding encoding)
        {
            Options = options;
            Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        public MessagePackSerializerOptions Options { get; }

        public Encoding Encoding { get; }

        public object DeserializeByStream(Stream stream, Type type)
        {
            return MessagePackSerializer.Deserialize(type, stream, Options);
        }
        public object DeserializeByByte(byte[] data, Type type)
        {
            return MessagePackSerializer.Deserialize(type, data, Options);
        }


        public byte[] SerializeToByte(object val, Type type)
        {
            return MessagePackSerializer.Serialize(type, val, Options);
        }

        public void SerializeToStream(object val, Type type, Stream stream)
        {
            MessagePackSerializer.Serialize(type, stream, val, Options);
        }

    }
}
