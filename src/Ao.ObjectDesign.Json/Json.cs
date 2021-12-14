using Newtonsoft.Json;

namespace Ao.ObjectDesign.Wpf.Json
{
    internal static class Json
    {
        public static readonly JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
    }
}
