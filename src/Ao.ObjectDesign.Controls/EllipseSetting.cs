using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Designing;
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
