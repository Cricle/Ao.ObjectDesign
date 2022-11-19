using System.IO.Abstractions;

namespace Ao.ObjectDesign.Session.Wpf.Desiging
{
    public interface IDesignSessionActions
    {
        IFileInfo TargetFile { get; }
    }
}
