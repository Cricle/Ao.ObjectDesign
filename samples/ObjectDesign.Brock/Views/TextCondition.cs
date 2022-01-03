using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Conditions;
using ObjectDesign.Brock.Controls;
using System.Windows.Controls;

namespace ObjectDesign.Brock.Views
{
    public class TextCondition : TemplateCondition
    {
        public override bool CanBuild(WpfTemplateForViewBuildContext context)
        {
            return context.PropertyProxy.PropertyInfo.Name == nameof(TextBlock.Text) &&
                (context.PropertyProxy.DeclaringInstance is TextBlockSetting||
                context.PropertyProxy.DeclaringInstance is TextBoxSetting);
        }

        protected override string GetResourceKey()
        {
            return "ObjectDesign.Text";
        }
    }
}
