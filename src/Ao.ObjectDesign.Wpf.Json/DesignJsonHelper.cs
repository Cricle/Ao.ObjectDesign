using Ao.ObjectDesign.Wpf.Designing;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.IO;
using System.Text;

namespace Ao.ObjectDesign.Wpf.Json
{
    public static class DesignJsonHelper
    {
        internal static readonly JsonSerializerSettings settings= CreateSettings();

        static JsonSerializerSettings CreateSettings()
        {
            return CreateSerializeSettings();
        }
        public static JsonSerializerSettings CreateSerializeSettings()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = CreateIgnoresContractResolver()
            };
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
        public static string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, settings);
        }
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, settings);
        }
        public static T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, settings);
        }
        public static object Deserialize(string value, Type type)
        {
            return JsonConvert.DeserializeObject(value, type, settings);
        }
    }
}
