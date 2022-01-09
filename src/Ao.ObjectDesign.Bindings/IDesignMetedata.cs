using Ao.ObjectDesign.Designing.Level;

namespace Ao.ObjectDesign.Bindings
{
    public interface IDesignMetedata<TUI, TContext> : IDesignRefControl<TUI>
    {
        IDesignContext<TUI, TContext> DesignContext { get; }

        IRect ContainerBounds { get; }
        IVector ContainerOffset { get; }

        IVector IgnoreCanvasTargetOffset { get; }
        IVector InCanvasPosition { get; }

        bool IsContainerCanvas { get; }

        IRect ParentBounds { get; }
        IVector ParentOffset { get; }

        IRect TargetBounds { get; }
        IVector TargetOffset { get; }
    }
}
