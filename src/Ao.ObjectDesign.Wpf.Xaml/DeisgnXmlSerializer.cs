using Portable.Xaml;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Ao.ObjectDesign.Wpf.Xaml
{
    public static class DeisgnXamlSerializer
    {
        public static object Deserialize(string text)
        {
            return Deserialize(new StringReader(text));
        }
        public static object Deserialize(TextReader reader)
        {
            return XamlServices.Load(reader);
        }
        public static string Serialize(object obj)
        {
            StringWriter writer = new StringWriter();
            Serialize(obj, writer);
            return writer.ToString();
        }
        public static void Serialize(object obj,TextWriter writer)
        {
            XamlServices.Save(writer, obj);
        }

    }
}
