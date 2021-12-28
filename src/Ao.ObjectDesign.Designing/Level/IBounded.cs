using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing.Level
{
    public interface IPositionBounded
    {
        IVector GetPosition();

        IRect GetBounds();
    }
}
