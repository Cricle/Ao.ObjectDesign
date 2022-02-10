using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Bindings.Annotations;
using Ao.ObjectDesign.Bindings.BindingCreators;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Designing.Level;
using FastExpressionCompiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.AutoBind
{

    public static class BindingCreators<TUI,TDesignObject,TBindingScope>
    {
        private static readonly Dictionary<UISpiritType, CreateBindingCreator<TUI,TDesignObject,TBindingScope>> bindingCreatorTypeMap = new Dictionary<UISpiritType, CreateBindingCreator<TUI, TDesignObject, TBindingScope>>();
        private static readonly Dictionary<Type, IBindingCreatorFactory<TUI, TDesignObject, TBindingScope>> factoryMap = new Dictionary<Type, IBindingCreatorFactory<TUI, TDesignObject, TBindingScope>>();

        public static IBindingCreatorFactory<TUI, TDesignObject, TBindingScope> CreateFactory(Type settingType)
        {
            return CreateFactory(settingType, true);
        }
        public static IBindingCreatorFactory<TUI, TDesignObject, TBindingScope> CreateFactory(Type settingType, bool cache)
        {
            if (cache)
            {
                if (factoryMap.TryGetValue(settingType, out var cacheFc))
                {
                    return cacheFc;
                }
            }

            var creatorAttrs = settingType.GetCustomAttributes<BindingCreatorAttribute>();

            var creators = creatorAttrs.Select(x => GetCreator(x.UIType, x.SettingType)).ToList();
            var factory = new ActionsBindingCreatorFactory<TUI,TDesignObject,TBindingScope>(settingType, creators);
            if (cache)
            {
                factoryMap[settingType] = factory;
            }
            return factory;
        }
        public static IBindingCreatorFactory<TUI, TDesignObject, TBindingScope> CreateFactory(Type settingType, IEnumerable<CreateBindingCreator<TUI, TDesignObject, TBindingScope>> creators)
        {
            return new ActionsBindingCreatorFactory<TUI, TDesignObject, TBindingScope>(settingType, creators);
        }

        public static CreateBindingCreator<TUI, TDesignObject, TBindingScope> GetCreator<TSetting>(Type designObjectType)
        {
            return GetCreator(typeof(TSetting), designObjectType);
        }
        public static CreateBindingCreator<TUI, TDesignObject, TBindingScope> GetCreator(Type setting, Type designObjectType)
        {
            var uiType = setting.GetCustomAttribute<MappingForAttribute>();
            if (uiType == null)
            {
                throw new ArgumentException($"Type {setting} has not mapping for attribute");
            }
            return GetCreator(uiType.Type, setting, designObjectType);
        }

        public static CreateBindingCreator<TUI, TDesignObject, TBindingScope> GetCreator(Type uiType, Type settingType,Type designObjectType)
        {
            if (uiType is null)
            {
                throw new ArgumentNullException(nameof(uiType));
            }

            if (settingType is null)
            {
                throw new ArgumentNullException(nameof(settingType));
            }

            var key = new UISpiritType(uiType, settingType);
            if (!bindingCreatorTypeMap.TryGetValue(key, out var bct))
            {
                var t = typeof(AutoBindingCreator<,,>).MakeGenericType(uiType, settingType, designObjectType);
                bct = Compile(t);
                bindingCreatorTypeMap[key] = bct;
            }
            return bct;
        }
        private static CreateBindingCreator<TUI, TDesignObject, TBindingScope> Compile(Type type)
        {
            var par1 = Expression.Parameter(typeof(IDesignPair<TUI, TDesignObject>));
            var par2 = Expression.Parameter(typeof(IBindingCreatorState));

            var body = Expression.New(type.GetConstructors()[0], par1, par2);

            return Expression.Lambda<CreateBindingCreator<TUI, TDesignObject, TBindingScope>>(body, par1, par2)
                .CompileFast();
        }
    }
}
