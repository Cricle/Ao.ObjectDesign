using System.Windows;

namespace Ao.ObjectDesign.Test
{
    class MyDepObject : DependencyObject
    {
        public int Age
        {
            get { return (int)GetValue(AgeProperty); }
            set { SetValue(AgeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Age.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AgeProperty =
            DependencyProperty.Register("Age", typeof(int), typeof(MyDepObject), new PropertyMetadata(0));
    }
}
