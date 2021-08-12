namespace Ao.ObjectDesign.WpfDesign
{
    public class TwoSceneResultEventArgs : ActionSceneResultEventArgs
    {
        public TwoSceneResultEventArgs(string name, string resultName)
            : base(name, SceneActions.Copied)
        {
            ResultName = resultName;
        }

        public string ResultName { get; }
    }
}
