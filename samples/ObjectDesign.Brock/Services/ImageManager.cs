using ObjectDesign.Brock.Controls;
using ObjectDesign.Brock.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Net;
using System.Windows;
using System.Windows.Media;

namespace ObjectDesign.Brock.Services
{
    internal class ImageManager
    {
        public const string FeatureKey = "ObjectDesign.Brock.Services.ImageManager";

        private readonly Dictionary<ResourceIdentity, WeakReference<ImageSource>> imgCaches =
            new Dictionary<ResourceIdentity, WeakReference<ImageSource>>();

        public ImageManager(IDirectoryInfo folder)
        {
            Folder = folder;
        }

        public static readonly ImageSource TranslateImage = CreateTranslateImage();

        public IDirectoryInfo Folder { get; }

        private static ImageSource CreateTranslateImage()
        {
            var img = new DrawingImage();
            img.Drawing = new GeometryDrawing
            {
                Brush = Brushes.Transparent,
                Pen = new Pen { Brush = Brushes.Transparent },
                Geometry = new RectangleGeometry(new Rect(0, 0, 32, 32)),
            };
            img.Freeze();
            return img;
        }

        public ImageSource GetImage(ResourceIdentity identity)
        {
            return GetImage(identity, true);
        }

        public ImageSource GetImage(ResourceIdentity identity, bool cache)
        {
            ImageSource bi;
            if (cache)
            {
                if (imgCaches.TryGetValue(identity, out var m))
                {
                    if (!m.TryGetTarget(out bi))
                    {
                        bi = ResourceIdentityImageConverter.Create(identity, Folder);
                        bi?.Freeze();
                        m.SetTarget(bi);
                    }
                }
                else
                {
                    bi = ResourceIdentityImageConverter.Create(identity, Folder);
                    bi?.Freeze();
                    imgCaches.Add(identity.Clone(), new WeakReference<ImageSource>(bi));
                }
            }
            else
            {
                bi = ResourceIdentityImageConverter.Create(identity, Folder);
                bi?.Freeze();
            }
            return bi;
        }
    }
}
