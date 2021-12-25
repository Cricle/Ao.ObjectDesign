using System.IO.Abstractions;

namespace Ao.ObjectDesign.Session.Desiging
{
    public interface IDesignSessionWorking
    {
        IDirectoryInfo WorkSpace { get; }
    }
}
