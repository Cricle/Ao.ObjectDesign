using Ao.ObjectDesign.Conditions;
using Ao.ObjectDesign.Designing;

namespace ObjectDesign.Brock.Views
{
    public class RotateTransformDesignerCondition : TemplateCondition<RotateTransformDesigner>
    {
        protected override string GetResourceKey()
        {
            return "ObjectDesign.RotateTransformDesigner";
        }
    }
}
