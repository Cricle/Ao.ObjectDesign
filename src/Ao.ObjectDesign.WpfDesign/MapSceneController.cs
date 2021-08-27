using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class MapSceneController<TDesignObject> : DesignSceneController<UIElement, TDesignObject>
    {
        protected MapSceneController(IDesignPackage<TDesignObject> designMap, UIElementCollection uiElements)
        {
            DesignPackage = designMap ?? throw new ArgumentNullException(nameof(designMap));
            UIElements = uiElements ?? throw new ArgumentNullException(nameof(uiElements));
            DesignMap = designMap.UIDesinMap;
            UseUnitDesignAttribute = true;
            bindingTasks = new List<Func<BindingExpressionBase>>();
        }
        private readonly List<Func<BindingExpressionBase>> bindingTasks;

        public bool LazyBinding { get; set; }

        public List<Func<BindingExpressionBase>> BindingTasks => bindingTasks;

        public UIDesignMap DesignMap { get; }

        public IDesignPackage<TDesignObject> DesignPackage { get; }

        public UIElementCollection UIElements { get; }

        public bool UseUnitDesignAttribute { get; set; }

        protected override void AddUIElement(IDesignPair<UIElement, TDesignObject> unit)
        {
            BindDesignUnit(unit);
            UIElements.Add(unit.UI);
        }

        protected override void RemoveUIElement(IDesignPair<UIElement, TDesignObject> unit)
        {
            UIElements.Remove(unit.UI);
        }
        
        public IReadOnlyList<BindingExpressionBase> ExecuteBinding()
        {
            var datas = bindingTasks.Select(x => x()).ToList();
            bindingTasks.Clear();
            return datas;
        }
        public IReadOnlyList<BindingExpressionBase> ExecuteBinding(int batch)
        {
            return ExecuteBinding(batch, true);
        }
        public IReadOnlyList<BindingExpressionBase> ExecuteBinding(int batch, bool usingDoEvents)
        {
            var count = bindingTasks.Count;
            var exp = new BindingExpressionBase[count];

            if (usingDoEvents)
            {
                var currentCount = 0;
                for (int i = 0; i < count; i++)
                {
                    exp[i] = bindingTasks[i]();
                    currentCount++;
                    if (currentCount > batch)
                    {
                        DoEventsHelper.DoEvents();
                        currentCount = 0;
                    }
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    exp[i] = bindingTasks[i]();
                }
            }
            return exp;

        }

        protected virtual void BindDesignUnit(IDesignPair<UIElement, TDesignObject> unit)
        {
            var lazy = LazyBinding;
            IEnumerable<IWithSourceBindingScope> bindingScopes;
            if (UseUnitDesignAttribute && unit.HasCreateAttributes())
            {
                var state = DesignPackage.CreateBindingCreatorState(unit);
                bindingScopes = unit.CreateFromAttribute(state);
            }
            else
            {
                bindingScopes = DesignPackage.CreateBindingScopes(unit);
            }
            Debug.Assert(bindingScopes != null);
            if (lazy)
            {
                var funcs = bindingScopes.Select(x => new Func<BindingExpressionBase>(
                    () => x.Bind(unit.UI)));
                bindingTasks.AddRange(funcs);
            }
            else
            {
                foreach (var item in bindingScopes)
                {
                    item.Bind(unit.UI);
                }
            }
            
        }

        protected override UIElement CreateUI(TDesignObject designingObject)
        {
            var t = DesignMap.GetUIType(designingObject.GetType());
            if (t is null)
            {
                return null;
            }
            return (UIElement)DesignMap.CreateByFactoryOrEmit(t);
        }
    }
}
