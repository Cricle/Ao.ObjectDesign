using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.WpfDesign.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public class BindingCreatorFactoryCollection<TDesignObject> : List<IBindingCreatorFactory<TDesignObject>>
    {
        public BindingCreatorFactoryCollection()
        {
        }

        public BindingCreatorFactoryCollection(int capacity) : base(capacity)
        {
        }

        public BindingCreatorFactoryCollection(IEnumerable<IBindingCreatorFactory<TDesignObject>> collection) : base(collection)
        {
        }

        public virtual IEnumerable<IBindingCreatorFactory<TDesignObject>> FindBindingCreatorFactorys(IDesignPair<UIElement,TDesignObject> unit, IBindingCreatorState state)
        {
            return this.OrderByDescending(x => x.Order).Where(x => x.IsAccept(unit, state));
        }

        public IBindingCreatorFactory<TDesignObject> FindBindingCreatorFactory(IDesignPair<UIElement, TDesignObject> unit, IBindingCreatorState state)
        {
            return FindBindingCreatorFactorys(unit, state).FirstOrDefault();
        }

        public IReadOnlyList<IBindingCreatorFactory<TDesignObject>> AddFromAttributeBindingCreator(Type type)
        {
            var attrs = type.GetCustomAttributes(typeof(BindingCreatorFactoryAttribute))
                .OfType<BindingCreatorFactoryAttribute>()
                .Select(x => CreateCreator(x.CreatorType))
                .ToList();
            AddRange(attrs);
            return attrs;
        }
        protected virtual IBindingCreatorFactory<TDesignObject> CreateCreator(Type type)
        {
            return (IBindingCreatorFactory<TDesignObject>)Activator.CreateInstance(type);
        }
    }
}
