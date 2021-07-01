using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace ObjectDesign.Wpf.DesignControls
{
    public class DottedLineAdorner : Adorner
    {
        public double StrokeWidth
        {
            get { return (double)GetValue(StrokeWidthProperty); }
            set { SetValue(StrokeWidthProperty, value); }
        }


        public Brush Brush
        {
            get { return (Brush)GetValue(BrushProperty); }
            set { SetValue(BrushProperty, value); }
        }


        public double RadiusX
        {
            get { return (double)GetValue(RadiusXProperty); }
            set { SetValue(RadiusXProperty, value); }
        }


        public double RadiusY
        {
            get { return (double)GetValue(RadiusYProperty); }
            set { SetValue(RadiusYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RadiusY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusYProperty =
            DependencyProperty.Register("RadiusY", typeof(double), typeof(DottedLineAdorner), new PropertyMetadata(0d,(o,__)=>((FrameworkElement)o).InvalidateVisual()));


        // Using a DependencyProperty as the backing store for RadiusX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusXProperty =
            DependencyProperty.Register("RadiusX", typeof(double), typeof(DottedLineAdorner), new PropertyMetadata(0d, (o, __) => ((FrameworkElement)o).InvalidateVisual()));


        // Using a DependencyProperty as the backing store for Brush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BrushProperty =
            DependencyProperty.Register("Brush", typeof(Brush), typeof(DottedLineAdorner), new PropertyMetadata(Brushes.Black, (o, __) => ((FrameworkElement)o).InvalidateVisual()));


        // Using a DependencyProperty as the backing store for StrokeWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeWidthProperty =
            DependencyProperty.Register("StrokeWidth", typeof(double), typeof(DottedLineAdorner), new PropertyMetadata(1d, (o, __) => ((FrameworkElement)o).InvalidateVisual()));


        private readonly FrameworkElement fe;
        public DottedLineAdorner(UIElement adornedElement) 
            : base(adornedElement)
        {
            fe = (AdornedElement as FrameworkElement);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var eltSize = fe.DesiredSize;
            var pen = new Pen(Brush, StrokeWidth) {DashStyle = DashStyles.DashDot};
            drawingContext.DrawRoundedRectangle(null, pen, new Rect(0, 0, eltSize.Width, eltSize.Height), RadiusX, RadiusY);
        }
    }
}
