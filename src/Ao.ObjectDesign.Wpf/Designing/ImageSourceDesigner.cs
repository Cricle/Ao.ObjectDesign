using Ao.ObjectDesign.Wpf.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(ImageSource))]
    public class ImageSourceDesigner : NotifyableObject
    {
        private Uri uri;
        private int? decodePixelHeight;
        private int? decodePixelWidth;
        private BitmapCacheOption cacheOption = BitmapCacheOption.OnLoad;
        private BitmapCreateOptions createOptions;
        private RequestCacheLevel requestCacheLevel;

        public virtual RequestCacheLevel RequestCacheLevel
        {
            get => requestCacheLevel;
            set
            {
                Set(ref requestCacheLevel, value);
                RaiseImageSourceChanged();
            }
        }
        public virtual BitmapCreateOptions CreateOptions
        {
            get => createOptions;
            set
            {
                Set(ref createOptions, value);
                RaiseImageSourceChanged();
            }
        }
        public virtual BitmapCacheOption CacheOption
        {
            get => cacheOption;
            set
            {
                Set(ref cacheOption, value);
                RaiseImageSourceChanged();
            }
        }
        public virtual int? DecodePixelWidth
        {
            get => decodePixelWidth;
            set
            {
                Set(ref decodePixelWidth, value);
                RaiseImageSourceChanged();
            }
        }
        public virtual int? DecodePixelHeight
        {
            get => decodePixelHeight;
            set
            {
                Set(ref decodePixelHeight, value);
                RaiseImageSourceChanged();
            }
        }

        public virtual Uri Uri
        {
            get => uri;
            set
            {
                Set(ref uri, value);
                RaiseImageSourceChanged();
            }
        }
        [PlatformTargetProperty]
        public ImageSource ImageSource
        {
            get
            {
                if (Uri is null)
                {
                    return null;
                }
                var bitmap = new BitmapImage();
                bitmap.CacheOption = cacheOption;
                bitmap.CreateOptions = createOptions;
                if (decodePixelHeight != null)
                {
                    bitmap.DecodePixelHeight = decodePixelHeight.Value;
                }
                if (decodePixelWidth != null)
                {
                    bitmap.DecodePixelWidth = decodePixelWidth.Value;
                }
                bitmap.UriCachePolicy = new RequestCachePolicy(requestCacheLevel);
                bitmap.BeginInit();
                bitmap.UriSource = uri;
                bitmap.EndInit();
                return bitmap;
            }
            set
            {
                if (value is null || !(value is BitmapImage img) || img.UriSource is null)//TODO:支持流
                {
                    Uri = null;
                    DecodePixelHeight = DecodePixelWidth = null;
                    CacheOption = default;
                    CreateOptions = default;
                    RequestCacheLevel = default;
                }
                else
                {
                    Uri = img.UriSource;
                    DecodePixelHeight = img.DecodePixelHeight;
                    DecodePixelWidth = img.DecodePixelWidth;
                    CacheOption = img.CacheOption;
                    CreateOptions = img.CreateOptions;
                    RequestCacheLevel = img.UriCachePolicy?.Level ?? RequestCacheLevel.Default;
                }
            }
        }

        protected void RaiseImageSourceChanged()
        {
            RaisePropertyChanged(nameof(ImageSource));
        }

    }
}
