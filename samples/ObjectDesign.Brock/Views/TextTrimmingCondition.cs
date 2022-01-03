using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Conditions;
using System.Windows;

namespace ObjectDesign.Brock.Views
{
    public class TextTrimmingCondition : TemplateCondition
    {
        public override bool CanBuild(WpfTemplateForViewBuildContext context)
        {
            return context.PropertyProxy.Type == typeof(TextTrimming);
        }
        protected override string GetResourceKey()
        {
            return "ObjectDesign.TextTrimming";
        }
    }
}
