using Ao.ObjectDesign.Designing.Level;

namespace Ao.ObjectDesign.Bindings
{
    public interface IDesignBoundsContext
    {
        IVector TargetOffset { get; }
        IRect TargetBounds { get; }

        IVector ParentOffset { get; }
        IRect ParentBounds { get; }

        IVector ContainerOffset { get; }
        IRect ContainerBounds { get; }
    }
}
