using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing.Level
{
    public interface IDesignScene<TDesignObject>
    {
        IList<TDesignObject> DesigningObjects { get; }
    }
}
