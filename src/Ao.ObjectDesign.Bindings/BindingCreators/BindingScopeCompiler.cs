using Ao.ObjectDesign;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Bindings.BindingCreators
{
    public abstract class BindingScopeCompiler<TBindingMaker>
    {
        protected BindingScopeCompiler(Type settingType, Type uiType)
        {
            SettingType = settingType ?? throw new ArgumentNullException(nameof(settingType));
            UIType = uiType ?? throw new ArgumentNullException(nameof(uiType));
        }

        public Type SettingType { get; }

        public Type UIType { get; }

        public bool DefaultIsBind { get; set; } = true;

        public IEnumerable<PropertyInfo> Properties => GetProperties();

        public IEnumerable<TBindingMaker> Analysis()
        {
            var props = GetProperties();

            foreach (var prop in props)
            {
                var scope = AnalysisPart(prop);
                if (scope != null)
                {
                    yield return scope;
                }
            }
        }

        protected virtual TBindingMaker AnalysisPart(PropertyInfo property)
        {
            var bindFor = property.GetCustomAttribute<BindForAttribute>();
            if (bindFor == null && DefaultIsBind)
            {
                bindFor = new BindForAttribute(UIType, property.Name) { VisitPath = property.Name };
            }
            if (bindFor == null)
            {
                return default;
            }

            return CreateScope(property, bindFor);
        }

        protected abstract TBindingMaker CreateScope(PropertyInfo property, BindForAttribute bindFor);

        protected virtual IEnumerable<PropertyInfo> GetProperties()
        {
            var query = SettingType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.CanWrite && x.GetCustomAttribute<NotMappingPropertyAttribute>() == null);
            return query;
        }

        public static string GetAttributeOrDefaultName(PropertyInfo property, BindForAttribute bindFor)
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
            return bindFor.VisitPath;
        }

    }
}
