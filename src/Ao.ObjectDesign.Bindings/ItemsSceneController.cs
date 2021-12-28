using Ao.ObjectDesign.Designing.Level;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Bindings
{
    public abstract class ItemsSceneController<TUI, TDesignObject, TBindingScope, TExpression> : LazyMapSceneController<TUI, TDesignObject, TBindingScope, TExpression>
    {
        public ItemsSceneController(IDesignPackage<TUI, TDesignObject, TBindingScope> designMap, IList uiElements)
            : base(designMap)
        {
            UIElements = uiElements ?? throw new ArgumentNullException(nameof(uiElements));
        }

        public IList UIElements { get; }

        protected override void RemoveFromContainer(IDesignPair<TUI, TDesignObject> unit)
        {
            UIElements.Remove(unit.UI);
        }
        protected override void AddToContainer(IDesignPair<TUI, TDesignObject> unit)
        {
            UIElements.Add(unit.UI);
        }
    }
}
