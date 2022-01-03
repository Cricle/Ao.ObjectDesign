using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Conditions;
using ObjectDesign.Brock.Components;

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
