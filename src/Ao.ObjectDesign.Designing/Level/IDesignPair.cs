namespace Ao.ObjectDesign.Designing.Level
{
    public interface IDesignPair<out TUI,out TDesignObject>
    {
        TUI UI { get; }

        TDesignObject DesigningObject { get; }
    }
}
