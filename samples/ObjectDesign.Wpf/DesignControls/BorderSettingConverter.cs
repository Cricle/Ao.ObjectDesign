using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace ObjectDesign.Wpf.DesignControls
{
    public class BorderSettingConverter : IValueConverter
    {
        private FrameworkElement lightBorder;
        private CornerRadiusDesigner design;
        private DottedLineAdorner adorner;
        private AdornerLayer layout;

        public BorderSettingConverter(FrameworkElement lightBorder, CornerRadiusDesigner design)
        {
            this.lightBorder = lightBorder;
            this.design = design;
            layout = AdornerLayer.GetAdornerLayer(lightBorder);
            adorner = new DottedLineAdorner(lightBorder);
            //adorner.SetBinding(DottedLineAdorner.StrokeWidthProperty, new Binding(nameof(CornerDesign.BorderWidth))
            //{
            //    Source = design
            //});
            //adorner.SetBinding(DottedLineAdorner.BrushProperty, new Binding(nameof(LightBorder.BorderBrush))
            //{
            //    Source = lightBorder
            //});
            adorner.SetBinding(DottedLineAdorner.RadiusXProperty, new Binding("CornerRadius.TopLeft")
            {
                Source = lightBorder
            });
            adorner.SetBinding(DottedLineAdorner.RadiusYProperty, new Binding("CornerRadius.BottomRight")
            {
                Source = lightBorder
            });
            adorner.SetBinding(DottedLineAdorner.OpacityProperty, new Binding("Opacity")
            {
                Source = lightBorder
            });
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //var brush = new SolidColorBrush(design.BorderColor);
            //if (design.IsSolid)
            //{
            //    layout.Remove(adorner);
            //    return brush;
            //}
            //layout.Add(adorner);
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is SolidColorBrush;
        }
    }
}
