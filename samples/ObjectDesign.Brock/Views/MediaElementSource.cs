using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Conditions;
using ObjectDesign.Brock.Controls;

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
