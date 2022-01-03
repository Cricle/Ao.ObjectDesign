using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Conditions;
using ObjectDesign.Brock.Components;

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
