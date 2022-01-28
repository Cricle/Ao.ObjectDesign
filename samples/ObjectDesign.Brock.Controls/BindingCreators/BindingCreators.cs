using Ao.ObjectDesign;
using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.WpfDesign;
using ObjectDesign.Brock.Components;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    public static class BindingCreators
    {
        private static readonly Dictionary<(Type, Type), Type> bindingCreatorTypeMap = new Dictionary<(Type, Type), Type>();
        
        public static IWpfBindingCreator<UIElementSetting> Create<TSetting>()
        {
            return Create(typeof(TSetting));
        }
        public static IWpfBindingCreator<UIElementSetting> Create(Type setting)
        {
            var uiType = setting.GetCustomAttribute<MappingForAttribute>();
            if (uiType == null)
            {
                throw new ArgumentException($"Type {setting} has not mapping for attribute");
            }
            return Create(uiType.Type,setting);
        }

        public static IWpfBindingCreator<UIElementSetting> Create(Type ui, Type setting)
        {
            var key = (ui, setting);
            if (!bindingCreatorTypeMap.TryGetValue(key, out var bct))
            {
                bct = typeof(BrockAutoBindingCreator<,>).MakeGenericType(ui, setting);
                bindingCreatorTypeMap[key] = bct;
            }
            return (IWpfBindingCreator<UIElementSetting>)ReflectionHelper.Create(bct);
        }
    }
}
