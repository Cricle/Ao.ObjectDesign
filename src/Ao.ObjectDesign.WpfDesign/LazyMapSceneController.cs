using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class LazyMapSceneController<TDesignObject> : MapSceneController<TDesignObject>
    {
        private readonly Dictionary<TDesignObject, LazyBindingBox<TDesignObject>> bindingTaskMap;

        public bool LazyBinding { get; set; }

        public Dictionary<TDesignObject, LazyBindingBox<TDesignObject>> BindingTaskMap => bindingTaskMap;

        protected LazyMapSceneController(IDesignPackage<TDesignObject> designMap) : base(designMap)
        {
            bindingTaskMap = new Dictionary<TDesignObject, LazyBindingBox<TDesignObject>>();
        }

        public IReadOnlyList<BindingExpressionBase> ExecuteBinding()
        {
            return ExecuteBinding(-1, false);
        }
        public IReadOnlyList<BindingExpressionBase> ExecuteBinding(int batch)
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
        protected IReadOnlyList<BindingExpressionBase> CoreExecuteBinding(int batch, bool usingDoEvents,bool needReturns)
        {
            int count = 0;
            if (needReturns)
            {
                count = bindingTaskMap.Values.Sum(x => x.BindingScopes.Count);
            }
            var exp = new List<BindingExpressionBase>(count);

            if (usingDoEvents)
            {
                var currentCount = 0;
                foreach (var item in bindingTaskMap.Values)
                {
                    foreach (var scope in item.BindingScopes)
                    {
                        var res = scope.Bind(item.Pair.UI);
                        if (needReturns)
                        {
                            exp.Add(res);
                        }
                        currentCount++;
                        if (currentCount > batch)
                        {
                            DoEventsHelper.DoEvents();
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
            var inners = Nexts.Values.OfType<LazyMapSceneController<TDesignObject>>()
                .SelectMany(x => x.ExecuteBinding(batch, usingDoEvents));
            return exp.Concat(inners).ToList();

        }
        public IReadOnlyList<BindingExpressionBase> ExecuteBinding(int batch, bool usingDoEvents)
        {
            return CoreExecuteBinding(batch, usingDoEvents, true);
        }
        protected override void RunBinding(IDesignPair<UIElement, TDesignObject> unit, IEnumerable<IWithSourceBindingScope> scopes)
        {
            if (LazyBinding)
            {
                bindingTaskMap.Remove(unit.DesigningObject);
                bindingTaskMap.Add(unit.DesigningObject, new LazyBindingBox<TDesignObject>(unit, scopes.ToList()));
            }
            else
            {
                base.RunBinding(unit, scopes);
            }
        }
    }
}
