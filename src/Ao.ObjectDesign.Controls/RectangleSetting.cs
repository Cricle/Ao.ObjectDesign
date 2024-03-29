﻿using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System.ComponentModel;
using System.Windows.Shapes;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(Rectangle))]
    public class RectangleSetting : ShapeSetting, IMiddlewareDesigner<Rectangle>
    {
        private double radiusX;
        private double radiusY;

        [DefaultValue(0d)]
        public virtual double RadiusX
        {
            get => radiusX;
            set => Set(ref radiusX, value);
        }
        [DefaultValue(0d)]
        public virtual double RadiusY
        {
            get => radiusY;
            set => Set(ref radiusY, value);
        }
        public override void SetDefault()
        {
            base.SetDefault();
            RadiusX = RadiusY = 0;
        }

        public void Apply(Rectangle value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((Shape)value);
                RadiusY = value.RadiusY;
                RadiusX = value.RadiusX;
            }
        }

        public void WriteTo(Rectangle value)
        {
            if (value != null)
            {
                WriteTo((Shape)value);
                value.RadiusY = RadiusY;
                value.RadiusX = RadiusX;
            }
        }
    }
}
