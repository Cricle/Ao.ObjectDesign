using Ao.ObjectDesign;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session;
using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.Wpf.Designing;
using Ao.ObjectDesign.WpfDesign;
using Ao.ObjectDesign.WpfDesign.Input;
using ObjectDesign.Brock.Components;
using ObjectDesign.Brock.Level;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ObjectDesign.Brock.InputBindings
{
    internal class CopyPasteInputBinding: PreviewKeyboardInputBase
    {
        public CopyPasteInputBinding(IDesignSession<Scene,UIElementSetting> session,
            SceneEngine<Scene, UIElementSetting> engine)
        {
            Session = session;
            Engine = engine;
        }

        private bool cutMode;

        public IDesignSession<Scene, UIElementSetting> Session { get; }

        public SceneEngine<Scene, UIElementSetting> Engine { get; }

        public override void OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.C:
                        HandleCopy(false);
                        break;
                    case Key.X:
                        HandleCopy(true);
                        break;
                    case Key.V:
                        HandlePressed();
                        break;
                    default:
                        break;
                }
            }
        }
        private UIElementSetting CopyDesiging(UIElementSetting x)
        {
            if (x is IDesignScene<UIElementSetting> scene && scene is UIElementSetting)
            {
                return (UIElementSetting)scene.Clone();
            }
            return (UIElementSetting)ReflectionHelper.Clone(x.GetType(), x, DesigningHelpers.KnowDesigningTypes);
        }
        private void HandlePressed()
        {
            IReadOnlyList<UIElementSetting> objs = Engine.ClipboardManager.GetFromCopied(CopyDesiging, true, false); ;
            IDesignSceneController<UIElement, UIElementSetting> controller = Session.SceneManager.CurrentSceneController;
            if (objs != null)
            {
                if (!cutMode)
                {
                    foreach (var item in objs)
                    {
                        if (item is FrameworkElementSetting fes)
                        {
                            fes.PositionSize.X += 10;
                            fes.PositionSize.Y += 10;
                        }
                    }
                }
                SilentObservableCollection<UIElementSetting> designObjects = Session.SceneManager.CurrentScene.DesigningObjects;
                foreach (var item in objs)
                {
                    designObjects.Add(item);
                }
                if (cutMode)
                {
                    HashSet<UIElementSetting> set = new HashSet<UIElementSetting>(Engine.ClipboardManager.OriginObjects);
                    UIElement[] ui = Engine.ClipboardManager.OriginObjects
                        .Select(x => Session.SceneManager.CurrentSceneController.DesignObjectUnitMap[x].UI)
                        .ToArray();
                    designObjects.RemoveRange(Engine.ClipboardManager.OriginObjects);
                    if (ui.Length != 0)
                    {
                        Session.Suface.DesigningObjects = ui;
                    }
                }
                else
                {
                    Engine.ClipboardManager.SetCopiedObject(objs, false);
                    var map = Session.SceneManager.CurrentSceneController.DesignObjectUnitMap;
                    Session.Suface.DesigningObjects = objs.Select(x => map[x].UI).ToArray();
                }
            }
            Session.Suface.UpdateInRender();
        }

        public void HandleCopy(bool isCutMode)
        {
            var set = new ReadOnlyHashSet<UIElement>(Session.Suface.DesigningObjects);
            var vals = Session.SceneManager.CurrentSceneController.Find(set)
                .Select(x => x.DesigningObject)
                .ToList();
            Engine.ClipboardManager.SetCopiedObject(vals, false);
            cutMode = isCutMode;
        }
    }
}
