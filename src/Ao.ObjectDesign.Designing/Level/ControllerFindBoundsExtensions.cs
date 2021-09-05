using System;
using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign.Designing.Level
{
    public static class ControllerFindBoundsExtensions
    {
        class DesignWithController<TUI, TDesignObject>
        {
            public DesignSceneController<TUI, TDesignObject> Controller;

            public IDesignPair<TUI, TDesignObject> Pair;
        }
        public static IReadOnlyList<IDesignPair<TUI, TDesignObject>> Remove<TUI, TDesignObject>(this DesignSceneController<TUI, TDesignObject> controller,
            IReadOnlyHashSet<TUI> uis)
        {
            return Remove(controller, (x, y) => uis.Contains(y.UI));
        }
        public static IReadOnlyList<IDesignPair<TUI, TDesignObject>> Remove<TUI, TDesignObject>(this DesignSceneController<TUI, TDesignObject> controller,
            IReadOnlyHashSet<TDesignObject> uis)
        {
            return Remove(controller, (x, y) => uis.Contains(y.DesigningObject));
        }
        public static IReadOnlyList<IDesignPair<TUI, TDesignObject>> Remove<TUI, TDesignObject>(this DesignSceneController<TUI, TDesignObject> controller,
            Func<DesignSceneController<TUI, TDesignObject>, IDesignPair<TUI, TDesignObject>, bool> condition)
        {
            var pairs = Find(controller, condition, (x, y) => new DesignWithController<TUI, TDesignObject>
            {
                Controller = x,
                Pair = y
            });
            foreach (var item in pairs)
            {
                item.Controller.Scene.DesigningObjects.Remove(item.Pair.DesigningObject);
            }
            return pairs.Select(x => x.Pair).ToList();
        }
        public static IEnumerable<IDesignPair<TUI, TDesignObject>> Find<TUI, TDesignObject>(this DesignSceneController<TUI, TDesignObject> controller,
            IReadOnlyHashSet<TUI> uis)
        {
            return Find(controller, (x, y) => uis.Contains(y.UI));
        }
        public static IEnumerable<DesignSceneController<TUI, TDesignObject>> FindControllers<TUI, TDesignObject>(this DesignSceneController<TUI, TDesignObject> controller,
            IReadOnlyHashSet<TUI> uis)
        {
            return FindControllers(controller, (x, y) => uis.Contains(y.UI));
        }
        public static DesignSceneController<TUI, TDesignObject> FindController<TUI, TDesignObject>(this DesignSceneController<TUI, TDesignObject> controller,
            TUI ui)
            where TUI : class
        {
            return FindControllers(controller, (x, y) => y.UI == ui).FirstOrDefault();
        }
        public static DesignSceneController<TUI, TDesignObject> FindController<TUI, TDesignObject>(this DesignSceneController<TUI, TDesignObject> controller,
            TDesignObject obj)
            where TDesignObject : class
        {
            return FindControllers(controller, (x, y) => y.DesigningObject == obj).FirstOrDefault();
        }

        public static IEnumerable<DesignSceneController<TUI, TDesignObject>> FindControllers<TUI, TDesignObject>(this DesignSceneController<TUI, TDesignObject> controller,
            IReadOnlyHashSet<TDesignObject> objs)
        {
            return FindControllers(controller, (x, y) => objs.Contains(y.DesigningObject));
        }
        public static IEnumerable<DesignSceneController<TUI, TDesignObject>> FindControllers<TUI, TDesignObject>(this DesignSceneController<TUI, TDesignObject> controller,
            Func<DesignSceneController<TUI, TDesignObject>, IDesignPair<TUI, TDesignObject>, bool> condition)
        {
            foreach (var item in controller.Nexts.Values)
            {
                foreach (var next in FindControllers(item, condition))
                {
                    yield return next;
                }
            }
            if (controller.DesignUnits.Where(x => condition(controller, x)).Any())
            {
                yield return controller;
            }
        }
        public static IEnumerable<IDesignPair<TUI, TDesignObject>> Find<TUI, TDesignObject>(this DesignSceneController<TUI, TDesignObject> controller,
            IReadOnlyHashSet<TDesignObject> uis)
        {
            return Find(controller, (x, y) => uis.Contains(y.DesigningObject));
        }
        public static IEnumerable<IDesignPair<TUI, TDesignObject>> Find<TUI, TDesignObject>(this DesignSceneController<TUI, TDesignObject> controller,
            Func<DesignSceneController<TUI, TDesignObject>, IDesignPair<TUI, TDesignObject>, bool> condition)
        {
            return Find(controller, condition, (x, y) => y);
        }
        public static IEnumerable<TRet> Find<TUI, TDesignObject, TRet>(this DesignSceneController<TUI, TDesignObject> controller,
            Func<DesignSceneController<TUI, TDesignObject>, IDesignPair<TUI, TDesignObject>, bool> condition,
            Func<DesignSceneController<TUI, TDesignObject>, IDesignPair<TUI, TDesignObject>, TRet> selector)
        {
            foreach (var item in controller.Nexts.Values)
            {
                foreach (var next in Find(item, condition, selector))
                {
                    yield return next;
                }
            }
            foreach (var item in controller.DesignUnits.Where(x => condition(controller, x)))
            {
                yield return selector(controller, item);
            }
        }
        public static IEnumerable<IElementBounds<TUI, TDesignObject>> Lookup<TUI, TDesignObject>(this DesignSceneController<TUI, TDesignObject> controller,
            IReadOnlyHashSet<TUI> elements)
            where TDesignObject : IPositionBounded
        {
            return LookupCore(controller, elements, x => x.GetBounds(), DefaultVector.Zero);
        }
        public static IEnumerable<IElementBounds<TUI, TDesignObject>> Lookup<TUI, TDesignObject>(this DesignSceneController<TUI, TDesignObject> controller,
            IReadOnlyHashSet<TUI> elements,
            Func<TDesignObject, IRect> rectSelector)
        {
            return LookupCore(controller, elements, rectSelector, DefaultVector.Zero);
        }
        public static IEnumerable<IElementBounds<TUI, TDesignObject>> Lookup<TUI, TDesignObject>(this DesignSceneController<TUI, TDesignObject> controller,
            Func<IDesignPair<TUI, TDesignObject>, bool> condition)
            where TDesignObject : IPositionBounded
        {
            return LookupCore(controller, condition, x=>x.GetBounds(), DefaultVector.Zero);
        }

        public static IEnumerable<IElementBounds<TUI, TDesignObject>> Lookup<TUI, TDesignObject>(this DesignSceneController<TUI, TDesignObject> controller,
             Func<IDesignPair<TUI, TDesignObject>, bool> condition,
             Func<TDesignObject, IRect> rectSelector)
        {
            return LookupCore(controller, condition, rectSelector, DefaultVector.Zero);
        }
        public static IEnumerable<IDesignPair<TUI, TDesignObject>> Get<TUI, TDesignObject>(this DesignSceneController<TUI, TDesignObject> controller,
            Func<IDesignPair<TUI, TDesignObject>, bool> condition)
        {
            foreach (var item in controller.Nexts.Values)
            {
                foreach (var val in Get(item, condition))
                {
                    yield return val;
                }
                foreach (var val in item.DesignUnits.Where(condition))
                {
                    yield return val;
                }
            }
        }
        private static IEnumerable<IElementBounds<TUI, TDesignObject>> LookupCore<TUI, TDesignObject>(this DesignSceneController<TUI, TDesignObject> controller,
           IReadOnlyHashSet<TUI> elements,
           Func<TDesignObject, IRect> rectSelector,
           IVector offset)
        {
            return LookupCore(controller, x => elements.Contains(x.UI), rectSelector, offset);
        }
        private static IEnumerable<IElementBounds<TUI, TDesignObject>> LookupCore<TUI, TDesignObject>(this DesignSceneController<TUI, TDesignObject> controller,
            Func<IDesignPair<TUI, TDesignObject>, bool> condition,
            Func<TDesignObject, IRect> rectSelector,
            IVector offset)
        {
            IVector v = default;
            if (controller.Scene is TDesignObject d)
            {
                var rect = rectSelector(d);
                if (rect != null)
                {
                    v = new DefaultVector(rect.Left + offset.X, rect.Top + offset.Y);
                }
            }
            foreach (var item in controller.Nexts.Values)
            {
                foreach (var val in LookupCore(item, condition, rectSelector, v))
                {
                    yield return val;
                }
            }
            foreach (var val in controller.DesignUnits.Where(condition))
            {
                var rect = rectSelector(val.DesigningObject);
                var bound = new ElementBounds<TUI, TDesignObject>
                {
                    Bounds = rect,
                    Controller = controller,
                    ActualBounds = new DefaultRect(rect.Left + v.X, rect.Top + v.Y, rect.Right + v.X, rect.Bottom + v.Y),
                    Pair = val
                };
                yield return bound;
            }
        }
    }
}
