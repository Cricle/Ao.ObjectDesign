using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace Ao.ObjectDesign
{
    public class ObjectDeclaring : IObjectDeclaring,IEquatable<ObjectDeclaring>
    {
        protected ObjectDeclaring()
        {

        }
        public ObjectDeclaring(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }


        public virtual Type Type { get; }

        public override int GetHashCode()
        {
            if (Type is null)
            {
                return 0;
            }
            return Type.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is ObjectDeclaring)
            {
                return Equals((ObjectDeclaring)obj);
            }
            return false;
        }
        public bool Equals(ObjectDeclaring other)
        {
            if (other is null)
            {
                return false;
            }
            return other.Type == Type;
        }

        protected IEnumerable<T> AsProperties<T>(Func<PropertyInfo, T> valueCreator)
        {
            if (valueCreator is null)
            {
                throw new ArgumentNullException(nameof(valueCreator));
            }

            Debug.Assert(Type != null);

            if (!Type.IsClass)
            {
                yield break;
            }
            PropertyInfo[] properties = Type.GetProperties();
            var len = properties.Length;
            for (int i = 0; i < len; i++)
            {
                var item = properties[i];
                if (CanProxy(item))
                {
                    yield return valueCreator(item);
                }
            }
        }

        protected virtual bool CanProxy(PropertyInfo propertyInfo)
        {
            if (propertyInfo.GetIndexParameters().Length != 0)
            {
                return false;
            }
            EditorBrowsableAttribute attr = propertyInfo.GetCustomAttribute<EditorBrowsableAttribute>();
            if (attr is null)
            {
                return true;
            }
            return attr.State != EditorBrowsableState.Never;
        }
        protected virtual IPropertyDeclare CreatePropertyDeclare(PropertyInfo propertyInfo)
        {
            return new PropertyDeclare(propertyInfo);
        }
        public override string ToString()
        {
            return $"{{{Type.FullName}}}";
        }

        public IEnumerable<IPropertyDeclare> GetPropertyDeclares()
        {
            return AsProperties(CreatePropertyDeclare);
        }

    }
}
