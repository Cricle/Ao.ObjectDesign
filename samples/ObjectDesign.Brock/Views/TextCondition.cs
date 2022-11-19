
using Ao.ObjectDesign.Conditions;
using ObjectDesign.Brock.Controls;
using System.Windows.Controls;
using Ao.ObjectDesign;

namespace ObjectDesign.Brock.Views
{
    public class TextCondition : TemplateCondition
    {
        public override bool CanBuild(WpfTemplateForViewBuildContext context)
        {
            return context.PropertyProxy.PropertyInfo.Name == nameof(TextBlock.Text) &&
                (context.PropertyProxy.DeclaringInstance is TextBlockSetting ||
                context.PropertyProxy.DeclaringInstance is TextBoxSetting);
        }

        protected override string GetResourceKey()
        {
            return "ObjectDesign.Text";
        }
    }
}
