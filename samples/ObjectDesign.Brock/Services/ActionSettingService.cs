using Ao.ObjectDesign;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Designing;
using Ao.ObjectDesign.Wpf.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows;
using Ao.ObjectDesign.Abstract.Store;
using ObjectDesign.Brock.Components;
using ObjectDesign.Brock.Level;
using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.Session;
using Ao.ObjectDesign.Session.BuildIn;
using ObjectDesign.Brock.Controls;

namespace ObjectDesign.Brock.Services
{
    internal class ActionSettingService
    {
        private readonly MySceneMakerRuntime runtime;
        private readonly SceneEngine<Scene,UIElementSetting> engine;
        private readonly SceneMakerStartup<Scene, UIElementSetting> startup;
        private readonly DesignPanel designPanel;

        public ActionSettingService(MySceneMakerRuntime runtime,
            SceneEngine<Scene, UIElementSetting> engine,
            SceneMakerStartup<Scene, UIElementSetting> startup,
            DesignPanel designPanel)
        {
            this.designPanel = designPanel;
            this.startup = startup;
            this.runtime = runtime;
            this.engine = engine;
        }

        public void PutInContainer()
        {
            PutInContainer(runtime.CurrentSession.Suface.DesigningObjects);
        }
        public void PutInContainer(IReadOnlyList<UIElement> uis)
        {
            ActionInScene.ActionInDesigins(runtime, (x, y) =>
             {
                 var index = x.Scene.DesigningObjects.IndexOf(y.DesigningObject);
                 var cv = new CanvasSetting
                 {
                     DesigningObjects = { y.DesigningObject }
                 };
                 cv.SetDefault();
                 x.Scene.DesigningObjects[index] = cv;
                 var pair = x.DesignObjectUnitMap[cv];
                 runtime.SwithDesigningContexts(pair);
                 runtime.CurrentSession.Only(pair);
             }, uis);
        }
        public void Delete()
        {
            Delete(runtime.CurrentSession.Suface.DesigningObjects);
        }
        public void Delete(IReadOnlyList<UIElement> uis)
        {
            ActionInScene.ActionInDesigins(runtime, (x, y) =>
            {
                x.Scene.DesigningObjects.Remove(y.DesigningObject);
            }, uis);
        }
        public void CopyToClipboard()
        {
            var hash = new ReadOnlyHashSet<UIElement>(runtime.CurrentSession.Suface.DesigningObjects);
            CopyToClipboard(hash);
        }
        public void CopyToClipboard(IReadOnlyHashSet<UIElement> uis)
        {
            var pairs = runtime.CurrentSession.SceneManager.CurrentSceneController.Find(uis)
                .Select(x => x.DesigningObject)
                .ToList();

            engine.ClipboardManager.SetCopiedObject(pairs, true);
        }
        public void Copy(UIElementSetting setting, int count)
        {
            var c = runtime.CurrentSession.SceneManager.CurrentSceneController.FindController(setting);
            if (c != null)
            {
                for (int i = 0; i < count; i++)
                {
                    if (setting is IDesignScene<UIElementSetting> ds)
                    {
                        var x = (UIElementSetting)ds.Clone();
                        c.Scene.DesigningObjects.Add(x);
                    }
                    else
                    {
                        var val = (UIElementSetting)ReflectionHelper.Clone(setting.GetType(), setting, DesigningHelpers.KnowDesigningTypes);
                        c.Scene.DesigningObjects.Add(val);
                    }
                }
            }
        }
        public void MulityCopy(int count)
        {
            MulityCopy(runtime.CurrentSession.Suface.DesigningObjects, count);
        }
        public void MulityCopy(IReadOnlyList<UIElement> uis, int count)
        {
            ActionInScene.ActionInDesigins(runtime, (x, y) =>
            {
                Copy(y.DesigningObject, count);
            }, uis);
        }
        public UIElementSetting Create(Type type)
        {
            if (!typeof(UIElementSetting).IsAssignableFrom(type))
            {
                throw new ArgumentException($"Type {type} does not base on {typeof(UIElementSetting)}");
            }
            UIElementSetting inst;
            var fc = engine.UIDesignMap.GetInstanceFactory(type);
            if (fc == null)
            {
                inst = ReflectionHelper.Create(type) as UIElementSetting;
                inst.SetDefault();
            }
            else
            {
                inst = fc.Create() as FrameworkElementSetting;
            }
            return inst;
        }
        public void Insert(Type type, Point pos, bool addSequence)
        {
            Insert(runtime.CurrentSession.Suface.DesigningObjects, type, pos, addSequence);
        }
        public void Insert(IReadOnlyList<UIElement> elements, Type type, Point pos, bool addSequence)
        {
            var session = runtime.CurrentSession;

            var inst = Create(type) as FrameworkElementSetting;

            IDesignSceneController<UIElement, UIElementSetting> controller = null;
            double px = 0;
            double py = 0;
            if (elements != null && elements.Count == 1)
            {
                var val = elements[0];
                var lookController = new List<IDesignSceneController<UIElement, UIElementSetting>>(1)
                {
                    session.SceneManager.CurrentSceneController
                };
                var eles = new ReadOnlyHashSet<UIElement>(elements);
                var controllers = session.SceneManager.CurrentSceneController.FindControllers(eles);
                var targetController = controllers.LastOrDefault();
                if (targetController != null)
                {
                    targetController.DesignUnitNextMap.TryGetValue(val, out var inner);
                    inner = inner ?? targetController;
                    inner.Scene.DesigningObjects.Add(inst);
                    if (addSequence)
                    {
                        AddCollectionAddSequence(inner.Scene, inst);
                    }
                    controller = inner;
                    var scenes = controllers.Select(x => x.Scene)
                        .OfType<FrameworkElementSetting>()
                        .Where(x => x.PositionSize != null);
                    px = scenes.Sum(x => x.PositionSize.X);
                    py = scenes.Sum(x => x.PositionSize.Y);
                    if (inner != targetController && inner.Scene is FrameworkElementSetting innerDo && innerDo.PositionSize != null)
                    {
                        px += innerDo.PositionSize.X;
                        py += innerDo.PositionSize.Y;
                    }
                }
                else
                {
                    session.SceneManager.CurrentScene.DesigningObjects.Add(inst);
                    if (addSequence)
                    {
                        AddCollectionAddSequence(session.SceneManager.CurrentScene, inst);
                    }
                }
            }
            else
            {
                session.SceneManager.CurrentScene.DesigningObjects.Add(inst);
                if (addSequence)
                {
                    AddCollectionAddSequence(session.SceneManager.CurrentScene, inst);
                }
            }
            if (controller is null)
            {
                controller = session.SceneManager.CurrentSceneController;
            }
            if (controller.Scene is FrameworkElementSetting d)
            {
                inst.PositionSize.X = pos.X - px;
                inst.PositionSize.Y = pos.Y - py;
            }
            else
            {
                inst.PositionSize.X = pos.X;
                inst.PositionSize.Y = pos.Y;
            }
            session.Only(inst);
            if (controller.DesignObjectUnitMap.TryGetValue(inst, out var unit))
            {
                runtime.SwithDesigningContexts(unit.DesigningObject);
            }
            session.Suface.UpdateInRender();
        }
        private void AddCollectionAddSequence(IDesignScene<UIElementSetting> scene, UIElementSetting setting)
        {
            //if (runtime.HasSession)
            //{
            //    var seq = new CollectionAddFallback<UIElementSetting>(scene.DesigningObjects, setting);
            //    runtime.CurrentSession.Sequencer.Undos.Push(seq, true);
            //}
        }

        public void Select(UIElementSetting setting)
        {
            if (setting != null)
            {
                var controller = runtime.CurrentSession.SceneManager.CurrentSceneController.FindController(setting);
                if (controller != null)
                {
                    var pair = controller.DesignObjectUnitMap[setting];
                    runtime.CurrentSession.Only(pair);
                    runtime.SwithDesigningContexts(pair);
                    runtime.CurrentSession.Suface.UpdateInRender();
                }
            }
            else
            {
                runtime.CurrentSession.Suface.ClearDesignObjects();
                runtime.CurrentSession.Suface.UpdateInRender();
            }
        }
        public void Save(Stream stream)
        {
            using (var gs = new GZipStream(stream, CompressionLevel.Fastest))
            using (var sw = new StreamWriter(gs))
            {
                var str = JsonDesignInterop.Default.SerializeToString(runtime.CurrentSession.Scene);
                sw.Write(str);
            }
        }
        public void Clean()
        {
            if (runtime.CurrentSession != null)
            {
                var id = runtime.CurrentSession.Id;
                engine.ClipboardManager.SetCopiedObject(null, true);
                runtime.CurrentSession = null;
                startup.RemoveSession(id);
            }
        }

        public void Load(Stream stream)
        {
            using (var s = new GZipStream(stream, CompressionMode.Decompress))
            {
                var str = new StreamReader(s).ReadToEnd();
                var sc = JsonDesignInterop.Default.DeserializeByString<Scene>(str);
                var session = startup.NewSession(sc);
                SetSession(session);
                session.SceneManager.SetLazyBinding(false);
                session.LazyBinding = false;
            }
        }
        public void SetSession(IDesignSession<Scene,UIElementSetting> session)
        {
            runtime.CurrentSession = session;
            runtime.ForViewDataTemplateSelector = session.ObjectDesigner.CreateTemplateSelector();
            designPanel.DesignSession = session;
        }
    }
}
