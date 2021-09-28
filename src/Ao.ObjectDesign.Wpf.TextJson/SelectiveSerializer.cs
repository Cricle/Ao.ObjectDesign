using Dahomey.Json.Serialization.Converters.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf.TextJson
{

    public class SelectiveSerializer : IObjectMappingConvention
    {
        private readonly IObjectMappingConvention defaultObjectMappingConvention = new DefaultObjectMappingConvention();
        private readonly IReadOnlyHashSet<Type> fields;

        public SelectiveSerializer(IReadOnlyHashSet<Type> fields)
        {
            this.fields = fields;
        }

        public void Apply<T>(JsonSerializerOptions options, ObjectMapping<T> objectMapping)
        {
            defaultObjectMappingConvention.Apply(options, objectMapping);
            foreach (IMemberMapping memberMapping in objectMapping.MemberMappings)
            {
                if (memberMapping is MemberMapping<T> member&& fields.Contains(member.MemberType))
                {
                    member.ShouldSerializeMethod = o => false;
                }
            }
        }
    }
}
