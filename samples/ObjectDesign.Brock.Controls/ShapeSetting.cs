using Ao.ObjectDesign.Wpf.Designing;
using ObjectDesign.Brock.Components;
using System.Windows.Media;

namespace ObjectDesign.Brock.Controls
{

    public abstract class ShapeSetting : FrameworkElementSetting
    {
        private BrushDesigner stroke;
        private double strokeThickness;
        private DoubleCollectionDesigner strokeDashArray;
        private BrushDesigner fill;

        public BrushDesigner Fill
        {
            get => fill;
            set
            {
                Set(ref fill, value);
            }
        }

        public DoubleCollectionDesigner StrokeDashArray
        {
            get => strokeDashArray;
            set
            {
                Set(ref strokeDashArray, value);
            }
        }

        public double StrokeThickness
        {
            get => strokeThickness;
            set
            {
                Set(ref strokeThickness, value);
            }
        }

        public BrushDesigner Stroke
        {
            get => stroke;
            set
            {
                Set(ref stroke, value);
            }
        }
        public override void SetDefault()
        {
            base.SetDefault();
            StrokeDashArray = new DoubleCollectionDesigner();
            StrokeThickness = 1;
            Stroke = new BrushDesigner
            {
                Type = PenBrushTypes.Solid,
                SolidColorBrushDesigner = new SolidColorBrushDesigner
                {
                    Color = new ColorDesigner()
                }
            };
            Stroke.SolidColorBrushDesigner.Color.SetColor(Colors.Black);
            Fill = new BrushDesigner();
            Fill.SetBrush(Brushes.Transparent);
        }
    }
}
