using Ao.ObjectDesign.Wpf.Annotations;
using System;
using System.Reflection;

namespace Ao.ObjectDesign.Wpf.Data
{
    public class AttributeBindForGetter : IBindForGetter
    {
        public static readonly AttributeBindForGetter Instance = new AttributeBindForGetter();

        private AttributeBindForGetter() { }

        public BindForAttribute Get(PropertyInfo info)
        {
            return info.GetCustomAttribute<BindForAttribute>();
        }

        public BindForAttribute Get(Type type)
        {
            return type.GetCustomAttribute<BindForAttribute>();
        }
    }
}
