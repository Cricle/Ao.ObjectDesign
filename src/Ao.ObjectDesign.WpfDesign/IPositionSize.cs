using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public interface IPositionSize
    {
        Vector Position { get; }

        Size Size { get; }
    }
}
