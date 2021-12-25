using Ao.ObjectDesign;
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
            foreach (var item in props)
            {
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
