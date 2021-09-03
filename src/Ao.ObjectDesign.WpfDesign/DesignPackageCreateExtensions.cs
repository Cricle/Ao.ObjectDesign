using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.WpfDesign.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public static class DesignPackageCreateExtensions
    {
        public static bool HasCreateAttributes<TDesignObject>(this IDesignPair<UIElement, TDesignObject> unit)
        {
            return unit.DesigningObject.GetType().IsDefined(typeof(BindingCreatorFactoryAttribute));
        }
        public static IEnumerable<IWithSourceBindingScope> CreateFromAttribute<TDesignObject>(this IDesignPair<UIElement, TDesignObject> unit,
            IBindingCreatorState state)
        {
            return CreateFromAttribute(unit, state, x => (IBindingCreator<TDesignObject>)Activator.CreateInstance(x));
        }
        public static IEnumerable<IBindingCreator<TDesignObject>> CreateBindingCreatorFromAttribute<TDesignObject>(this IDesignPair<UIElement, TDesignObject> unit)
        {
            return CreateBindingCreatorFromAttribute(unit, x => (IBindingCreator<TDesignObject>)ReflectionHelper.Create(x));
        }
        public static IEnumerable<IBindingCreator<TDesignObject>> CreateBindingCreatorFromAttribute<TDesignObject>(this IDesignPair<UIElement, TDesignObject> unit,
            Func<Type, IBindingCreator<TDesignObject>> creatorFactory)
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
        public static IEnumerable<IWithSourceBindingScope> CreateFromAttribute<TDesignObject>(this IDesignPair<UIElement, TDesignObject> unit,
            IBindingCreatorState state,
            Func<Type, IBindingCreator<TDesignObject>> creatorFactory)
        {
            if (unit is null)
            {
                throw new ArgumentNullException(nameof(unit));
            }

            return CreateBindingCreatorFromAttribute(unit,creatorFactory)
                .SelectMany(x => x.BindingScopes);
        }
    }
}
