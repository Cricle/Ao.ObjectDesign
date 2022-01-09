using Ao.ObjectDesign.Bindings.Annotations;
using Ao.ObjectDesign.Designing.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ao.ObjectDesign.Bindings
{
    public static class DesignPackageCreateExtensions
    {
        public static bool HasCreateAttributes<TUI, TDesignObject>(this IDesignPair<TUI, TDesignObject> unit)
        {
            return unit.DesigningObject.GetType().IsDefined(typeof(BindingCreatorFactoryAttribute));
        }
        public static IEnumerable<TBindingScope> CreateFromAttribute<TUI, TDesignObject, TBindingScope>(this IDesignPair<TUI, TDesignObject> unit,
            IBindingCreatorState state)
        {
            return CreateFromAttribute(unit, state, x => (IBindingCreator<TUI, TDesignObject, TBindingScope>)Activator.CreateInstance(x));
        }
        public static IEnumerable<IBindingCreator<TUI, TDesignObject, TBindingScope>> CreateBindingCreatorFromAttribute<TUI, TDesignObject, TBindingScope>(this IDesignPair<TUI, TDesignObject> unit)
        {
            return CreateBindingCreatorFromAttribute(unit, x => (IBindingCreator<TUI, TDesignObject, TBindingScope>)ReflectionHelper.Create(x));
        }
        public static IEnumerable<IBindingCreator<TUI, TDesignObject, TBindingScope>> CreateBindingCreatorFromAttribute<TUI, TDesignObject, TBindingScope>(this IDesignPair<TUI, TDesignObject> unit,
            Func<Type, IBindingCreator<TUI, TDesignObject, TBindingScope>> creatorFactory)
        {
            if (unit is null)
            {
                throw new ArgumentNullException(nameof(unit));
            }

            return unit.DesigningObject.GetType()
                .GetCustomAttributes(typeof(BindingCreatorFactoryAttribute))
                .OfType<BindingCreatorFactoryAttribute>()
                .Select(x => x.CreatorType)
                .Select(creatorFactory);
        }
        public static IEnumerable<TBindingScope> CreateFromAttribute<TUI, TDesignObject, TBindingScope>(this IDesignPair<TUI, TDesignObject> unit,
            IBindingCreatorState state,
            Func<Type, IBindingCreator<TUI, TDesignObject, TBindingScope>> creatorFactory)
        {
            if (unit is null)
            {
                throw new ArgumentNullException(nameof(unit));
            }

            return CreateBindingCreatorFromAttribute(unit, creatorFactory)
                .SelectMany(x => x.BindingScopes);
        }
    }
}
