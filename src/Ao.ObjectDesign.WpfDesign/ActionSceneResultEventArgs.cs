using System;

namespace Ao.ObjectDesign.WpfDesign
{
    public class ActionSceneResultEventArgs : EventArgs
    {
        public ActionSceneResultEventArgs(string name, SceneActions action)
        {
            Name = name;
            Action = action;
        }

        public string Name { get; }

        public SceneActions Action { get; }
    }
}
