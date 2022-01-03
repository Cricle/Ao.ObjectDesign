using ObjectDesign.Brock.Services;
using System;
using System.Globalization;
using System.IO;
using System.IO.Abstractions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;
using ObjectDesign.Brock.Controls;

namespace ObjectDesign.Brock.Converters
{
    public class ResourceIdentityImageConverter : IValueConverter
    {
        public static readonly ResourceIdentityImageConverter Absolute = new ResourceIdentityImageConverter();
        internal static readonly FileSystem defaultFileSystem = new FileSystem();

        public ResourceIdentityImageConverter(IDirectoryInfo workplace)
        {
            Workplace = workplace;
        }
        public ResourceIdentityImageConverter()
        {
        }

        internal ImageManager ImageManager { get; set; }

        public IDirectoryInfo Workplace { get; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ResourceIdentity ri)
            {
                object image;
                if (ImageManager != null)
                {
                    image = ImageManager.GetImage(ri, true);
                }
                else
                {
                    image = Create(ri, Workplace);
                }
                if (image==null)
                {
                    image = ImageManager.TranslateImage;
                }
                return image;
            }
            return Binding.DoNothing;
        }
        public static bool SetWhenGif(DependencyObject image, IFileSystem fs, ResourceIdentity ri)
        {
            var path = ri.GetPath(fs, null);
            if (!File.Exists(path))
            {
                return false;
            }
            var img = (Image)image;
            if (img.IsLoaded)
            {
                Run();
            }
            else
            {
                img.Loaded += (_, __) => Run();
            }
            return true;

            void Run()
            {
                var f = File.OpenRead(path);
                var s = new BitmapImage();
                s.BeginInit();
                s.CacheOption = BitmapCacheOption.OnLoad;
                s.StreamSource = f;
                s.EndInit();
                ImageBehavior.SetAnimatedSource((Image)image, s);
            }
        }

        public static ImageSource Create(ResourceIdentity ri, IDirectoryInfo dir)
        {
            ImageSource image = null;
            switch (ri.Type)
            {
                case ResourceTypes.Uri:
                    image = CreateByUri(ri);
                    break;
                case ResourceTypes.Path:
                    image = CreateByPath(ri, dir);
                    break;
                default:
                    break;
            }
            return image;
        }
        private static ImageSource CreateByUri(ResourceIdentity ri)
        {
            if (!Uri.TryCreate(ri.Uri, UriKind.Absolute, out _))
            {
                return null;
            }
            var uri = new Uri(ri.Uri);
            var s = new BitmapImage();
            s.BeginInit();
            PrepareImage(s);
            s.UriSource = uri;
            s.EndInit();
            return s;
        }
        private static ImageSource CreateByPath(ResourceIdentity ri, IDirectoryInfo dir)
        {
            if (string.IsNullOrEmpty(ri.ResourceName))
            {
                return null;
            }
            var path = ri.ResourceName;
            if (!string.IsNullOrEmpty(ri.ResourceGroupName))
            {
                path = Path.Combine(ri.ResourceGroupName, ri.ResourceName);
            }
            if (dir != null)
            {
                path = Path.Combine(dir.FullName, path);
            }
            if (dir != null)
            {
                if (!dir.FileSystem.File.Exists(path))
                {
                    return null;
                }
            }
            else if (!defaultFileSystem.File.Exists(path))
            {
                return null;
            }
            var s = new BitmapImage();
            using (var fs = File.OpenRead(path))
            {
                s.BeginInit();
                PrepareImage(s);
                s.StreamSource = fs;
                s.EndInit();
                return s;
            }
        }
        private static void PrepareImage(BitmapImage image)
        {
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.DecodePixelWidth = (int)SystemParameters.WorkArea.Width * 2;
            image.DecodePixelHeight = (int)SystemParameters.WorkArea.Height * 2;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
