namespace Ao.ObjectDesign.Designing.Level
{
    public class DesignPair<TUI, TDesignObject> : IDesignPair<TUI, TDesignObject>
    {
        public DesignPair(TUI uI, TDesignObject designingObject)
        {
            UI = uI;
            DesigningObject = designingObject;
        }

        public TUI UI { get; }

        public TDesignObject DesigningObject { get; }
    }
}
