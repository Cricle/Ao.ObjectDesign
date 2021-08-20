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
            return unit.DesigningObject.GetType().IsDefined(typeof(BindingCreatorAttribute));
        }
        public static IEnumerable<IBindingScope> CreateFromAttribute<TDesignObject>(this IDesignPair<UIElement, TDesignObject> unit,
            IBindingCreatorState state)
        {
            return CreateFromAttribute(unit, state, x => (IBindingCreator<TDesignObject>)Activator.CreateInstance(x));
        }
        public static IEnumerable<IBindingScope> CreateFromAttribute<TDesignObject>(this IDesignPair<UIElement, TDesignObject> unit,
            IBindingCreatorState state,
            Func<Type, IBindingCreator<TDesignObject>> creatorFactory)
        {
            if (unit is null)
            {
                throw new ArgumentNullException(nameof(unit));
            }

            return unit.DesigningObject.GetType()
                .GetCustomAttributes(typeof(BindingCreatorAttribute))
                .OfType<BindingCreatorAttribute>()
                .Select(x => x.CreatorType)
                .Select(creatorFactory)
                .SelectMany(x => x.Create(unit, state));
        }
    }
}
