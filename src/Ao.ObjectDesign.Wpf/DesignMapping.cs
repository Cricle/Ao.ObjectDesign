using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf
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
#if NET5_0
            return HashCode.Combine(ClrType,UIType);
#else
            return ClrType.GetHashCode() + UIType.GetHashCode();
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
    }
}
