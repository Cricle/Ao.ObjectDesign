using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing.Level
{
    public interface IPositionBounded
    {
        IVector Position { get; }

        IRect GetBounds();
    }
}
