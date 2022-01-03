using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Conditions;
using ObjectDesign.Brock.Components;

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
