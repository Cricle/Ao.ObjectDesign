using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ao.ObjectDesign.Wpf.Json
{
    public class IgnoreContractResolver : DefaultContractResolver
    {
        public IgnoreContractResolver()
            :this(new HashSet<Type>())
        {

        }
        public IgnoreContractResolver(HashSet<Type> ignoreTypes)
        {
            IgnoreTypes = ignoreTypes ?? throw new ArgumentNullException(nameof(ignoreTypes));
        }

        public HashSet<Type> IgnoreTypes { get; }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);
            if (member is PropertyInfo info && IgnoreTypes.Contains(info.PropertyType))
            {
                prop.Ignored = true;
            }
            return prop;
        }
    }
}
