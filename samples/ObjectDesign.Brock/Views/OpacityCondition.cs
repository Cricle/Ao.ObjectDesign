
using Ao.ObjectDesign.Conditions;
using ObjectDesign.Brock.Components;
using Ao.ObjectDesign;

namespace ObjectDesign.Brock.Views
{
    public class OpacityCondition : TemplateCondition
    {
        public override bool CanBuild(WpfTemplateForViewBuildContext context)
        {
            bool ok = context.PropertyProxy.PropertyInfo.Name == nameof(UIElementSetting.Opacity) &&
                context.PropertyProxy.DeclaringInstance is UIElementSetting;
            return ok;
        }

        protected override string GetResourceKey()
        {
            return "ObjectDesign.Opacity";
        }
    }
}
