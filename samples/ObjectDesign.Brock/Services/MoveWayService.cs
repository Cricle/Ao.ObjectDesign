using System.Collections.Generic;
using System.Windows;

namespace ObjectDesign.Brock.Services
{
    internal class MoveWayService
    {
        private readonly MySceneMakerRuntime runtime;

        public MoveWayService(MySceneMakerRuntime runtime)
        {
            this.runtime = runtime;
        }
        public void MoveTop()
        {
            MoveTop(runtime.CurrentSession.Suface.DesigningObjects);
        }
        public void MoveTop(IReadOnlyList<UIElement> objs)
        {
            ActionInScene.ActionInDesigins(runtime, (x, y) =>
            {
                var index = x.Scene.DesigningObjects.IndexOf(y.DesigningObject);
                if (index != 0)
                {
                    x.Scene.DesigningObjects.Move(index, 0);
                }
            }, objs, true);
        }
        public void MoveUp()
        {
            MoveUp(runtime.CurrentSession.Suface.DesigningObjects);
        }
        public void MoveUp(IReadOnlyList<UIElement> objs)
        {
            ActionInScene.ActionInDesigins(runtime, (x, y) =>
            {
                var index = x.Scene.DesigningObjects.IndexOf(y.DesigningObject);
                if (index != 0)
                {
                    x.Scene.DesigningObjects.Move(index, index - 1);
                }
            }, objs, true);
        }
        public void MoveDown()
        {
            MoveDown(runtime.CurrentSession.Suface.DesigningObjects);
        }
        public void MoveDown(IReadOnlyList<UIElement> objs)
        {
            ActionInScene.ActionInDesigins(runtime, (x, y) =>
            {
                var index = x.Scene.DesigningObjects.IndexOf(y.DesigningObject);
                var count = x.Scene.DesigningObjects.Count;
                if (index != count - 1)
                {
                    x.Scene.DesigningObjects.Move(index, index + 1);
                }
            }, objs, true);
        }
        public void MoveBottom()
        {
            MoveBottom(runtime.CurrentSession.Suface.DesigningObjects);
        }
        public void MoveBottom(IReadOnlyList<UIElement> objs)
        {
            ActionInScene.ActionInDesigins(runtime, (x, y) =>
             {
                 var index = x.Scene.DesigningObjects.IndexOf(y.DesigningObject);
                 var count = x.Scene.DesigningObjects.Count;
                 if (index != count - 1)
                 {
                     x.Scene.DesigningObjects.Move(index, count - 1);
                 }
             }, objs, true);
        }

    }
}
