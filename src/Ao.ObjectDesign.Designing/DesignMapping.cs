using Ao.ObjectDesign.Designing.Annotations;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Ao.ObjectDesign.Designing
{
    public class DesignMapping : IEquatable<DesignMapping>
    {
        public DesignMapping(Type clrType, Type uiType)
        {
            ClrType = clrType ?? throw new ArgumentNullException(nameof(clrType));
            UIType = uiType ?? throw new ArgumentNullException(nameof(uiType));
        }

        public Type ClrType { get; }

        public Type UIType { get; }

        public bool Equals(DesignMapping other)
        {
            if (other is null)
            {
                return false;
            }
            return other.UIType == UIType &&
                other.ClrType == ClrType;
        }

        public override int GetHashCode()
        {
            Debug.Assert(ClrType != null);
            Debug.Assert(UIType != null);
#if NET5_0
            return HashCode.Combine(ClrType, UIType);
#else
            unchecked
            {
                var h = 31 * 17 + ClrType.GetHashCode();
                h = h * 31 + UIType.GetHashCode();
                return h;
            }
#endif
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as DesignMapping);
        }
        public override string ToString()
        {
            return $"{{Clr:{ClrType}, UI:{UIType}}}";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DesignMapping FromMapping(Type type)
        {
            MappingForAttribute forAttribute = type.GetCustomAttribute<MappingForAttribute>();
            if (forAttribute is null)
            {
                throw new ArgumentException($"Type {type.FullName} not tag MappingForAttribute");
            }
            return new DesignMapping(type, forAttribute.Type);
        }
    }
}
