﻿using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [UnfoldMapping]
    [DesignFor(typeof(Brush))]
    public class BrushDesigner : NotifyableObject
    {
        public BrushDesigner()
        {
            SolidColorBrushDesigner = new SolidColorBrushDesigner();
            LinearGradientBrushDesigner = new LinearGradientBrushDesigner();
            RadialGradientBrushDesigner = new RadialGradientBrushDesigner();
            ImageBrushDesigner = new ImageBrushDesigner();
        }
        private static readonly PropertyChangedEventArgs imageBrushDesignerChangedEventArgs = new PropertyChangedEventArgs(nameof(ImageBrushDesigner));
        private static readonly PropertyChangedEventArgs solidColorBrushDesignerChangedEventArgs = new PropertyChangedEventArgs(nameof(SolidColorBrushDesigner));
        private static readonly PropertyChangedEventArgs linearGradientBrushDesignerChangedEventArgs = new PropertyChangedEventArgs(nameof(LinearGradientBrushDesigner));
        private PenBrushTypes type;

        private SolidColorBrushDesigner solidColorBrushDesigner;
        private LinearGradientBrushDesigner linearGradientBrushDesigner;
        private RadialGradientBrushDesigner radialGradientBrushDesigner;
        private ImageBrushDesigner imageBrushDesigner;

        public virtual RadialGradientBrushDesigner RadialGradientBrushDesigner
        {
            get => radialGradientBrushDesigner;
            set
            {
                if (value != radialGradientBrushDesigner)
                {
                    CoreSetRadialGradientBrushDesigner(value);
                }
            }
        }
        protected void CoreSetRadialGradientBrushDesigner(RadialGradientBrushDesigner value)
        {
            if (radialGradientBrushDesigner != null)
            {
                radialGradientBrushDesigner.PropertyChanged -= OnPropertyChanged;
            }
            if (value != null)
            {
                value.PropertyChanged += OnPropertyChanged;
            }
            Set(ref radialGradientBrushDesigner, value);
            RaiseBrushChanged();
        }


        public virtual LinearGradientBrushDesigner LinearGradientBrushDesigner
        {
            get => linearGradientBrushDesigner;
            set
            {
                if (value != linearGradientBrushDesigner)
                {
                    CoreSetLinearGradientBrushDesigner(value);
                }
            }
        }
        protected void CoreSetLinearGradientBrushDesigner(LinearGradientBrushDesigner value)
        {
            if (linearGradientBrushDesigner != null)
            {
                linearGradientBrushDesigner.PropertyChanged -= OnPropertyChanged;
            }
            if (value != null)
            {
                value.PropertyChanged += OnPropertyChanged;
            }
            Set(ref linearGradientBrushDesigner, value);
            RaiseBrushChanged();
        }

        public virtual SolidColorBrushDesigner SolidColorBrushDesigner
        {
            get => solidColorBrushDesigner;
            set
            {
                if (value != solidColorBrushDesigner)
                {
                    CoreSetSolidColorBrushDesigner(value);
                }
            }
        }
        protected virtual void CoreSetSolidColorBrushDesigner(SolidColorBrushDesigner value)
        {
            if (solidColorBrushDesigner != null)
            {
                solidColorBrushDesigner.PropertyChanged -= OnPropertyChanged;
            }
            if (value != null)
            {
                value.PropertyChanged += OnPropertyChanged;
            }
            Set(ref solidColorBrushDesigner, value);
            RaiseBrushChanged();

        }

        public virtual ImageBrushDesigner ImageBrushDesigner
        {
            get => imageBrushDesigner;
            set
            {
                if (value != imageBrushDesigner)
                {
                    CoreSetImageBrushDesigner(value);
                    Type = PenBrushTypes.Image;
                }
            }
        }

        protected void CoreSetImageBrushDesigner(ImageBrushDesigner value)
        {
            if (imageBrushDesigner != null)
            {
                imageBrushDesigner.PropertyChanged -= OnPropertyChanged;
            }
            if (value != null)
            {
                value.PropertyChanged += OnPropertyChanged;
            }
            Set(ref imageBrushDesigner, value);
            RaiseBrushChanged();

        }
        [PlatformTargetGetMethod]
        [return: ProvideMulityValues]
        [return: PropertyProvideValue(nameof(SolidColorBrushDesigner), typeof(SolidColorBrush))]
        [return: PropertyProvideValue(nameof(LinearGradientBrushDesigner), typeof(LinearGradientBrush))]
        [return: PropertyProvideValue(nameof(RadialGradientBrushDesigner), typeof(RadialGradientBrush))]
        [return: PropertyProvideValue(nameof(ImageBrushDesigner), typeof(ImageBrush))]
        public virtual Brush GetBrush()
        {
            PenBrushTypes t = type;
            if (t == PenBrushTypes.Solid)
            {
                return SolidColorBrushDesigner?.GetSolidColorBrush();
            }
            else if (t == PenBrushTypes.Liner)
            {
                return LinearGradientBrushDesigner?.GetLinearGradientBrush();
            }
            else if (t == PenBrushTypes.Radial)
            {
                return RadialGradientBrushDesigner?.GetRadialGradientBrush();
            }
            else if (t == PenBrushTypes.Image)
            {
                return ImageBrushDesigner?.GetImageBrush();
            }
            return null;
        }
        [PlatformTargetSetMethod]
        public virtual void SetBrush(Brush value)
        {
            if (value is null)
            {
                Type = PenBrushTypes.None;
            }
            else if (value is SolidColorBrush scb)
            {
                Type = PenBrushTypes.Solid;
                SolidColorBrushDesigner.SetSolidColorBrush(scb);
            }
            else if (value is LinearGradientBrush lgb)
            {
                Type = PenBrushTypes.Liner;
                LinearGradientBrushDesigner.SetLinearGradientBrush(lgb);
            }
            else if (value is RadialGradientBrush rgb)
            {
                Type = PenBrushTypes.Radial;
                RadialGradientBrushDesigner.SetRadialGradientBrush(rgb);
            }
            else if (value is ImageBrush ib)
            {
                Type = PenBrushTypes.Image;
                ImageBrushDesigner.SetImageBrush(ib);
            }
            else
            {
                Type = PenBrushTypes.None;
            }
        }
        [DefaultValue(PenBrushTypes.None)]
        public virtual PenBrushTypes Type
        {
            get => type;
            set
            {
                if (type == value)
                {
                    return;
                }
                Set(ref type, value);
                if (value == PenBrushTypes.Liner)
                {
                    if (LinearGradientBrushDesigner is null)
                    {
                        CoreSetLinearGradientBrushDesigner(new LinearGradientBrushDesigner());
                    }
                }
                else if (value == PenBrushTypes.Radial)
                {
                    if (RadialGradientBrushDesigner is null)
                    {
                        CoreSetRadialGradientBrushDesigner(new RadialGradientBrushDesigner());
                    }
                }
                else if (value == PenBrushTypes.Solid)
                {
                    if (SolidColorBrushDesigner is null)
                    {
                        CoreSetSolidColorBrushDesigner(new SolidColorBrushDesigner());
                    }
                }
                else if (value == PenBrushTypes.Image)
                {
                    if (ImageBrushDesigner is null)
                    {
                        CoreSetImageBrushDesigner(new ImageBrushDesigner());
                    }
                }
                RaiseBrushChanged();
            }
        }
        private static readonly PropertyChangedEventArgs brushChangedEventArgs = new PropertyChangedEventArgs(nameof(Brush));
        public void RaiseBrushChanged()
        {
            RaisePropertyChanged(brushChangedEventArgs);
        }
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaiseBrushChanged();
        }
    }
}
