using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ao.ObjectDesign.Designing.Level
{
    public interface IDeisgnScene<TDesignObject>
    {
        IList<TDesignObject> DesigningObjects { get; }
    }
}
