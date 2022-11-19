
using Ao.ObjectDesign.Conditions;
using ObjectDesign.Brock.Components;
using Ao.ObjectDesign;

namespace ObjectDesign.Brock.Views
{
    public class NameCondition : TemplateCondition
    {
        public override bool CanBuild(WpfTemplateForViewBuildContext context)
        {
            bool ok = context.PropertyProxy.PropertyInfo.Name == nameof(FrameworkElementSetting.Name) &&
                context.PropertyProxy.DeclaringInstance is FrameworkElementSetting;
            return ok;
        }

        protected override string GetResourceKey()
        {
            return "ObjectDesign.Name";
        }
    }
}
