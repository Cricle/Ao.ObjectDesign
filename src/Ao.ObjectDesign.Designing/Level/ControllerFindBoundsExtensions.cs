using System;
using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign.Designing.Level
{
    public static class ControllerFindBoundsExtensions
    {
        class DesignWithController<TUI, TDesignObject>
        {
            public IDesignSceneController<TUI, TDesignObject> Controller;

            public IDesignPair<TUI, TDesignObject> Pair;
        }
        public static IReadOnlyList<IDesignPair<TUI, TDesignObject>> Get<TUI, TDesignObject>(this IDesignSceneController<TUI, TDesignObject> controller)
        {
            var lst = new List<IDesignPair<TUI, TDesignObject>>();
            var current = new List<IDesignSceneController<TUI, TDesignObject>> { controller };
            while (current.Count != 0)
            {
                lst.AddRange(current.SelectMany(x => x.DesignUnits));
                current = current.SelectMany(x => x.Nexts.Values).ToList();
            }
            return lst;
        }

        public static IReadOnlyList<IDesignPair<TUI, TDesignObject>> Remove<TUI, TDesignObject>(this IDesignSceneController<TUI, TDesignObject> controller,
            IReadOnlyHashSet<TUI> uis)
        {
            return Remove(controller, (x, y) => uis.Contains(y.UI));
        }
        public static IReadOnlyList<IDesignPair<TUI, TDesignObject>> Remove<TUI, TDesignObject>(this IDesignSceneController<TUI, TDesignObject> controller,
            IReadOnlyHashSet<TDesignObject> uis)
        {
            return Remove(controller, (x, y) => uis.Contains(y.DesigningObject));
        }
        public static IReadOnlyList<IDesignPair<TUI, TDesignObject>> Remove<TUI, TDesignObject>(this IDesignSceneController<TUI, TDesignObject> controller,
            Func<IDesignSceneController<TUI, TDesignObject>, IDesignPair<TUI, TDesignObject>, bool> condition)
        {
            var pairs = Find(controller, condition, (x, y) => new DesignWithController<TUI, TDesignObject>
            {
                Controller = x,
                Pair = y
            }).ToList();
            foreach (var item in pairs)
            {
                item.Controller.Scene.DesigningObjects.Remove(item.Pair.DesigningObject);
            }
            return pairs.Select(x => x.Pair).ToList();
        }
        public static IEnumerable<IDesignPair<TUI, TDesignObject>> Find<TUI, TDesignObject>(this IDesignSceneController<TUI, TDesignObject> controller,
            IReadOnlyHashSet<TUI> uis)
        {
            return Find(controller, (x, y) => uis.Contains(y.UI));
        }
        public static IEnumerable<IDesignSceneController<TUI, TDesignObject>> FindControllers<TUI, TDesignObject>(this IDesignSceneController<TUI, TDesignObject> controller,
            IReadOnlyHashSet<TUI> uis)
        {
            return FindControllers(controller, (x, y) => uis.Contains(y.UI));
        }
        public static IDesignSceneController<TUI, TDesignObject> FindController<TUI, TDesignObject>(this IDesignSceneController<TUI, TDesignObject> controller,
            TUI ui)
        {
            return FindControllers(controller, (x, y) => EqualityComparer<TUI>.Default.Equals(y.UI , ui)).FirstOrDefault();
        }
        public static IDesignSceneController<TUI, TDesignObject> FindController<TUI, TDesignObject>(this IDesignSceneController<TUI, TDesignObject> controller,
            TDesignObject obj)
        {
            return FindControllers(controller, (x, y) => EqualityComparer<TDesignObject>.Default.Equals(y.DesigningObject, obj)).FirstOrDefault();
        }

        public static IEnumerable<IDesignSceneController<TUI, TDesignObject>> FindControllers<TUI, TDesignObject>(this IDesignSceneController<TUI, TDesignObject> controller,
            IReadOnlyHashSet<TDesignObject> objs)
        {
            return FindControllers(controller, (x, y) => objs.Contains(y.DesigningObject));
        }
        public static IEnumerable<IDesignSceneController<TUI, TDesignObject>> FindControllers<TUI, TDesignObject>(this IDesignSceneController<TUI, TDesignObject> controller,
            Func<IDesignSceneController<TUI, TDesignObject>, IDesignPair<TUI, TDesignObject>, bool> condition)
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
        public static IEnumerable<IDesignPair<TUI, TDesignObject>> Find<TUI, TDesignObject>(this IDesignSceneController<TUI, TDesignObject> controller,
            IReadOnlyHashSet<TDesignObject> uis)
        {
            return Find(controller, (x, y) => uis.Contains(y.DesigningObject));
        }
        public static IEnumerable<IDesignPair<TUI, TDesignObject>> Find<TUI, TDesignObject>(this IDesignSceneController<TUI, TDesignObject> controller,
            Func<IDesignSceneController<TUI, TDesignObject>, IDesignPair<TUI, TDesignObject>, bool> condition)
        {
            return Find(controller, condition, (x, y) => y);
        }
        public static IEnumerable<TRet> Find<TUI, TDesignObject, TRet>(this IDesignSceneController<TUI, TDesignObject> controller,
            Func<IDesignSceneController<TUI, TDesignObject>, IDesignPair<TUI, TDesignObject>, TRet> selector)
        {
            return Find(controller, null, selector);
        }
        public static IEnumerable<TRet> Find<TUI, TDesignObject, TRet>(this IDesignSceneController<TUI, TDesignObject> controller,
            Func<IDesignSceneController<TUI, TDesignObject>, IDesignPair<TUI, TDesignObject>, bool> condition,
            Func<IDesignSceneController<TUI, TDesignObject>, IDesignPair<TUI, TDesignObject>, TRet> selector)
        {
            foreach (var item in controller.Nexts.Values)
            {
                foreach (var next in Find(item, condition, selector))
                {
                    yield return next;
                }
            }
            IEnumerable<IDesignPair<TUI, TDesignObject>> query = controller.DesignUnits;
            if (condition != null)
            {
                query = query.Where(x => condition(controller, x));
            }
            foreach (var item in query)
            {
                yield return selector(controller, item);
            }
        }
        public static IEnumerable<IElementBounds<TUI, TDesignObject>> Lookup<TUI, TDesignObject>(this IDesignSceneController<TUI, TDesignObject> controller,
            IReadOnlyHashSet<TUI> elements)
            where TDesignObject : IPositionBounded
        {
            return LookupCore(controller, elements, x => x.GetBounds(), DefaultVector.Zero);
        }
        public static IEnumerable<IElementBounds<TUI, TDesignObject>> Lookup<TUI, TDesignObject>(this IDesignSceneController<TUI, TDesignObject> controller,
            IReadOnlyHashSet<TUI> elements,
            Func<TDesignObject, IRect> rectSelector)
        {
            return LookupCore(controller, elements, rectSelector, DefaultVector.Zero);
        }
        public static IEnumerable<IElementBounds<TUI, TDesignObject>> Lookup<TUI, TDesignObject>(this IDesignSceneController<TUI, TDesignObject> controller,
            Func<IDesignPair<TUI, TDesignObject>, bool> condition)
            where TDesignObject : IPositionBounded
        {
            return LookupCore(controller, condition, x => x.GetBounds(), DefaultVector.Zero);
        }
        public static IEnumerable<IElementBounds<TUI, TDesignObject>> Lookup<TUI, TDesignObject>(this IDesignSceneController<TUI, TDesignObject> controller)
           where TDesignObject : IPositionBounded
        {
            return LookupCore(controller, (Func<IDesignPair<TUI, TDesignObject>, bool>)null, x => x.GetBounds(), DefaultVector.Zero);
        }

        public static IEnumerable<IElementBounds<TUI, TDesignObject>> Lookup<TUI, TDesignObject>(this IDesignSceneController<TUI, TDesignObject> controller,
             Func<IDesignPair<TUI, TDesignObject>, bool> condition,
             Func<TDesignObject, IRect> rectSelector)
        {
            return LookupCore(controller, condition, rectSelector, DefaultVector.Zero);
        }
        public static IEnumerable<IElementBounds<TUI, TDesignObject>> Lookup<TUI, TDesignObject>(this IDesignSceneController<TUI, TDesignObject> controller,
             Func<TDesignObject, IRect> rectSelector)
        {
            return LookupCore(controller, (Func<IDesignPair<TUI, TDesignObject>, bool>)null, rectSelector, DefaultVector.Zero);
        }
        public static IEnumerable<IDesignPair<TUI, TDesignObject>> Get<TUI, TDesignObject>(this IDesignSceneController<TUI, TDesignObject> controller,
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
        private static IEnumerable<IElementBounds<TUI, TDesignObject>> LookupCore<TUI, TDesignObject>(this IDesignSceneController<TUI, TDesignObject> controller,
           IReadOnlyHashSet<TUI> elements,
           Func<TDesignObject, IRect> rectSelector,
           IVector offset)
        {
            return LookupCore(controller, x => elements.Contains(x.UI), rectSelector, offset);
        }
        private static IEnumerable<IElementBounds<TUI, TDesignObject>> LookupCore<TUI, TDesignObject>(this IDesignSceneController<TUI, TDesignObject> controller,
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
            foreach (var item in controller.Nexts.Values.Reverse())
            {
                foreach (var val in LookupCore(item, condition, rectSelector, v))
                {
                    yield return val;
                }
            }
            IEnumerable<IDesignPair<TUI, TDesignObject>> query = controller.DesignUnits.Reverse();
            if (condition != null)
            {
                query = query.Where(condition);
            }
            foreach (var val in query)
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
