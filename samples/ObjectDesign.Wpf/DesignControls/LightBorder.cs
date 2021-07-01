using Ao.ObjectDesign.Wpf.Designing;
using ObjectDesign.Wpf.Views;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ObjectDesign.Wpf.DesignControls
{

    public class LightBorder : Border
    {
        public LightBorder()
        {
            BorderBrush = Brushes.Black;
        }


        internal MyControl MyControl
        {
            get { return (MyControl)GetValue(MyControlProperty); }
            set { SetValue(MyControlProperty, value); }
        }
        // Using a DependencyProperty as the backing store for MyControl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyControlProperty =
            DependencyProperty.Register("MyControl", typeof(MyControl), typeof(LightBorder), new PropertyMetadata(null, OnMyControlChanged));

        private static void OnMyControlChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is MyControl c && o is LightBorder fe)
            {
                void Bind()
                {
                    fe.SetBinding(CornerRadiusProperty, new Binding(nameof(CornerDesign.CornerRadius))
                    {
                        Source = c.CornerDesign,
                        Mode = BindingMode.OneWay
                    });
                    fe.SetBinding(WidthProperty, new Binding(nameof(LocationSize.Width))
                    {
                        Source = c.Size,
                        Mode = BindingMode.OneWay
                    });
                    fe.SetBinding(HeightProperty, new Binding(nameof(LocationSize.Height))
                    {
                        Source = c.Size,
                        Mode = BindingMode.OneWay
                    });
                    fe.SetBinding(BorderBrushProperty, new Binding(nameof(CornerDesign.IsSolid))
                    {
                        Source = c.CornerDesign,
                        Converter = new BorderSettingConverter(fe, c.CornerDesign),
                        ConverterParameter = c,
                        Mode = BindingMode.OneWay
                    });
                    fe.SetBinding(BorderThicknessProperty, new Binding(nameof(CornerDesign.BorderMargin))
                    {
                        Source = c.CornerDesign,
                        Mode = BindingMode.OneWay
                    });
                    fe.SetBinding(OpacityProperty, new Binding(nameof(Opacity))
                    {
                        Source = c,
                        Mode = BindingMode.OneWay
                    });
                    fe.SetBinding(BackgroundProperty, new Binding(nameof(PenBrush.Brush))
                    {
                        Source = c.Background,
                        Mode = BindingMode.OneWay
                    });
                    var txt = new TextBlock { Text = "hello" };
                    txt.SetBinding(TextBlock.FontFamilyProperty, new Binding(nameof(FontSetting.FontFamily))
                    {
                        Source = c.Font,
                        Mode = BindingMode.OneWay
                    });
                    txt.SetBinding(TextBlock.FontSizeProperty, new Binding(nameof(FontSetting.FontSize))
                    {
                        Source = c.Font,
                        Mode = BindingMode.OneWay
                    });
                    txt.SetBinding(TextBlock.FontStyleProperty, new Binding(nameof(FontSetting.FontStyle))
                    {
                        Source = c.Font,
                        Mode = BindingMode.OneWay
                    });
                    txt.SetBinding(TextBlock.FontWeightProperty, new Binding(nameof(FontSetting.FontWeight))
                    {
                        Source = c.Font,
                        Mode = BindingMode.OneWay
                    });
                    fe.Child = txt;
                }
                if (fe.IsLoaded)
                {
                    Bind();
                }
                else
                {
                    RoutedEventHandler hd = (_, __) => Bind();
                    fe.Loaded += hd;
                    fe.Unloaded -= hd;
                }
            }
        }
    }
}
