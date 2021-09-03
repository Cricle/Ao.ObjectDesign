using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public interface IDesignRefControl
    {
        UIElement Container { get; }
        UIElement Parent { get; }
        UIElement Target { get; }
    }
    public interface IDesignPositionFetcher
    {
        Vector GetPosition(UIElement element);

        Size GetSize(UIElement element);

        Vector GetInCanvaPosition(UIElement element);
    }
    public interface IDesignMetedata : IDesignRefControl
    {
        DesignContext DesignContext { get; }

        Rect ContainerBounds { get; }
        Vector ContainerOffset { get; }

        Vector IgnoreCanvasTargetOffset { get; }
        Vector InCanvasPosition { get; }

        bool IsContainerCanvas { get; }

        Rect ParentBounds { get; }
        Vector ParentOffset { get; }

        Rect TargetBounds { get; }
        Vector TargetOffset { get; }
    }
}