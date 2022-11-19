using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.WpfDesign;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace Ao.ObjectDesign.Session.Wpf.Desiging
{
    public static class DesignSufaceActionsExtensions
    {
        public static void SetDesignObjectsByElements<TScene, TSetting>(this DesignSuface suface,
            IDesignSceneController<UIElement, TSetting> controller,
            IReadOnlyList<UIElement> elements,
            Func<IDesignPair<UIElement, TSetting>, bool> keepFunc)
            where TScene : IDesignScene<TSetting>
        {
            var set = new ReadOnlyHashSet<UIElement>(elements);
            var pairs = controller.Find(set);
            SetDesignObjects<TScene, TSetting>(suface,
                controller,
                pairs,
                keepFunc);
        }
        public static void Only<TScene, TSetting>(this DesignSuface suface,
            IDesignSceneController<UIElement, TSetting> controller,
            UIElement element)
            where TScene : IDesignScene<TSetting>
        {
            var pair = controller.Find((x, y) => y.UI == element).FirstOrDefault();
            if (pair != null)
            {
                Only<TScene, TSetting>(suface, controller, pair);
            }
        }
        public static void Only<TScene, TSetting>(this DesignSuface suface,
           IDesignSceneController<UIElement, TSetting> controller,
           TSetting setting)
            where TScene : IDesignScene<TSetting>
        {
            var pair = controller.Find((x, y) => EqualityComparer<TSetting>.Default.Equals(y.DesigningObject, setting))
                .FirstOrDefault();
            if (pair != null)
            {
                Only<TScene, TSetting>(suface, controller, pair);
            }
        }
        public static void Only<TScene, TSetting>(this DesignSuface suface,
          IDesignSceneController<UIElement, TSetting> controller,
          IDesignPair<UIElement, TSetting> pair)
            where TScene : IDesignScene<TSetting>
        {
            if (pair != null)
            {
                suface.DesigningObjects = new UIElement[] { pair.UI };
                ExecuteBind<TScene, TSetting>(controller, pair.DesigningObject);
            }
        }
        public static bool ToggleDesignObjectsByElements<TScene, TSetting>(this DesignSuface suface,
            IDesignSceneController<UIElement, TSetting> controller,
            UIElement element)
            where TScene : IDesignScene<TSetting>
        {
            UIElement[] origin = suface.DesigningObjects;
            if (origin is null || origin.Length == 0)
            {
                Only<TScene, TSetting>(suface, controller, element);
                return true;
            }
            int index = Array.FindIndex(origin, x => x == element);
            if (index != -1)
            {
                if (origin.Length == 1)
                {
                    suface.ClearDesignObjects();
                }
                else
                {
                    UIElement[] newArr = new UIElement[origin.Length - 1];
                    if (index != 0)
                    {
                        Array.Copy(origin, 0, newArr, 0, index);
                    }
                    if (origin.Length != index + 1)
                    {
                        Array.Copy(origin, index + 1, newArr, index, origin.Length - index - 1);
                    }
                    suface.DesigningObjects = newArr;
                }
                return true;
            }
            var unit = controller.Find((x, y) => y.UI == element).FirstOrDefault();
            if (unit is null)
            {
                return false;
            }
            Array.Resize(ref origin, origin.Length + 1);
            ExecuteBind<TScene, TSetting>(controller, unit.DesigningObject);
            origin[origin.Length - 1] = element;
            suface.DesigningObjects = origin;
            return true;
        }
        public static void ToggleDesignObjectsByElements<TScene, TSetting>(this DesignSuface suface,
            IDesignSceneController<UIElement, TSetting> controller,
            IReadOnlyList<UIElement> elements)
            where TScene : IDesignScene<TSetting>
        {
            var set = new ReadOnlyHashSet<UIElement>(elements);
            var pairs = controller.Find(set);
            ToggleDesignObjects<TScene, TSetting>(suface,
                controller,
                pairs);
        }
        public static void SetDesignObjectsBySettings<TScene, TSetting>(this DesignSuface suface,
           IDesignSceneController<UIElement, TSetting> controller,
           IReadOnlyList<TSetting> settings,
           Func<IDesignPair<UIElement, TSetting>, bool> keepFunc)
            where TScene : IDesignScene<TSetting>
        {
            var set = new ReadOnlyHashSet<TSetting>(settings);
            var pairs = controller.Find(set);
            SetDesignObjects<TScene, TSetting>(suface,
                controller,
                pairs,
                keepFunc);
        }
        public static void ToggleDesignObjectsBySettings<TScene, TSetting>(this DesignSuface suface,
            IDesignSceneController<UIElement, TSetting> controller,
            IReadOnlyList<TSetting> settings)
            where TScene : IDesignScene<TSetting>
        {
            if (suface is null)
            {
                throw new ArgumentNullException(nameof(suface));
            }

            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var set = new ReadOnlyHashSet<TSetting>(settings);
            var pairs = controller.Find(set);
            ToggleDesignObjects<TScene, TSetting>(suface,
                controller,
                pairs);
        }
        private static void ToggleDesignObjects<TScene, TSetting>(DesignSuface suface,
            IDesignSceneController<UIElement, TSetting> controller,
            IEnumerable<IDesignPair<UIElement, TSetting>> pairs)
            where TScene : IDesignScene<TSetting>
        {
            Debug.Assert(suface != null);
            Debug.Assert(controller != null);
            Debug.Assert(pairs != null);
            var set = new HashSet<TSetting>(pairs.Select(x => x.DesigningObject));
            SetDesignObjects<TScene, TSetting>(suface, controller, pairs, x => !set.Contains(x.DesigningObject));
        }
        internal static void SetDesignObjects<TScene, TSetting>(DesignSuface suface,
            IDesignSceneController<UIElement, TSetting> controller,
            IEnumerable<IDesignPair<UIElement, TSetting>> pairs,
            Func<IDesignPair<UIElement, TSetting>, bool> keepFunc)
            where TScene : IDesignScene<TSetting>
        {
            Debug.Assert(suface != null);
            Debug.Assert(controller != null);
            Debug.Assert(pairs != null);
            Debug.Assert(keepFunc != null);

            suface.DesigningObjects = pairs.Where(keepFunc)
                            .Select(x => x.UI)
                            .ToArray();
            ExecuteBind<TScene, TSetting>(controller, pairs.Select(x => x.DesigningObject));
        }
        internal static void ExecuteBind<TScene, TSetting>(IDesignSceneController<UIElement, TSetting> controller,
             IEnumerable<TSetting> pairs)
            where TScene : IDesignScene<TSetting>
        {
            Debug.Assert(controller != null);
            Debug.Assert(pairs != null);

            foreach (var item in pairs)
            {
                ExecuteBind<TScene, TSetting>(controller, item);
            }
        }
        internal static void ExecuteBind<TScene, TSetting>(IDesignSceneController<UIElement, TSetting> controller,
            TSetting item)
             where TScene : IDesignScene<TSetting>
        {
            Debug.Assert(controller != null);
            Debug.Assert(item != null);

            var c = controller.FindController(item);
            if (c is WpfLazyMapSceneController<TSetting> lmsc &&
                lmsc.BindingTaskMap.TryGetValue(item, out var box))
            {
                foreach (var scope in box.BindingScopes)
                {
                    scope.Bind(box.Pair.UI);
                }
                lmsc.BindingTaskMap.Remove(item);
            }

        }
    }
}
