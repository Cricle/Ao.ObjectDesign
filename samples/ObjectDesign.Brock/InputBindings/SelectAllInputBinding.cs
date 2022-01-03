using Ao.ObjectDesign.Session.Controllers;
using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.WpfDesign;
using Ao.ObjectDesign.WpfDesign.Input;
using ObjectDesign.Brock.Components;
using ObjectDesign.Brock.Level;
using System.Linq;
using System.Windows.Input;

namespace ObjectDesign.Brock.InputBindings
{
    internal class SelectAllInputBinding:PreviewKeyboardInputBase
    {
        public SelectAllInputBinding(IDesignSession<Scene, UIElementSetting> session)
        {
            Session = session;
        }

        public IDesignSession<Scene, UIElementSetting> Session { get; }

        public override void OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers& ModifierKeys.Control)== ModifierKeys.Control)
            {
                if (e.Key== Key.A)
                {
                    if (Session.SceneManager.CurrentSceneController is WpfSceneController<UIElementSetting> controller)
                    {
                        foreach (var item in controller.BindingTaskMap.Values)
                        {
                            item.ExecuteBinding();
                        }
                        controller.BindingTaskMap.Clear();
                    }
                    Session.Suface.DesigningObjects =
                        Session.SceneManager.CurrentSceneController.DesignUnitMap.Keys.ToArray();
                    Session.Suface.UpdateInRender();
                }
            }
        }
    }
}
