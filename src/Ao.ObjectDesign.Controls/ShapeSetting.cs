﻿using Ao.ObjectDesign.Wpf.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(Shape))]
    public class ShapeSetting : FrameworkElementSetting, IMiddlewareDesigner<Shape>
    {
        private BrushDesigner stroke;
        private PenLineCap strokeEndLineCap;
        private PenLineCap strokeStartLineCap;
        private double strokeThickness;
        private BrushDesigner fill;
        private double strokeDashOffset;
        private Stretch stretch;
        private double strokeMiterLimit;
        private DoubleCollectionDesigner strokeDashArray;
        private PenLineCap strokeDashCap;
        private PenLineJoin strokeLineJoin;

        public virtual PenLineJoin StrokeLineJoin
        {
            get => strokeLineJoin;
            set => Set(ref strokeLineJoin, value);
        }

        public virtual PenLineCap StrokeDashCap
        {
            get => strokeDashCap;
            set => Set(ref strokeDashCap, value);
        }

        public virtual DoubleCollectionDesigner StrokeDashArray
        {
            get => strokeDashArray;
            set => Set(ref strokeDashArray, value);
        }

        public virtual double StrokeMiterLimit
        {
            get => strokeMiterLimit;
            set => Set(ref strokeMiterLimit, value);
        }

        public virtual Stretch Stretch
        {
            get => stretch;
            set => Set(ref stretch, value);
        }

        public virtual double StrokeDashOffset
        {
            get => strokeDashOffset;
            set => Set(ref strokeDashOffset, value);
        }

        public virtual BrushDesigner Fill
        {
            get => fill;
            set => Set(ref fill, value);
        }

        public virtual double StrokeThickness
        {
            get => strokeThickness;
            set => Set(ref strokeThickness, value);
        }


        public virtual PenLineCap StrokeEndLineCap
        {
            get => strokeEndLineCap;
            set => Set(ref strokeEndLineCap, value);
        }
        public virtual PenLineCap StrokeStartLineCap
        {
            get => strokeStartLineCap;
            set => Set(ref strokeStartLineCap,value);
        }

        public virtual BrushDesigner Stroke
        {
            get => stroke;
            set => Set(ref stroke, value);
        }
        public override void SetDefault()
        {
            base.SetDefault();
            Stroke = null;
            StrokeEndLineCap =
                StrokeStartLineCap = PenLineCap.Flat;
            StrokeThickness = 1;
            Fill = null;
            StrokeDashOffset = 0;
            Stretch = Stretch.None;
            StrokeMiterLimit = 0;
            StrokeDashCap = PenLineCap.Flat;
            StrokeLineJoin = PenLineJoin.Miter;
        }

        public void Apply(Shape value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((FrameworkElement)value);
                Stroke = new BrushDesigner { Brush=value.Stroke};
                StrokeEndLineCap = value.StrokeEndLineCap;
                StrokeStartLineCap = value.StrokeStartLineCap;
                StrokeThickness = value.StrokeThickness;
                Fill = new BrushDesigner { Brush=value.Fill};
                StrokeDashOffset = value.StrokeDashOffset;
                Stretch = value.Stretch;
                StrokeMiterLimit = value.StrokeMiterLimit;
                StrokeDashCap = value.StrokeDashCap;
                StrokeLineJoin = value.StrokeLineJoin;
                if (value.StrokeDashArray != null)
                {
                    if (strokeDashArray is null)
                    {
                        StrokeDashArray = new DoubleCollectionDesigner { DoubleCollection = value.StrokeDashArray };
                    }
                    else
                    {
                        strokeDashArray.Clear();
                        strokeDashArray.AddRange(value.StrokeDashArray);
                    }
                }
            }
        }

        public void WriteTo(Shape value)
        {
            if (value!=null)
            {
                WriteTo((FrameworkElement)value);
                value.Stroke = stroke?.Brush;
                value.StrokeEndLineCap = strokeEndLineCap;
                value.StrokeStartLineCap = strokeStartLineCap;
                value.StrokeThickness = strokeThickness;
                value.Fill = fill?.Brush;
                value.StrokeDashOffset = strokeDashOffset;
                value.Stretch = stretch;
                value.StrokeMiterLimit = strokeMiterLimit;
                value.StrokeDashCap = strokeDashCap;
                value.StrokeLineJoin = strokeLineJoin;
            }
        }
    }
}
