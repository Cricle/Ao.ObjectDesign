using Ao.ObjectDesign.Designing.Level;

namespace Ao.ObjectDesign.Bindings
{
    public interface IDesignPositionFetcher<TUI>
    {
        IVector GetPosition(TUI element);

        IVector GetSize(TUI element);

        IVector GetInCanvaPosition(TUI element);
    }
}
