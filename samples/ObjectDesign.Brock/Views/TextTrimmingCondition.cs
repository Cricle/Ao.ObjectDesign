
using Ao.ObjectDesign.Conditions;
using System.Windows;
using Ao.ObjectDesign;

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
