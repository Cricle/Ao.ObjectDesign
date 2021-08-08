using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System;
using System.ComponentModel;
using System.Net.Cache;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(ImageSource))]
    public class ImageSourceDesigner : NotifyableObject
    {
        private string uri;
        private int? decodePixelHeight;
        private int? decodePixelWidth;
        private BitmapCacheOption cacheOption = BitmapCacheOption.OnLoad;
        private BitmapCreateOptions createOptions;
        private RequestCacheLevel requestCacheLevel;

        [DefaultValue(RequestCacheLevel.Default)]
        public virtual RequestCacheLevel RequestCacheLevel
        {
            get => requestCacheLevel;
            set
            {
                Set(ref requestCacheLevel, value);
                RaiseImageSourceChanged();
            }
        }
        [DefaultValue(BitmapCreateOptions.None)]
        public virtual BitmapCreateOptions CreateOptions
        {
            get => createOptions;
            set
            {
                Set(ref createOptions, value);
                RaiseImageSourceChanged();
            }
        }
        [DefaultValue(BitmapCacheOption.None)]
        public virtual BitmapCacheOption CacheOption
        {
            get => cacheOption;
            set
            {
                Set(ref cacheOption, value);
                RaiseImageSourceChanged();
            }
        }
        [DefaultValue(null)]
        public virtual int? DecodePixelWidth
        {
            get => decodePixelWidth;
            set
            {
                Set(ref decodePixelWidth, value);
                RaiseImageSourceChanged();
            }
        }
        [DefaultValue(null)]
        public virtual int? DecodePixelHeight
        {
            get => decodePixelHeight;
            set
            {
                Set(ref decodePixelHeight, value);
                RaiseImageSourceChanged();
            }
        }

        [DefaultValue(null)]
        public virtual string Uri
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
                BitmapImage bitmap = new BitmapImage
                {
                    CacheOption = cacheOption,
                    CreateOptions = createOptions
                };
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
                bitmap.UriSource = new Uri(uri);
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
                    Uri = img.UriSource.AbsoluteUri;
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
