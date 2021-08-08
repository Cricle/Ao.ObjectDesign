using Ao.ObjectDesign.Designing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.WpfDesign.Level
{
    public interface IDeisgnScene<TDesignObject>
    {
        SilentObservableCollection<TDesignObject> DesigningObjects { get; }
    }
}
