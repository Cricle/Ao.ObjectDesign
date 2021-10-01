using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(ImageBrush))]
    public class ImageBrushDesigner : TileBrushDesigner
    {
        public ImageBrushDesigner()
        {
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (IncludePropertyNames.Contains(e.PropertyName))
            {
                RaiseImageBrushChanged();
            }
        }

        private ImageSourceDesigner source;

        [DefaultValue(null)]
        public virtual ImageSourceDesigner Source
        {
            get => source;
            set
            {
                Set(ref source, value);
                RaiseImageBrushChanged();
            }
        }
        [PlatformTargetProperty]
        public virtual ImageBrush ImageBrush
        {
            get
            {
                if (source is null)
                {
                    return null;
                }
                ImageBrush brush = new ImageBrush
                {
                    ImageSource = source.ImageSource
                };
                Apply(brush);
                return brush;
            }
            set
            {
                if (value is null)
                {
                    Source = null;
                }
                else
                {
                    Source = new ImageSourceDesigner { ImageSource = value.ImageSource };
                }
                WriteTo(value);
            }
        }
        private static readonly PropertyChangedEventArgs imageBrushChangedEventArgs = new PropertyChangedEventArgs(nameof(ImageBrush));
        protected void RaiseImageBrushChanged()
        {
            RaisePropertyChanged(imageBrushChangedEventArgs);
        }
        protected override void TileBrushPropertyChanged()
        {
            RaiseImageBrushChanged();
        }
    }
}
