using Ao.ObjectDesign.Bindings.Annotations;
using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Annotations;
using Ao.ObjectDesign.Session.BindingCreators;
using Ao.ObjectDesign.WpfDesign;
using System;
using System.Reflection;

namespace Ao.ObjectDesign.Session.BuildIn
{
    public partial class ControlBuildIn<TScene, TSetting>
        where TScene : IDesignScene<TSetting>
    {
        public ControlBuildIn<TScene, TSetting> WithType<T>()
        {
            return WithType(typeof(T));
        }
        public ControlBuildIn<TScene, TSetting> WithType(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (!type.IsClass || type.IsAbstract)
            {
                throw new ArgumentException($"Type {type} is not class or abstract");
            }
            var mapFor = type.GetCustomAttribute<MappingForAttribute>();
            if (mapFor != null)
            {
                AddMap(type, mapFor.Type);
            }
            var factoryAttr = type.GetCustomAttribute<BindingCreatorFactoryAttribute>();
            if (factoryAttr != null)
            {
                var factory = (BindingCreatorFactory<TSetting>)Activator.CreateInstance(factoryAttr.CreatorType);
                AddBindingCreatorFactory(factory);
            }
            var icAttr = type.GetCustomAttribute<HasInstanceCreatorAttribute>();
            if (icAttr != null)
            {
                var ic = (IInstanceFactory)Activator.CreateInstance(icAttr.CreatorType);
                AddInstacnceFactory(ic);
            }
            return this;
        }
    }
}
