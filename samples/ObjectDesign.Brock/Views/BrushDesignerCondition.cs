using Ao.ObjectDesign.Wpf.Conditions;
using Ao.ObjectDesign.Wpf.Designing;

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
