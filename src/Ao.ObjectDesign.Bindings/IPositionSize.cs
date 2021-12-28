using Ao.ObjectDesign.Designing.Level;
using System.Windows;

namespace Ao.ObjectDesign.Bindings
{
    public interface IPositionSize
    {
        IVector Position { get; }

        IVector Size { get; }
    }
}
