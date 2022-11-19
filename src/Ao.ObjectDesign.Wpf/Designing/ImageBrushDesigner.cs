using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows.Media;

namespace Ao.ObjectDesign.Designing
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
        [PlatformTargetGetMethod]
        public virtual ImageBrush GetImageBrush()
        {
            if (source is null)
            {
                return null;
            }
            ImageBrush brush = new ImageBrush
            {
                ImageSource = source.GetImageSource(),
            };
            Apply(brush);
            return brush;
        }
        [PlatformTargetSetMethod]
        public virtual void SetImageBrush(ImageBrush value)
        {
            if (value is null)
            {
                Source = null;
            }
            else
            {
                Source = new ImageSourceDesigner();
                Source.SetImageSource(value.ImageSource);
            }
            WriteTo(value);
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
