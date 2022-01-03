using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Conditions;
using ObjectDesign.Brock.Components;

namespace ObjectDesign.Brock.Views
{
    public class PositionSizeCondition : TemplateCondition
    {
        public override bool CanBuild(WpfTemplateForViewBuildContext context)
        {
            return context.PropertyProxy.Type == typeof(PositionSize);
        }

        protected override string GetResourceKey()
        {
            return "ObjectDesign.PositionSize";
        }
    }
}
