using Ao.ObjectDesign.Designing.Annotations;

using ObjectDesign.Brock.Controls.BindingCreators;
using Ao.ObjectDesign.Bindings;
using System.Windows.Shapes;

namespace ObjectDesign.Brock.Controls
{
    
        
    
    public class LineSetting : ShapeSetting
    {
        private double x1;
        private double y1;
        private double x2;
        private double y2;

        public double X1
        {
            get => x1;
            set => Set(ref x1, value);
        }
        public double Y1
        {
            get => y1;
            set => Set(ref y1, value);
        }
        public double X2
        {
            get => x2;
            set => Set(ref x2, value);
        }
        public double Y2
        {
            get => y2;
            set => Set(ref y2, value);
        }
    }
}
