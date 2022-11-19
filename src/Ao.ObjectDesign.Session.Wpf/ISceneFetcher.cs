using System.IO.Abstractions;

namespace Ao.ObjectDesign.Session.Wpf
{
    public interface ISceneFetcher<TScene>
    {
        TScene Get(IFileInfo file);
    }
}
