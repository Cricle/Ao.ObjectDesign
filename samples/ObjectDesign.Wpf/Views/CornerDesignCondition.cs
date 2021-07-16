using Ao.ObjectDesign.Wpf.Conditions;
using Ao.ObjectDesign.Wpf.Designing;

namespace ObjectDesign.Wpf.Views
{
    public class CornerDesignCondition : TemplateCondition<CornerRadiusDesigner>
    {
        protected override string GetResourceKey()
        {
            return "ObjectDesign.CornerDesign";
        }
    }
}
