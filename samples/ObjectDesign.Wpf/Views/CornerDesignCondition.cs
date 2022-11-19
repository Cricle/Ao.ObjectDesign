using Ao.ObjectDesign.Conditions;
using Ao.ObjectDesign.Designing;

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
