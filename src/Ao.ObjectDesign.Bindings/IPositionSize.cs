using Ao.ObjectDesign.Designing.Level;

namespace Ao.ObjectDesign.Bindings
{
    public interface IPositionSize
    {
        IVector Position { get; }

        IVector Size { get; }
    }
}
