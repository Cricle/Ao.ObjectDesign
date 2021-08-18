namespace Ao.ObjectDesign.Designing.Level
{
    public interface IDesignPair<TUI, TDesignObject>
    {
        TUI UI { get; }

        TDesignObject DesigningObject { get; }
    }
}
