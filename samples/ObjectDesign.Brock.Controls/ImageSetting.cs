using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using ObjectDesign.Brock.Components;
using System.Windows.Controls;
using System.Windows.Media;

namespace ObjectDesign.Brock.Controls
{
    [MappingFor(typeof(Image))]
    public class ImageSetting : FrameworkElementSetting
    {
        private ImageSourceDesigner source;
        private Stretch stretch;
        private StretchDirection stretchDirection;

        public StretchDirection StretchDirection
        {
            get => stretchDirection;
            set => Set(ref stretchDirection, value);
        }

        public Stretch Stretch
        {
            get => stretch;
            set => Set(ref stretch, value);
        }

        public ImageSourceDesigner Source
        {
            get => source;
            set => Set(ref source, value);
        }
        public override void SetDefault()
        {
            base.SetDefault();
            Source = new ImageSourceDesigner();
            Stretch = Stretch.None;
            StretchDirection = StretchDirection.UpOnly;
        }
    }
}
