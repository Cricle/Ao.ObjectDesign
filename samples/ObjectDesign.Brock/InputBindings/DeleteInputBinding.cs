using Ao.ObjectDesign;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Wpf.Desiging;
using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Input;
using ObjectDesign.Brock.Components;
using ObjectDesign.Brock.Level;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace ObjectDesign.Brock.InputBindings
{
    internal class DeleteInputBinding : PreviewKeyboardInputBase
    {
        public DeleteInputBinding(IDesignSession<Scene, UIElementSetting> session)
        {
            Session = session;
        }

        public IDesignSession<Scene, UIElementSetting> Session { get; }

        public override void OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                UIElement[] obj = Session.Suface.DesigningObjects;
                if (obj != null)
                {
                    var set = new ReadOnlyHashSet<UIElement>(obj);
                    Session.SceneManager.CurrentSceneController.Remove(set);
                    Session.Suface.ClearDesignObjects();
                    Session.Suface.UpdateInRender();
                }
            }
        }
    }
}
