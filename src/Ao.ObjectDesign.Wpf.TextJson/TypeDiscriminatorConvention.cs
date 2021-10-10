using Dahomey.Json.Serialization.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ao.ObjectDesign.Wpf.TextJson
{
    public class TypeDiscriminatorConvention : IDiscriminatorConvention
    {
        private readonly ReadOnlyMemory<byte> _memberName;
        private readonly Dictionary<Type, string> typeNames = new Dictionary<Type, string>();
        private readonly Dictionary<string, Type> nameTypes = new Dictionary<string, Type>();

        public TypeDiscriminatorConvention()
        {
            _memberName = Encoding.UTF8.GetBytes("$type");
        }

        public ReadOnlySpan<byte> MemberName => _memberName.Span;

        public Type ReadDiscriminator(ref Utf8JsonReader reader)
        {
            var type = reader.GetString();
            if (!nameTypes.TryGetValue(type, out var t))
            {
                var ts = type.Split(',');
                var ass = Assembly.Load(ts[1]);
                t = ass.GetType(ts[0]);
                nameTypes.Add(type, t);
            }
            return t;
        }

        public bool TryRegisterType(Type type)
        {
            return true;
        }

        public void WriteDiscriminator(Utf8JsonWriter writer, Type actualType)
        {
            if (!typeNames.TryGetValue(actualType, out var str))
            {
                var assemboy = actualType.Assembly.GetName().Name;
                var typeName = actualType.FullName;
                str = typeName + ", " + assemboy;
                typeNames.Add(actualType, str);
            }
            writer.WriteStringValue(str);
        }
    }
}
