﻿using Ao.ObjectDesign.Wpf.Annotations;
using System.Windows.Media;
namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(SkewTransform))]
    public class SkewTransformDesigner : NotifyableObject
    {
        private double angleX;
        private double angleY;
        private double centerX;
        private double centerY;

        public virtual double CenterY
        {
            get => centerY;
            set
            {
                Set(ref centerY, value);
                RaiseSkewTransformChanged();
            }
        }

        public virtual double CenterX
        {
            get => centerX;
            set
            {
                Set(ref centerX, value);
                RaiseSkewTransformChanged();
            }
        }

        public virtual double AngleY
        {
            get => angleY;
            set
            {
                Set(ref angleY, value);
                RaiseSkewTransformChanged();
            }
        }

        public virtual double AngleX
        {
            get => angleX;
            set
            {
                Set(ref angleX, value);
                RaiseSkewTransformChanged();
            }
        }
        [PlatformTargetProperty]
        public virtual SkewTransform SkewTransform
        {
            get => new SkewTransform(angleX, angleY, centerX, centerY);
            set
            {
                if (value is null)
                {
                    AngleX = AngleY = CenterX = CenterY = 0;
                }
                else
                {
                    AngleX = value.AngleX;
                    AngleY = value.AngleY;
                    CenterX = value.CenterX;
                    CenterY = value.CenterY;
                }
            }
        }

        private void RaiseSkewTransformChanged()
        {
            RaisePropertyChanged(nameof(SkewTransform));
        }
    }
}