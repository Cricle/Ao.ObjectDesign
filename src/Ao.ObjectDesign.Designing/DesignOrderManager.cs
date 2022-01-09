using Ao.ObjectDesign.Designing.Annotations;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ao.ObjectDesign.Designing
{
    public class DesignOrderManager : Dictionary<PropertyIdentity, int>
    {
        public void AddWithType(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var props = type.GetProperties();
            var len = props.Length;
            for (int i = 0; i < len; i++)
            {
                var item = props[i];
                var attr = item.GetCustomAttribute<DesignOrderAttribute>();
                if (attr != null)
                {
                    var identity = new PropertyIdentity(item.DeclaringType, item.Name);
                    if (!ContainsKey(identity))
                    {
                        this[identity] = attr.Order;
                    }
                }
            }
        }
    }
}
