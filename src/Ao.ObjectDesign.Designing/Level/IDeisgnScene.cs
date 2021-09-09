using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ao.ObjectDesign.Designing.Level
{
    public interface IDesignScene<TDesignObject>
    {
        IList<TDesignObject> DesigningObjects { get; }
    }
}
