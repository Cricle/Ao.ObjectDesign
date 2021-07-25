using Ao.ObjectDesign.Designing.Annotations;
using System;
using System.Reflection;

namespace Ao.ObjectDesign.Designing.Data
{
    public class NamedBindForGetter : IBindForGetter
    {
        public static readonly NamedBindForGetter Instance = new NamedBindForGetter();

        private NamedBindForGetter() { }

        public BindForAttribute Get(PropertyInfo info)
        {
            return new BindForAttribute(info.Name);
        }

        public BindForAttribute Get(Type type)
        {
            return type.GetCustomAttribute<BindForAttribute>();
        }
    }
}
