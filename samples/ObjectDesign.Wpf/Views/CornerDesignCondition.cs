using Ao.ObjectDesign.ForView;
using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Conditions;
using Ao.ObjectDesign.Wpf.Designing;
using System.Windows;

namespace ObjectDesign.Wpf.Views
{
    public class CornerDesignCondition : TemplateCondition<CornerDesign>
    {
        protected override string GetResourceKey()
        {
            return "ObjectDesign.CornerDesign";
        }
    }
}
