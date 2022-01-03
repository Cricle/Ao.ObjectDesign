using Ao.ObjectDesign.Wpf.Conditions;
using Ao.ObjectDesign.Wpf.Designing;

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
