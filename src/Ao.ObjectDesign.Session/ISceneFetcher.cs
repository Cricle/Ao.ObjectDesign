using System.IO.Abstractions;

namespace Ao.ObjectDesign.Session
{
    public interface ISceneFetcher<TScene>
    {
        TScene Get(IFileInfo file);
    }
}
