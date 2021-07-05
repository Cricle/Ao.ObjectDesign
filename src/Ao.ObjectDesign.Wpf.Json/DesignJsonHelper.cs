using Ao.ObjectDesign.Wpf.Designing;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Json
{
    public static class DesignJsonHelper
    {
        private static readonly JsonSerializerSettings settings;

        static DesignJsonHelper()
        {
            settings = CreateSerializeSettings();
        }
        public static JsonSerializerSettings CreateSerializeSettings()
        {
            var settings = new JsonSerializerSettings();
            SetSerializeSettings(settings);
            return settings;
        }
        public static void SetSerializeSettings(JsonSerializerSettings settings)
        {
            var resolver= new IgnoreContractResolver();
            foreach (var item in DesigningHelpers.KnowDesigningTypes)
            {
                resolver.IgnoreTypes.Add(item);
            }
            resolver.IgnoreTypes.Add(typeof(IEnumerable<GradientStop>));
            settings.ContractResolver = resolver;
        }
        public static string SerializeObject<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, settings);
        }
        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj, settings);
        }
        public static T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, settings);
        }
        public static object DeserializeObject(string value,Type type)
        {
            return JsonConvert.DeserializeObject(value, type, settings);
        }
    }
}
