using System;

namespace Ao.ObjectDesign.Sources.Types
{
    public class SourceColumn
    {
        public SourceColumn(string name, string originName, TypeCode typeCode)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            OriginName = originName ?? throw new ArgumentNullException(nameof(originName));
            TypeCode = typeCode;
        }

        public string Name { get; }

        public string OriginName { get; }

        public TypeCode TypeCode { get; }

        public override bool Equals(object obj)
        {
            if (obj is SourceColumn column)
            {
                return column.Name == Name &&
                    column.OriginName == OriginName &&
                    column.TypeCode == TypeCode;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ 
                OriginName.GetHashCode()^
                TypeCode.GetHashCode();
        }
        public override string ToString()
        {
            return $"{{Name: {Name}, OriginName: {OriginName}, TypeCode: {TypeCode}}}";
        }
    }
}
