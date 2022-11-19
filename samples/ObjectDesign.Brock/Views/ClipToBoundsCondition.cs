
using Ao.ObjectDesign.Conditions;
using ObjectDesign.Brock.Components;
using Ao.ObjectDesign;

namespace ObjectDesign.Brock.Views
{
    public class ClipToBoundsCondition : TemplateCondition
    {
        public override bool CanBuild(WpfTemplateForViewBuildContext context)
        {
            bool ok = context.PropertyProxy.PropertyInfo.Name == nameof(UIElementSetting.ClipToBounds) &&
                context.PropertyProxy.DeclaringInstance is UIElementSetting;
            return ok;
        }

        protected override string GetResourceKey()
        {
            return "ObjectDesign.ClipToBounds";
        }
    }
}
