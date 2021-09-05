namespace Ao.ObjectDesign.Designing.Level
{
    public interface IElementBounds<TUI, TDesignObject>
    {
        DesignSceneController<TUI, TDesignObject> Controller { get; }

        IRect Bounds { get; }

        IRect ActualBounds { get; }

        IDesignPair<TUI, TDesignObject> Pair { get; }
    }
}
