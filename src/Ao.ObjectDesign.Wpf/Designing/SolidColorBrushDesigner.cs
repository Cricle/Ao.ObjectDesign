using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(SolidColorBrush))]
    public class SolidColorBrushDesigner : NotifyableObject
    {
        public SolidColorBrushDesigner()
        {
            Color = new ColorDesigner();
        }
        private ColorDesigner color;

        [DefaultValue(null)]
        public virtual ColorDesigner Color
        {
            get => color;
            set
            {
                if (color != null)
                {
                    color.PropertyChanged -= OnColorPropertyChanged;
                }
                if (value != null)
                {
                    value.PropertyChanged += OnColorPropertyChanged;
                }
                Set(ref color, value);
                RaiseSolidColorBrushChanged();
            }
        }
        private void OnColorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ColorDesigner.Color))
            {
                RaiseSolidColorBrushChanged();
            }
        }

        [PlatformTargetProperty]
        public virtual SolidColorBrush SolidColorBrush
        {
            get => new SolidColorBrush(color?.Color ?? Colors.Transparent);
            set
            {
                if (value is null)
                {
                    color = null;
                }
                else
                {
                    if (color != null)
                    {
                        color.Color = value.Color;
                    }
                    else
                    {
                        Color = new ColorDesigner { Color = value.Color };
                    }
                }
            }
        }
        protected void RaiseSolidColorBrushChanged()
        {
            RaisePropertyChanged(nameof(SolidColorBrush));
        }
    }
}
