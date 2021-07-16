using Ao.ObjectDesign.Wpf.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(Polyline))]
    public class PolylineSetting : ShapeSetting, IMiddlewareDesigner<Polyline>
    {

        private PointCollectionDesigner points;
        private FillRule fillRule;

        public virtual FillRule FillRule
        {
            get => fillRule;
            set => Set(ref fillRule, value);
        }

        public virtual PointCollectionDesigner Points
        {
            get => points;
            set => Set(ref points, value);
        }


        public override void SetDefault()
        {
            base.SetDefault();
            Points = new PointCollectionDesigner();
            FillRule = FillRule.EvenOdd;
        }

        public void Apply(Polyline value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((Shape)value);
                if (value.Points is null)
                {
                    Points = new PointCollectionDesigner();
                }
                else
                {
                    Points = new PointCollectionDesigner { PointCollection = value.Points };
                }
                FillRule = value.FillRule;
            }
        }

        public void WriteTo(Polyline value)
        {
            if (value != null)
            {
                WriteTo((Shape)value);
                value.Points = points?.PointCollection;
                value.FillRule = fillRule;
            }
        }
    }
}
