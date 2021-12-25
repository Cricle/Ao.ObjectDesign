using System.IO.Abstractions;

namespace Ao.ObjectDesign.Session.Desiging
{
    public interface IDesignSessionActions
    {
        IFileInfo TargetFile { get; }
    }
}
