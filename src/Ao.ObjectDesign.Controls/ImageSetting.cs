using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(Image))]
    public class ImageSetting : FrameworkElementSetting, IMiddlewareDesigner<Image>
    {
        private ImageSourceDesigner source;
        private Stretch stretch;
        private StretchDirection stretchDirection;

        [DefaultValue(StretchDirection.Both)]
        public virtual StretchDirection StretchDirection
        {
            get => stretchDirection;
            set => Set(ref stretchDirection, value);
        }

        [DefaultValue(Stretch.Uniform)]
        public virtual Stretch Stretch
        {
            get => stretch;
            set => Set(ref stretch, value);
        }

        public virtual ImageSourceDesigner Source
        {
            get => source;
            set => Set(ref source, value);
        }

        public override void SetDefault()
        {
            base.SetDefault();
            Source = new ImageSourceDesigner();
            StretchDirection = StretchDirection.Both;
            Stretch = Stretch.Uniform;
        }

        public void Apply(Image value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((FrameworkElement)value);
                Source = new ImageSourceDesigner();
                Source.SetImageSource(value.Source);
                StretchDirection = value.StretchDirection;
                Stretch = value.Stretch;
            }
        }

        public void WriteTo(Image value)
        {
            if (value != null)
            {
                WriteTo((FrameworkElement)value);
                value.StretchDirection = stretchDirection;
                value.Stretch = stretch;
                value.Source = source?.GetImageSource();
            }
        }
    }
}
