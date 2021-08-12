using System.Collections.ObjectModel;

namespace Ao.ObjectDesign.WpfDesign.Level
{
    public interface IDeisgnScene<TDesignObject>
    {
        SilentObservableCollection<TDesignObject> DesigningObjects { get; }
    }
}
