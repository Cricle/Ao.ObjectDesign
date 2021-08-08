using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Wpf.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class DesignUnit<TDesignObject> : IDesignUnit<TDesignObject>
        where TDesignObject : class
    {
        private DesignUnit()
        {
            bindingScopes = new List<IWithSourceBindingScope>();
        }

        protected DesignUnit(UIElement ui, TDesignObject designingObject)
            : this()
        {
            UI = ui ?? throw new ArgumentNullException(nameof(ui));
            DesigningObject = designingObject ?? throw new ArgumentNullException(nameof(designingObject));
            Mapping = new DesignMapping(designingObject.GetType(), ui.GetType());
        }
        protected DesignUnit(UIElement ui, TDesignObject designingObject, DesignMapping mapping)
            : this()
        {
            UI = ui ?? throw new ArgumentNullException(nameof(ui));
            DesigningObject = designingObject ?? throw new ArgumentNullException(nameof(designingObject));
            Mapping = mapping ?? throw new ArgumentNullException(nameof(mapping));
        }

        private readonly List<IWithSourceBindingScope> bindingScopes;

        public DesignMapping Mapping { get; }

        public UIElement UI { get; }

        public IReadOnlyList<IWithSourceBindingScope> BindingScopes => bindingScopes;

        public TDesignObject DesigningObject { get; }

        public void Build()
        {
            Debug.Assert(bindingScopes != null);
            bindingScopes.Clear();
            var bindingScope = CreateWithSourceBindingScope();
            bindingScopes.AddRange(bindingScope);
        }

        protected abstract IEnumerable<IWithSourceBindingScope> CreateWithSourceBindingScope();

        public virtual void AddScopes(params IWithSourceBindingScope[] scopes)
        {
            Debug.Assert(bindingScopes != null);
            bindingScopes.AddRange(scopes);
        }

        public virtual IReadOnlyList<BindingExpressionBase> Bind()
        {
            return bindingScopes.Select(x => x.Bind(UI)).ToList();
        }
    }
}
