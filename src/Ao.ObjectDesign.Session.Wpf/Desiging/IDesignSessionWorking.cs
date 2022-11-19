using System.IO.Abstractions;

namespace Ao.ObjectDesign.Session.Wpf.Desiging
{
    public interface IDesignSessionWorking
    {
        IDirectoryInfo WorkSpace { get; }
    }
}
