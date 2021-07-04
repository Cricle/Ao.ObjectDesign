﻿using Ao.ObjectDesign.Wpf.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(Image))]
    public class ImageSetting : FrameworkElementSetting,IMiddlewareDesigner<Image>
    {
        private ImageSourceDesigner source;
        private Stretch stretch;
        private StretchDirection stretchDirection;

        public virtual StretchDirection StretchDirection
        {
            get => stretchDirection;
            set => Set(ref stretchDirection, value);
        }

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
            Source = null;
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
                Source = new ImageSourceDesigner { ImageSource = value.Source };
                StretchDirection = value.StretchDirection;
                Stretch = value.Stretch;
            }
        }

        public void WriteTo(Image value)
        {
            if (value!=null)
            {
                WriteTo((FrameworkElement)value);
                value.StretchDirection = stretchDirection;
                value.Stretch = stretch;
            }
        }
    }
}
