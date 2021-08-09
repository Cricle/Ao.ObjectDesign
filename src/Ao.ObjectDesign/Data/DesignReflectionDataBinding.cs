using System.Reflection;

namespace Ao.ObjectDesign.Data
{
    public class DesignReflectionDataBinding : DesignPropertyDataBinding
    {
        protected override object GetValue(object instance, PropertyInfo sourceProp)
        {
            return sourceProp.GetValue(instance);
        }

        protected override void SetValue(object instance, PropertyInfo sourceProp, object value)
        {
            sourceProp.SetValue(instance, value);
        }
    }
}
