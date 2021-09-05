using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class ItemsSceneController<TDesignObject> : MapSceneController<TDesignObject>
    {
        public ItemsSceneController(IDesignPackage<TDesignObject> designMap, IList uiElements) 
            : base(designMap)
        {
            UIElements = uiElements ?? throw new ArgumentNullException(nameof(uiElements));
        }

        public IList UIElements { get; }

        protected override void RemoveFromContainer(IDesignPair<UIElement, TDesignObject> unit)
        {
            UIElements.Remove(unit.UI);
        }
        protected override void AddToContainer(IDesignPair<UIElement, TDesignObject> unit)
        {
            UIElements.Add(unit.UI);
        }
    }
    public abstract class ObservableSceneController<TDesignObject> : MapSceneController<TDesignObject>
    {
        protected ObservableSceneController(IDesignPackage<TDesignObject> designMap)
            : this(designMap, new SilentObservableCollection<object>())
        {

        }

        protected ObservableSceneController(IDesignPackage<TDesignObject> designMap, SilentObservableCollection<object> items)
            : base(designMap)
        {
            Items = items;
        }

        public SilentObservableCollection<object> Items { get; }

        protected override void AddToContainer(IDesignPair<UIElement, TDesignObject> unit)
        {
            Items.Add(unit.UI);
        }

        protected override void RemoveFromContainer(IDesignPair<UIElement, TDesignObject> unit)
        {
            Items.Remove(unit.UI);
        }
    }
    public abstract class MapSceneController<TDesignObject> : DesignSceneController<UIElement, TDesignObject>
    {
        protected MapSceneController(IDesignPackage<TDesignObject> designMap)
        {
            DesignPackage = designMap ?? throw new ArgumentNullException(nameof(designMap));
            DesignMap = designMap.UIDesinMap;
            UseUnitDesignAttribute = false;
            bindingTasks = new List<Func<BindingExpressionBase>>();
            bindingCreatorMap = new Dictionary<IDesignPair<UIElement, TDesignObject>, IEnumerable<IBindingCreator<TDesignObject>>>();
        }
        private readonly List<Func<BindingExpressionBase>> bindingTasks;

        private readonly Dictionary<IDesignPair<UIElement, TDesignObject>, IEnumerable<IBindingCreator<TDesignObject>>> bindingCreatorMap; 
       
        public bool LazyBinding { get; set; }

        public List<Func<BindingExpressionBase>> BindingTasks => bindingTasks;

        public UIDesignMap DesignMap { get; }

        public IDesignPackage<TDesignObject> DesignPackage { get; }

        public bool UseUnitDesignAttribute { get; set; }

        protected override void AddUIElement(IDesignPair<UIElement, TDesignObject> unit)
        {
            AddToContainer(unit);
            var creators = BindDesignUnit(unit);
            Debug.Assert(!bindingCreatorMap.ContainsKey(unit));
            bindingCreatorMap.Add(unit, creators);
            foreach (var item in creators)
            {
                item.Attack();
            }
        }
        protected abstract void RemoveFromContainer(IDesignPair<UIElement, TDesignObject> unit);
        protected abstract void AddToContainer(IDesignPair<UIElement, TDesignObject> unit);

        protected override void RemoveUIElement(IDesignPair<UIElement, TDesignObject> unit)
        {
            RemoveFromContainer(unit);
            var val = bindingCreatorMap[unit];
            bindingCreatorMap.Remove(unit);
            foreach (var item in val)
            {
                item.Detack();
            }
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

        protected virtual IEnumerable<IBindingCreator<TDesignObject>> BindDesignUnit(IDesignPair<UIElement, TDesignObject> unit)
        {
            var lazy = LazyBinding;

            var state = DesignPackage.CreateBindingCreatorState(unit);
            var creators = Compile(unit, state);
            var scopes = creators.SelectMany(x => x.BindingScopes);

            if (lazy)
            {
                var funcs = scopes.Select(x => new Func<BindingExpressionBase>(
                    () => x.Bind(unit.UI)));
                bindingTasks.AddRange(funcs);
            }
            else
            {
                foreach (var item in scopes)
                {
                    item.Bind(unit.UI);
                }
            }
            return creators;
        }
        protected virtual IEnumerable<IBindingCreator<TDesignObject>> Compile(IDesignPair<UIElement, TDesignObject> unit,IBindingCreatorState state)
        {
            if (UseUnitDesignAttribute && unit.HasCreateAttributes())
            {
                return unit.CreateBindingCreatorFromAttribute();
            }
            return DesignPackage.GetBindingCreatorFactorys(unit, state)
                .SelectMany(x => x.Create(unit,state));
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
