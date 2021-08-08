using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.WpfDesign
{
    public interface IInstanceFactory
    {
        Type TargetType { get; }

        object Create();
    }
}
