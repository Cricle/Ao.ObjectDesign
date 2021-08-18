using System.Collections.ObjectModel;

namespace Ao.ObjectDesign.Designing.Level
{
    public interface IObservableDeisgnScene<TDesignObject> : IDeisgnScene<TDesignObject>
    {
        new SilentObservableCollection<TDesignObject> DesigningObjects { get; }
    }
}
