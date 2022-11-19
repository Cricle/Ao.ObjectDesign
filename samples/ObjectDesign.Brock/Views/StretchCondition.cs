using Ao.ObjectDesign.Conditions;
using System.Windows.Media;

namespace ObjectDesign.Brock.Views
{
    public class StretchCondition : TemplateCondition<Stretch>
    {
        protected override string GetResourceKey()
        {
            return "ObjectDesign.Stretch";
        }
    }
}
