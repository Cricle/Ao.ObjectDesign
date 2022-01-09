using Ao.ObjectDesign.Bindings.Annotations;
using Ao.ObjectDesign.Designing.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ao.ObjectDesign.Bindings
{
    public class BindingCreatorFactoryCollection<TUI, TDesignObject, TBindingScope> : List<IBindingCreatorFactory<TUI, TDesignObject, TBindingScope>>
    {
        public BindingCreatorFactoryCollection()
        {
        }

        public BindingCreatorFactoryCollection(int capacity) : base(capacity)
        {
        }

        public BindingCreatorFactoryCollection(IEnumerable<IBindingCreatorFactory<TUI, TDesignObject, TBindingScope>> collection) : base(collection)
        {
        }

        public virtual IEnumerable<IBindingCreatorFactory<TUI, TDesignObject, TBindingScope>> FindBindingCreatorFactorys(IDesignPair<TUI, TDesignObject> unit, IBindingCreatorState state)
        {
            return this.OrderByDescending(x => x.Order).Where(x => x.IsAccept(unit, state));
        }

        public IBindingCreatorFactory<TUI, TDesignObject, TBindingScope> FindBindingCreatorFactory(IDesignPair<TUI, TDesignObject> unit, IBindingCreatorState state)
        {
            return FindBindingCreatorFactorys(unit, state).FirstOrDefault();
        }

        public IReadOnlyList<IBindingCreatorFactory<TUI, TDesignObject, TBindingScope>> AddFromAttributeBindingCreator(Type type)
        {
            var attrs = type.GetCustomAttributes(typeof(BindingCreatorFactoryAttribute))
                .OfType<BindingCreatorFactoryAttribute>()
                .Select(x => CreateCreator(x.CreatorType))
                .ToList();
            AddRange(attrs);
            return attrs;
        }
        protected virtual IBindingCreatorFactory<TUI, TDesignObject, TBindingScope> CreateCreator(Type type)
        {
            return (IBindingCreatorFactory<TUI, TDesignObject, TBindingScope>)Activator.CreateInstance(type);
        }
    }
}
