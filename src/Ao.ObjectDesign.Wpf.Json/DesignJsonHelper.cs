using Ao.ObjectDesign.Wpf.Designing;
using Newtonsoft.Json;
using System;

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
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = CreateIgnoresContractResolver()
            };
            return settings;
        }
        public static IgnoreContractResolver CreateIgnoresContractResolver()
        {
            IgnoreContractResolver resolver = new IgnoreContractResolver();
            foreach (Type item in DesigningHelpers.KnowDesigningTypes)
            {
                resolver.IgnoreTypes.Add(item);
            }
            return resolver;
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
        public static object DeserializeObject(string value, Type type)
        {
            return JsonConvert.DeserializeObject(value, type, settings);
        }
    }
}
