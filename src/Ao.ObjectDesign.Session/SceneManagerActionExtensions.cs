using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.WpfDesign;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign.Session
{
    public static class SceneManagerActionExtensions
    {
        public static void SetLazyBinding<TUI, TScene, TDesignObject>(this SceneManager<TUI, TScene, TDesignObject> mgr,
            bool lazyBinding)
            where TScene : IDesignScene<TDesignObject>
        {
            SetControllers(mgr, x =>
            {
                if (x is WpfLazyMapSceneController<TDesignObject> controller)
                {
                    controller.LazyBinding = lazyBinding;
                }
            });
        }
        public static void SetControllers<TUI, TScene, TDesignObject>(this SceneManager<TUI, TScene, TDesignObject> mgr,
            Action<IDesignSceneController<TUI, TDesignObject>> action)
            where TScene : IDesignScene<TDesignObject>
        {
            if (mgr is null)
            {
                throw new ArgumentNullException(nameof(mgr));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            if (mgr.CurrentSceneController is null)
            {
                throw new InvalidOperationException($"Can't set when CurrentSceneController is null!");
            }
            IEnumerable<IDesignSceneController<TUI, TDesignObject>> c = new IDesignSceneController<TUI, TDesignObject>[] { mgr.CurrentSceneController };
            while (c.Any())
            {
                foreach (var item in c)
                {
                    action(item);
                }
                c = c.SelectMany(x => x.DesignUnitNextMap.Values);
            }
        }
    }
}
