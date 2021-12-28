using Ao.ObjectDesign.Designing.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Bindings
{
    public interface IDesignRefControl<TUI>
    {
        TUI Container { get; }
        TUI Parent { get; }
        TUI Target { get; }
    }
}
