using Dahomey.Json.Serialization.Converters.Mappings;
using System;

namespace Ao.ObjectDesign.Wpf.TextJson
{
    public class AllObjectMappingConventionProvider : IObjectMappingConventionProvider
    {
        public IReadOnlyHashSet<Type> IgnoreTypes { get; }
        private readonly SelectiveSerializer selectiveSerializer;

        public AllObjectMappingConventionProvider(IReadOnlyHashSet<Type> ignoreTypes)
        {
            IgnoreTypes = ignoreTypes;
            selectiveSerializer = new SelectiveSerializer(ignoreTypes);
        }

        public IObjectMappingConvention GetConvention(Type type)
        {
            return selectiveSerializer;
        }
    }
}
