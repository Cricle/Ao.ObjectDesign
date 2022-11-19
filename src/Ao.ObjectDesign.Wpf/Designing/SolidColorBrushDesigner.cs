using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows.Media;

namespace Ao.ObjectDesign.Designing
{
    [DesignFor(typeof(SolidColorBrush))]
    public class SolidColorBrushDesigner : NotifyableObject
    {
        public SolidColorBrushDesigner()
        {
            Color = new ColorDesigner();
        }
        private ColorDesigner color;

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
            if (e.PropertyName == "Color")
            {
                RaiseSolidColorBrushChanged();
            }
        }
        [PlatformTargetGetMethod]
        public virtual SolidColorBrush GetSolidColorBrush()
        {
            return new SolidColorBrush(color?.GetColor() ?? Colors.Transparent);
        }
        [PlatformTargetSetMethod]
        public virtual void SetSolidColorBrush(SolidColorBrush value)
        {
            if (value is null)
            {
                color = null;
            }
            else
            {
                if (color != null)
                {
                    color.SetColor(value.Color);
                }
                else
                {
                    Color = new ColorDesigner();
                    color.SetColor(value.Color);
                }
            }
        }

        private static readonly PropertyChangedEventArgs solidColorBrushEventArgs = new PropertyChangedEventArgs(nameof(SolidColorBrush));
        protected void RaiseSolidColorBrushChanged()
        {
            RaisePropertyChanged(solidColorBrushEventArgs);
        }
    }
}
