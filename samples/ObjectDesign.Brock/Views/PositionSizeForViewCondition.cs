
using Ao.ObjectDesign.Conditions;
using ObjectDesign.Brock.Components;
using Ao.ObjectDesign;

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
