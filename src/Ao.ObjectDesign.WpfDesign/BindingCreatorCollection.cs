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
    public class BindingCreatorCollection<TDesignObject> : List<IBindingCreator<TDesignObject>>
    {
        public BindingCreatorCollection()
        {
        }

        public BindingCreatorCollection(int capacity) : base(capacity)
        {
        }

        public BindingCreatorCollection(IEnumerable<IBindingCreator<TDesignObject>> collection) : base(collection)
        {
        }

        public virtual IEnumerable<IBindingCreator<TDesignObject>> FindBindingCreators(IDesignPair<UIElement,TDesignObject> unit, IBindingCreatorState state)
        {
            return this.OrderByDescending(x => x.Order).Where(x => x.IsAccept(unit, state));
        }

        public IBindingCreator<TDesignObject> FindBindingCreator(IDesignPair<UIElement, TDesignObject> unit, IBindingCreatorState state)
        {
            return FindBindingCreators(unit, state).FirstOrDefault();
        }

        public IReadOnlyList<IBindingCreator<TDesignObject>> AddFromAttributeBindingCreator(Type type)
        {
            var attrs = type.GetCustomAttributes(typeof(BindingCreatorAttribute))
                .OfType<BindingCreatorAttribute>()
                .Select(x => CreateCreator(x.CreatorType))
                .ToList();
            AddRange(attrs);
            return attrs;
        }
        protected virtual IBindingCreator<TDesignObject> CreateCreator(Type type)
        {
            return (IBindingCreator<TDesignObject>)Activator.CreateInstance(type);
        }
    }
}
