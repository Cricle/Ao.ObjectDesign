using Ao.ObjectDesign.Designing.Annotations;
using System;
using System.Reflection;
using Ao.ObjectDesign.Designing;

namespace Ao.ObjectDesign.AutoBind
{
    public class AttributeBindingScopeCompiler : WpfBindingScopeCompiler
    {
        public AttributeBindingScopeCompiler(Type settingType, Type uiType) : base(settingType, uiType)
        {
        }

        protected override string GetVisitPath(PropertyInfo property, BindForAttribute bindFor)
        {
            var pathAttr = property.GetCustomAttribute<BindVisitPathAttribute>();
            if (pathAttr != null)
            {
                return pathAttr.VisiPath;
            }
            if (property.PropertyType.GetCustomAttribute<DesignForAttribute>() != null)
            {
                var box = DynamicTypePropertyHelper.FindFirstVirtualProperty(property.PropertyType);
                if (box != null)
                {
                    return string.Concat(property.Name, ".", box.Name);
                }
            }
            return base.GetVisitPath(property, bindFor);
        }
    }
}
