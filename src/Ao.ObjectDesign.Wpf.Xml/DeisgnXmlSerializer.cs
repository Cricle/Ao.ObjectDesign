using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Ao.ObjectDesign.Wpf.Xml
{
    public class DeisgnXmlSerializer
    {
        public static XmlAttributeOverrides CreateDesignIgnores()
        {
            var over = new XmlAttributeOverrides();
            foreach (var item in DesigningHelpers.KnowDesigningPropertyIdentities)
            {
                over.Add(item.Type, item.PropertyName, new XmlAttributes { XmlIgnore = true });
            }
            return over;
        }
        public static T Deserialize<T>(string text)
        {
            return (T)Deserialize(typeof(T), text);
        }
        public static object Deserialize(Type type, string text)
        {
            return Deserialize(type, new StringReader(text));
        }
        public static object Deserialize(Type type,TextReader reader)
        {
            var over = CreateDesignIgnores();
            var xmlSer = new XmlSerializer(type, over);
            return xmlSer.Deserialize(reader);
        }
        public static string Serialize(object obj)
        {
            return Serialize(obj, obj.GetType());
        }
        public static string Serialize(object obj, Type type)
        {
            var writer = new StringWriter();
            Serialize(obj, type, writer);
            return writer.ToString();
        }
        public static void Serialize(object obj,Type type, TextWriter writer)
        {
            var over = CreateDesignIgnores();
            var xmlSer = new XmlSerializer(type, over);
            xmlSer.Serialize(writer, obj);
        }

    }
    public class XmlUri : IXmlSerializable
    {
        private Uri _Value;

        public XmlUri() { }
        public XmlUri(Uri source) { _Value = source; }

        public static implicit operator Uri(XmlUri o)
        {
            return o == null ? null : o._Value;
        }

        public static implicit operator XmlUri(Uri o)
        {
            return o == null ? null : new XmlUri(o);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            _Value = new Uri(reader.ReadElementContentAsString());
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteValue(_Value.ToString());
        }
    }
}
