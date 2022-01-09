using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Ao.ObjectDesign.Bindings
{
    public abstract class LazyMapSceneController<TUI, TDesignObject, TBindingScope, TExpression> : MapSceneController<TUI, TDesignObject, TBindingScope, TExpression>
    {
        private readonly Dictionary<TDesignObject, LazyBindingBox<TUI, TDesignObject, TBindingScope, TExpression>> bindingTaskMap;

        public bool LazyBinding { get; set; }

        public Dictionary<TDesignObject, LazyBindingBox<TUI, TDesignObject, TBindingScope, TExpression>> BindingTaskMap => bindingTaskMap;

        protected LazyMapSceneController(IDesignPackage<TUI, TDesignObject, TBindingScope> designMap) : base(designMap)
        {
            bindingTaskMap = new Dictionary<TDesignObject, LazyBindingBox<TUI, TDesignObject, TBindingScope, TExpression>>();
        }

        public IReadOnlyList<TExpression> ExecuteBinding()
        {
            return ExecuteBinding(-1, false);
        }
        public IReadOnlyList<TExpression> ExecuteBinding(int batch)
        {
            return ExecuteBinding(batch, true);
        }
        public void ExecuteBindingNoReturn(int batch, bool usingDoEvents)
        {
            CoreExecuteBinding(batch, usingDoEvents, false);
        }
        public void ExecuteBindingNoReturn(int batch)
        {
            CoreExecuteBinding(batch, true, false);
        }
        public void ExecuteBindingNoReturn()
        {
            CoreExecuteBinding(-1, false, false);
        }
        protected IReadOnlyList<TExpression> CoreExecuteBinding(int batch, bool usingDoEvents, bool needReturns)
        {
            int count = 0;
            if (needReturns)
            {
                count = bindingTaskMap.Values.Sum(x => x.BindingScopes.Count);
            }
            var exp = new List<TExpression>(count);

            if (usingDoEvents)
            {
                var currentCount = 0;
                foreach (var item in bindingTaskMap.Values)
                {
                    foreach (var scope in item.BindingScopes)
                    {
                        var res = Bind(scope, item.Pair);
                        if (needReturns)
                        {
                            exp.Add(res);
                        }
                        currentCount++;
                        if (currentCount > batch)
                        {
                            DoEvent();
                            currentCount = 0;
                        }
                    }
                }
            }
            else
            {
                foreach (var item in bindingTaskMap.Values)
                {
                    if (needReturns)
                    {
                        var res = item.ExecuteBinding();
                        exp.AddRange(res);
                    }
                    else
                    {
                        item.ExecuteBindingNoReturn();
                    }
                }
            }
            bindingTaskMap.Clear();
            var inners = Nexts.Values.OfType<LazyMapSceneController<TUI, TDesignObject, TBindingScope, TExpression>>()
                .SelectMany(x => x.ExecuteBinding(batch, usingDoEvents));
            return exp.Concat(inners).ToList();

        }
        public IReadOnlyList<TExpression> ExecuteBinding(int batch, bool usingDoEvents)
        {
            return CoreExecuteBinding(batch, usingDoEvents, true);
        }
        protected abstract LazyBindingBox<TUI, TDesignObject, TBindingScope, TExpression> CreateBindingBox(IDesignPair<TUI, TDesignObject> unit, IEnumerable<TBindingScope> scopes);
        protected virtual void DoEvent()
        {

        }
        protected override void RunBinding(IDesignPair<TUI, TDesignObject> unit, IEnumerable<TBindingScope> scopes)
        {
            if (LazyBinding)
            {
                bindingTaskMap[unit.DesigningObject] = CreateBindingBox(unit, scopes);
            }
            else
            {
                base.RunBinding(unit, scopes);
            }
        }
    }

    public abstract class MapSceneController<TUI, TDesignObject, TBindingScope, TExpression> : DesignSceneController<TUI, TDesignObject>
    {
        private static readonly IBindingCreator<TUI, TDesignObject, TBindingScope>[] emptyCreator = new IBindingCreator<TUI, TDesignObject, TBindingScope>[0];

        protected MapSceneController(IDesignPackage<TUI, TDesignObject, TBindingScope> designMap)
        {
            DesignPackage = designMap ?? throw new ArgumentNullException(nameof(designMap));
            DesignMap = designMap.UIDesinMap;
            UseUnitDesignAttribute = false;
            IgnoreBinding = false;
            bindingCreatorMap = new Dictionary<IDesignPair<TUI, TDesignObject>, IEnumerable<IBindingCreator<TUI, TDesignObject, TBindingScope>>>();
        }

        private readonly Dictionary<IDesignPair<TUI, TDesignObject>, IEnumerable<IBindingCreator<TUI, TDesignObject, TBindingScope>>> bindingCreatorMap;

        public UIDesignMap DesignMap { get; }

        public IDesignPackage<TUI, TDesignObject, TBindingScope> DesignPackage { get; }

        public bool IgnoreBinding { get; set; }

        public bool UseUnitDesignAttribute { get; set; }

        protected override void AddUIElement(IDesignPair<TUI, TDesignObject> unit)
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
        protected abstract void RemoveFromContainer(IDesignPair<TUI, TDesignObject> unit);
        protected abstract void AddToContainer(IDesignPair<TUI, TDesignObject> unit);

        protected override void RemoveUIElement(IDesignPair<TUI, TDesignObject> unit)
        {
            RemoveFromContainer(unit);
            var val = bindingCreatorMap[unit];
            bindingCreatorMap.Remove(unit);
            foreach (var item in val)
            {
                item.Detack();
            }
        }


        protected virtual IEnumerable<IBindingCreator<TUI, TDesignObject, TBindingScope>> BindDesignUnit(IDesignPair<TUI, TDesignObject> unit)
        {
            if (IgnoreBinding)
            {
                return emptyCreator;
            }
            var state = DesignPackage.CreateBindingCreatorState(unit);
            var creators = Compile(unit, state);
            var scopes = creators.SelectMany(x => x.BindingScopes);

            RunBinding(unit, scopes);

            return creators;
        }
        protected virtual void RunBinding(IDesignPair<TUI, TDesignObject> unit, IEnumerable<TBindingScope> scopes)
        {
            foreach (var item in scopes)
            {
                Bind(item, unit);
            }
        }
        protected abstract TExpression Bind(TBindingScope scope, IDesignPair<TUI, TDesignObject> unit);
        protected virtual IEnumerable<IBindingCreator<TUI, TDesignObject, TBindingScope>> Compile(IDesignPair<TUI, TDesignObject> unit, IBindingCreatorState state)
        {
            if (UseUnitDesignAttribute && unit.HasCreateAttributes())
            {
                return unit.CreateBindingCreatorFromAttribute<TUI, TDesignObject, TBindingScope>();
            }
            return DesignPackage.GetBindingCreatorFactorys(unit, state)
                .SelectMany(x => x.Create(unit, state));
        }
        protected override TUI CreateUI(TDesignObject designingObject)
        {
            var t = DesignMap.GetUIType(designingObject.GetType());
            if (t is null)
            {
                return default(TUI);
            }
            return (TUI)DesignMap.CreateByFactoryOrEmit(t);
        }
    }

}
