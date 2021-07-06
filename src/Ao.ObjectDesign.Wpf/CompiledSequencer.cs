using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace Ao.ObjectDesign.Wpf
{
    [DebuggerDisplay("Undos = {Undos.Count}, Redos = {Redos.Count}")]
    public class CompiledSequencer : Sequencer
    {
        class PropertyIdentity : IEquatable<PropertyIdentity>
        {
            public PropertyIdentity(Type type, string propertyName)
            {
                Type = type;
                PropertyName = propertyName;
            }

            public Type Type { get; }

            public string PropertyName { get; }

            public bool Equals(PropertyIdentity other)
            {
                if (other is null)
                {
                    return false;
                }
                return other.Type == Type &&
                    other.PropertyName == PropertyName;
            }
            public override string ToString()
            {
                return $"{{{Type}, {PropertyName}}}";
            }
            public override bool Equals(object obj)
            {
                return Equals(obj as PropertyIdentity);
            }
            public override int GetHashCode()
            {
                return Type.GetHashCode() ^ PropertyName.GetHashCode();
            }
        }
        delegate void PropertySetter(object instance, object value);
        private static Dictionary<PropertyIdentity, PropertySetter> propertySetters = new Dictionary<PropertyIdentity, PropertySetter>();
        protected override void OnReset(ModifyDetail detail)
        {
            var instanceType = detail.Instance.GetType();
            var identity = new PropertyIdentity(instanceType, detail.PropertyName);
            if (!propertySetters.TryGetValue(identity, out var setter))
            {
                var prop = instanceType.GetProperty(detail.PropertyName);
                setter = BuildSetter(prop);
                propertySetters.Add(identity, setter);
            }
            setter(detail.Instance, detail.From);
        }
        private PropertySetter BuildSetter(PropertyInfo propertyInfo)
        {
            var par1 = Expression.Parameter(typeof(object));
            var par1Conv = Expression.Convert(par1, propertyInfo.DeclaringType);

            var par2 = Expression.Parameter(typeof(object));
            var par2Conv = Expression.Convert(par2, propertyInfo.SetMethod.GetParameters()[0].ParameterType);

            var body = Expression.Call(par1Conv, propertyInfo.SetMethod, par2Conv);

            var method = Expression.Lambda<PropertySetter>(body, par1, par2).Compile();
            return method;
        }
    }
}
