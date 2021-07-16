using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace Ao.ObjectDesign
{
    public class ObjectDeclaring : IObjectDeclaring
    {
        protected ObjectDeclaring()
        {

        }
        public ObjectDeclaring(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }


        public virtual Type Type { get; }

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
            foreach (PropertyInfo item in properties)
            {
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
