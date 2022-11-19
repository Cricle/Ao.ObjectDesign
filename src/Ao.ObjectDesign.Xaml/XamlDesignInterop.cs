using Ao.ObjectDesign.Store;
using Portable.Xaml;
using System;
using System.IO;
using System.Text;

namespace Ao.ObjectDesign.Xaml
{
    public class XamlDesignInterop : IDesignInterop
    {
        public static readonly XamlDesignInterop Default = new XamlDesignInterop(Encoding.UTF8);

        public XamlDesignInterop(Encoding encoding)
        {
            Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        public Encoding Encoding { get; }

        public object DeserializeByStream(Stream stream, Type type)
        {
            return XamlServices.Load(stream);
        }

        public object DeserializeByString(string str, Type type)
        {
            return XamlServices.Load(new XamlXmlReader(new StringReader(str)));
        }

        public object DeserializeByByte(byte[] data, Type type)
        {
            using (var ms = new MemoryStream(data))
            {
                return XamlServices.Load(new XamlXmlReader(ms));
            }
        }

        public void SerializeToStream(object val, Type type, Stream stream)
        {
            XamlServices.Save(stream, val);
        }

        public string SerializeToString(object val, Type type)
        {
            return XamlServices.Save(val);
        }

        public byte[] SerializeToByte(object val, Type type)
        {
            var str = SerializeToString(val, type);
            return Encoding.GetBytes(str);
        }
    }
}
