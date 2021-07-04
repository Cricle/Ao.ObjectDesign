using Ao.ObjectDesign.Wpf.Designing;
using Newtonsoft.Json;

namespace Ao.ObjectDesign.Wpf.Json
{
    public static class DesignJsonHelper
    {
        private static readonly IgnoreContractResolver ignoreContractResolver;
        private static readonly JsonSerializerSettings settings;

        static DesignJsonHelper()
        {
            ignoreContractResolver = new IgnoreContractResolver();
            settings = new JsonSerializerSettings();
            foreach (var item in DesigningHelpers.KnowDesigningTypes)
            {
                ignoreContractResolver.IgnoreTypes.Add(item);
            }
            settings.ContractResolver = ignoreContractResolver;
        }
        public static string SerializeObject<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, settings);
        }
        public static T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, settings);
        }
    }
}
