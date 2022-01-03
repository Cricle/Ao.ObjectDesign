using Ao.ObjectDesign.Wpf.Conditions;
using ObjectDesign.Brock.Controls;

namespace ObjectDesign.Brock.Views
{
    public class ResourceIdentityCondition : TemplateCondition<ResourceIdentity>
    {
        protected override string GetResourceKey()
        {
            return "ObjectDesign.ResourceIdentity";
        }
    }
}
