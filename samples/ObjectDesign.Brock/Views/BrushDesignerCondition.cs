using Ao.ObjectDesign.Conditions;
using Ao.ObjectDesign.Designing;

namespace ObjectDesign.Brock.Views
{
    public class BrushDesignerCondition : TemplateCondition<BrushDesigner>
    {
        protected override string GetResourceKey()
        {
            return "ObjectDesign.BrushDesigner";
        }
    }
}
