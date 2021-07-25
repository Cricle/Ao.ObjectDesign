using Ao.ObjectDesign.Designing.Annotations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ObjectDesign.Wpf.Controls
{
    [HasSetting(typeof(MoveIdSetting))]
    public class MoveId : UserControl
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Brush IdBrush
        {
            get { return (Brush)GetValue(IdBrushProperty); }
            set { SetValue(IdBrushProperty, value); }
        }


        public double IdFontSize
        {
            get { return (double)GetValue(IdFontSizeProperty); }
            set { SetValue(IdFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IdFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IdFontSizeProperty =
            DependencyProperty.Register("IdFontSize", typeof(double), typeof(MoveId), new PropertyMetadata(12d));


        // Using a DependencyProperty as the backing store for IdBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IdBrushProperty =
            DependencyProperty.Register("IdBrush", typeof(Brush), typeof(MoveId), new PropertyMetadata(null));



        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MoveId), new PropertyMetadata(null));


    }
}
