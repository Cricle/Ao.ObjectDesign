using Ao.ObjectDesign.Wpf.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                var brush = new ImageBrush();
                brush.ImageSource = source.ImageSource;
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
        protected void RaiseImageBrushChanged()
        {
            RaisePropertyChanged(nameof(ImageBrush));
        }
    }
}
