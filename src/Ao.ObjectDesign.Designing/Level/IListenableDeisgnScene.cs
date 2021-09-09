using System.Collections.ObjectModel;

namespace Ao.ObjectDesign.Designing.Level
{
    public interface IObservableDesignScene<TDesignObject> : IDesignScene<TDesignObject>
    {
        new SilentObservableCollection<TDesignObject> DesigningObjects { get; }
    }
}
