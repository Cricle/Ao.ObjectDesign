using System;
using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign
{
    public abstract class ObjectAccesstor : IObjectAccesstor
    {
        protected ObjectAccesstor(object instance)
        {
            Instance = instance ?? throw new System.ArgumentNullException(nameof(instance));
        }

        public object Instance { get; }

        public abstract IEnumerable<string> Names { get; }

        public abstract object GetValue(string name);

        public virtual bool HasName(string name)
        {
            return Names.Any(x => x == name);
        }

        public abstract void SetValue(string name, object value);

        public static ObjectAccesstor Create(object instance)
        {
            if (!instance.GetType().IsClass|| instance is string)
            {
                throw new ArgumentException("ObjectAccesstor can't design valuetype or string");
            }
            if (instance is IDictionary<string ,object> map)
            {
                return new MapObjectAccesstor(map);
            }
            else
            {
                return new DefaultObjectAccesstor(instance);
            }
        }
    }
}
