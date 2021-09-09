namespace Ao.ObjectDesign.Designing.Level
{
    public class ElementBounds<TUI, TDesignObject> : IElementBounds<TUI, TDesignObject>
    {
        public IDesignSceneController<TUI, TDesignObject> Controller { get; set; }

        public IRect Bounds { get; set; }

        public IRect ActualBounds { get; set; }

        public IDesignPair<TUI, TDesignObject> Pair { get; set; }
    }
}
