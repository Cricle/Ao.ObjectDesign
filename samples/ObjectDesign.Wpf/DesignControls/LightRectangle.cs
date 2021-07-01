using Ao.ObjectDesign.Wpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shapes;

namespace ObjectDesign.Wpf.DesignControls
{
    public static class LightRectangle
    {


        public static DesignableRectangle GetDesignableRectangle(Rectangle obj)
        {
            return (DesignableRectangle)obj.GetValue(DesignableRectangleProperty);
        }

        public static void SetDesignableRectangle(Rectangle obj, DesignableRectangle value)
        {
            obj.SetValue(DesignableRectangleProperty, value);
        }

        public static readonly DependencyProperty DesignableRectangleProperty =
            DependencyProperty.RegisterAttached("DesignableRectangle", typeof(DesignableRectangle), typeof(LightRectangle), new PropertyMetadata(null, OnDesignableRectangleChanged));

        //TODO:删除
        private static void OnDesignableRectangleChanged(DependencyObject o,DependencyPropertyChangedEventArgs e)
        {
            var rc = (Rectangle)o;
            if (e.NewValue is DesignableRectangle drc)
            {
                rc.SetBinding(Rectangle.WidthProperty, new Binding("Width")
                {
                    Source = drc.LocationSize,
                    Mode = BindingMode.TwoWay
                });
                rc.SetBinding(Rectangle.HeightProperty, new Binding("LocationSize.Height")
                {
                    Source = drc,
                    Mode = BindingMode.TwoWay
                });
                rc.SetBinding(Rectangle.StrokeProperty, new Binding("Brush")
                {
                    Source = drc.Stroke,
                    Mode = BindingMode.TwoWay
                });
                rc.SetBinding(Rectangle.StrokeThicknessProperty, new Binding("StrokeWidth")
                {
                    Source = drc,
                    Mode = BindingMode.TwoWay
                });
                rc.SetBinding(Rectangle.RadiusXProperty, new Binding("RadiusX")
                {
                    Source = drc,
                    Mode = BindingMode.TwoWay
                });
                rc.SetBinding(Rectangle.RadiusYProperty, new Binding("RadiusY")
                {
                    Source = drc,
                    Mode = BindingMode.TwoWay
                });
            }
        }
    }
}
