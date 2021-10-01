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
        internal static readonly JsonSerializerOptions settingsWithType = CreateWithType();
        public static JsonSerializerOptions CreateWithType()
        {
            var opt = CreateOptions();
            var registry = opt.GetDiscriminatorConventionRegistry();
            registry.DiscriminatorPolicy = Dahomey.Json.Serialization.Conventions.DiscriminatorPolicy.Always;
            registry.ClearConventions();
            registry.RegisterConvention(new TypeDiscriminatorConvention());
            return opt;
        }
        public static JsonSerializerOptions CreateOptions()
        {
            var options = new JsonSerializerOptions();
            options.IgnoreReadOnlyProperties = false;
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
        public static string SerializeWithType<T>(T obj)
        {
            return JsonSerializer.Serialize(obj, settingsWithType);
        }
        public static string SerializeWithType(object obj, Type type)
        {
            return JsonSerializer.Serialize(obj, type, settingsWithType);
        }
        public static string SerializeWithType(object obj)
        {
            return JsonSerializer.Serialize(obj, obj.GetType(), settingsWithType);
        }
        public static T Deserialize<T>(string value)
        {
            return JsonSerializer.Deserialize<T>(value, settings);
        }
        public static object Deserialize(string value, Type type)
        {
            return JsonSerializer.Deserialize(value, type, settings);
        }
        public static T DeserializeWithType<T>(string value)
        {
            return JsonSerializer.Deserialize<T>(value, settingsWithType);
        }
        public static object DeserializeWithType(string value, Type type)
        {
            return JsonSerializer.Deserialize(value, type, settingsWithType);
        }
    }
}
