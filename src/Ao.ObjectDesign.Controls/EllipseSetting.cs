using Ao.ObjectDesign.Wpf.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(Ellipse))]
    public class EllipseSetting : ShapeSetting, IMiddlewareDesigner<Ellipse>
    {
        public void Apply(Ellipse value)
        {
            Apply((Shape)value);
        }

        public void WriteTo(Ellipse value)
        {
            WriteTo((Shape)value);
        }
    }
}
