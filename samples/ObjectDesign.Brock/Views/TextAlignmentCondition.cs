using Ao.ObjectDesign.Conditions;
using System.Windows;

namespace ObjectDesign.Brock.Views
{
    public class TextAlignmentCondition : TemplateCondition<TextAlignment>
    {
        protected override string GetResourceKey()
        {
            return "ObjectDesign.TextAlignment";
        }
    }
}
