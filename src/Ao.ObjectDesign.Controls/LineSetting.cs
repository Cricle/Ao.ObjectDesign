using Ao.ObjectDesign.Wpf.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System.ComponentModel;
using System.Windows.Shapes;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(Line))]
    public class LineSetting : ShapeSetting, IMiddlewareDesigner<Line>
    {
        private double x1;
        private double y1;
        private double x2;
        private double y2;

        [DefaultValue(0d)]
        public virtual double X1
        {
            get => x1;
            set => Set(ref x1, value);
        }
        [DefaultValue(0d)]
        public virtual double X2
        {
            get => x2;
            set => Set(ref x2, value);
        }
        [DefaultValue(0d)]
        public virtual double Y1
        {
            get => y1;
            set => Set(ref y1, value);
        }
        [DefaultValue(0d)]
        public virtual double Y2
        {
            get => y2;
            set => Set(ref y2, value);
        }

        public override void SetDefault()
        {
            base.SetDefault();
            X1 = X2 = Y1 = Y2 = 0;
        }

        public void Apply(Line value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((Shape)value);
                X1 = value.X1;
                X2 = value.X2;
                Y1 = value.Y1;
                Y2 = value.Y2;
            }
        }

        public void WriteTo(Line value)
        {
            if (value != null)
            {
                WriteTo((Shape)value);
                value.X1 = X1;
                value.X2 = X2;
                value.Y1 = Y1;
                value.Y2 = Y2;

            }
        }
    }
}
