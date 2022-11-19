
using Ao.ObjectDesign.Conditions;
using ObjectDesign.Brock.Controls;
using Ao.ObjectDesign;

namespace ObjectDesign.Brock.Views
{
    public class MediaElementSourceCondition : TemplateCondition
    {
        public override bool CanBuild(WpfTemplateForViewBuildContext context)
        {
            bool ok = context.PropertyProxy.PropertyInfo.Name == nameof(MediaElementSetting.Source) &&
                context.PropertyProxy.DeclaringInstance is MediaElementSetting;
            return ok;
        }
        protected override string GetResourceKey()
        {
            return "ObjectDesign.MediaElementSource";
        }
    }
}
