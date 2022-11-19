using Ao.ObjectDesign;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf;
using ObjectDesign.Brock.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace ObjectDesign.Brock.Services
{
    internal static class ActionInScene
    {
        internal static void ActionInDesigins(MySceneMakerRuntime runtime,
            Action<IDesignSceneController<UIElement, UIElementSetting>, IDesignPair<UIElement, UIElementSetting>> action,
            bool reverse = false)
        {
            ActionInDesigins(runtime, action, runtime.CurrentSession.Suface.DesigningObjects, reverse);
        }
        internal static void ActionInDesigins(MySceneMakerRuntime runtime,
            Action<IDesignSceneController<UIElement, UIElementSetting>, IDesignPair<UIElement, UIElementSetting>> action,
            IReadOnlyList<UIElement> objs,
            bool reverse = false)
        {
            Debug.Assert(action != null);
            if (objs != null && objs.Count != 0)
            {
                var len = objs.Count;
                var uis = new ReadOnlyHashSet<UIElement>(objs);
                var ds = runtime.CurrentSession.SceneManager.CurrentSceneController.FindControllers(uis);
                if (reverse)
                {
                    ds = ds.Reverse();
                }
                foreach (var item in ds)
                {
                    for (int i = 0; i < len; i++)
                    {
                        if (item.DesignUnitMap.TryGetValue(objs[i], out var pair))
                        {
                            action(item, pair);
                        }
                    }
                }

                var arr = new List<UIElement>();
                foreach (var item in runtime.CurrentSession.SceneManager.CurrentSceneController.Find(uis))
                {
                    if (uis.Contains(item.UI))
                    {
                        arr.Add(item.UI);
                    }
                }
                runtime.CurrentSession.Suface.DesigningObjects = arr.ToArray();
                runtime.CurrentSession.Suface.UpdateInRender();
            }
        }

    }
}
