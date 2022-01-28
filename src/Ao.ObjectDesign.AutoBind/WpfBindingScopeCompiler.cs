using Ao.ObjectDesign.Bindings.BindingCreators;
using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Data;
using System;
using System.Reflection;

namespace Ao.ObjectDesign.AutoBind
{
    public class WpfBindingScopeCompiler : BindingScopeCompiler<IBindingMaker>
    {
        public WpfBindingScopeCompiler(Type settingType, Type uiType) : base(settingType, uiType)
        {
        }

        protected virtual string GetVisitPath(PropertyInfo property, BindForAttribute bindFor)
        {
            return bindFor.VisitPath;
        }
        protected override IBindingMaker CreateScope(PropertyInfo property, BindForAttribute bindFor)
        {
            var descs = DependencyObjectHelper.GetDependencyPropertyDescriptors(bindFor.DependencyObjectType);
            if (descs.TryGetValue(bindFor.PropertyName, out var desc))
            {
                var visiPath = GetVisitPath(property, bindFor);
                var maker = desc.DependencyProperty.Creator(visiPath);
                return maker;
            }
            return null;
        }
    }
}
