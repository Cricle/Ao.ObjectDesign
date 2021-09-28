using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Dahomey.Json;
using Ao.ObjectDesign.Wpf.Designing;

namespace Ao.ObjectDesign.Wpf.TextJson
{
    public static class DesignTextJsonHelper
    {
        internal static readonly JsonSerializerOptions settings = CreateOptions();

        public static JsonSerializerOptions CreateOptions()
        {
            var options = new JsonSerializerOptions();
            options.SetupExtensions();
            var reg = options.GetObjectMappingConventionRegistry();
            reg.RegisterProvider(new AllObjectMappingConventionProvider(DesigningHelpers.KnowDesigningTypes));
            return options;
        }

        public static string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj, settings);
        }
        public static string Serialize(object obj, Type type)
        {
            return JsonSerializer.Serialize(obj, type, settings);
        }
        public static string Serialize(object obj)
        {
            return JsonSerializer.Serialize(obj, obj.GetType(), settings);
        }
        public static T Deserialize<T>(string value)
        {
            return JsonSerializer.Deserialize<T>(value, settings);
        }
        public static object Deserialize(string value, Type type)
        {
            return JsonSerializer.Deserialize(value, type, settings);
        }
    }
}
