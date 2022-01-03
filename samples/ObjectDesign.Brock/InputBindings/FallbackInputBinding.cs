using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.WpfDesign;
using Ao.ObjectDesign.WpfDesign.Input;
using ObjectDesign.Brock.Components;
using ObjectDesign.Brock.Level;
using System.Windows.Input;

namespace ObjectDesign.Brock.InputBindings
{
    internal class FallbackInputBinding : PreviewKeyboardInputBase
    {
        public FallbackInputBinding(IDesignSession<Scene, UIElementSetting> session)
        {
            Session = session;
        }

        public IDesignSession<Scene, UIElementSetting> Session { get; }

        public override void OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers& ModifierKeys.Control)== ModifierKeys.Control)
            {
                if (e.Key== Key.Z)
                {
                    Session.Sequencer.Undo(true);
                    Session.Suface.UpdateInRender();
                }
                else if (e.Key== Key.U)
                {
                    Session.Sequencer.Redo(true);
                    Session.Suface.UpdateInRender();
                }
                else if (e.Key== Key.W)
                {
                    Session.Sequencer.CleanAllRecords();
                }
            }
        }

    }
}
