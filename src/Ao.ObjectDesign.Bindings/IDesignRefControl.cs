namespace Ao.ObjectDesign.Bindings
{
    public interface IDesignRefControl<TUI>
    {
        TUI Container { get; }
        TUI Parent { get; }
        TUI Target { get; }
    }
}
