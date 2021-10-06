using MessagePack;
using MessagePack.Formatters;

namespace Ao.ObjectDesign.Wpf.MessagePack
{
    public class NullMessagePackFormatter<T> : IMessagePackFormatter<T>
    {
        public T Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            return default;
        }

        public void Serialize(ref MessagePackWriter writer, T value, MessagePackSerializerOptions options)
        {
        }
    }
}
