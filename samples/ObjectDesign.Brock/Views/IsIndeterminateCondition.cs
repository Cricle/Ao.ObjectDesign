
using Ao.ObjectDesign.Conditions;
using ObjectDesign.Brock.Controls;
using Ao.ObjectDesign;

namespace ObjectDesign.Brock.Views
{
    public class IsIndeterminateCondition : TemplateCondition
    {
        public override bool CanBuild(WpfTemplateForViewBuildContext context)
        {
            bool ok = context.PropertyProxy.PropertyInfo.Name == nameof(ProgressBarSetting.IsIndeterminate) &&
                context.PropertyProxy.DeclaringInstance is ProgressBarSetting;
            return ok;
        }

        protected override string GetResourceKey()
        {
            return "ObjectDesign.IsIndeterminate";
        }
    }
}
