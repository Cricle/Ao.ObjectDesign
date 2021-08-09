using Ao.ObjectDesign;
using System.Diagnostics;
using System.Reflection;

namespace Ao.ObjectDesign.Data
{
    public class DesignEmitDataBinding : DesignPropertyDataBinding
    {
        private PropertyGetter sourcePropertyGetter;
        private PropertyGetter targetPropertyGetter;
        private PropertySetter sourcePropertySetter;
        private PropertySetter targetPropertySetter;

        protected override void CreateSourceResource()
        {
            var sourceProperyIdentity = new PropertyIdentity(Source.GetType(),
                SourcePropertyInfo.Name);
            sourcePropertyGetter = CompiledPropertyInfo.GetGetter(sourceProperyIdentity);
            sourcePropertySetter = CompiledPropertyInfo.GetSetter(sourceProperyIdentity);
        }
        protected override void CreateTargetResource()
        {
            var targetProperyIdentity = new PropertyIdentity(Target.GetType(),
                TargetPropertyInfo.Name);
            targetPropertyGetter = CompiledPropertyInfo.GetGetter(targetProperyIdentity);
            targetPropertySetter = CompiledPropertyInfo.GetSetter(targetProperyIdentity);
        }
        protected override void OnUnBind()
        {
            base.OnUnBind();
            targetPropertyGetter = sourcePropertyGetter = null;
            targetPropertySetter = sourcePropertySetter = null;
        }
        protected override object GetValue(object instance, PropertyInfo sourceProp)
        {
            if (sourceProp == SourcePropertyInfo)
            {
                Debug.Assert(sourcePropertyGetter != null);
                return sourcePropertyGetter(instance);
            }
            Debug.Assert(targetPropertyGetter != null);
            return targetPropertyGetter(instance);
        }

        protected override void SetValue(object instance, PropertyInfo sourceProp, object value)
        {
            if (sourceProp == SourcePropertyInfo)
            {
                Debug.Assert(sourcePropertySetter != null);
                sourcePropertySetter(instance,value);
            }
            else
            {
                Debug.Assert(targetPropertySetter != null);
                targetPropertySetter(instance, value);
            }
        }
    }
}
