using Ao.ObjectDesign.Wpf.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(FrameworkElement))]
    public class FrameworkElementSetting : UIElementSetting, IMiddlewareDesigner<FrameworkElement>
    {
        private string name;
        private bool useLayoutRounding;
        private FlowDirection flowDirection;
        private ThicknessDesigner margin;
        private VerticalAlignment verticalAlignment;
        private HorizontalAlignment horizontalAlignment;
        private string toolTip;
        private bool forceCursor;
        private double width;
        private double height;
        private double minWidth;
        private double maxWidth;
        private double minHeight;
        private double maxHeight;
        private CursorDesigner cursor;

        public virtual CursorDesigner Cursor
        {
            get => cursor;
            set => Set(ref cursor, value);
        }

        public virtual double MinHeight
        {
            get => minHeight;
            set
            {
                Set(ref minHeight, value);
            }
        }

        public virtual double MaxHeight
        {
            get => maxHeight;
            set
            {
                Set(ref maxHeight, value);
            }
        }


        public virtual double MaxWidth
        {
            get => maxWidth;
            set
            {
                Set(ref maxWidth, value);
            }
        }

        public virtual double MinWidth
        {
            get => minWidth;
            set
            {
                Set(ref minWidth, value);
            }
        }

        public virtual double Height
        {
            get => height;
            set
            {
                Set(ref height, value);
            }
        }
        public virtual double Width
        {
            get => width;
            set
            {
                Set(ref width, value);
            }
        }
        protected double Clamp(double value, double min, double max)
        {
            if (min > max)
            {
                return value;
            }
            if (max == double.PositiveInfinity)
            {
                return Math.Max(value, min);
            }
            return Math.Min(Math.Max(value, min), max);
        }

        public virtual bool ForceCursor
        {
            get => forceCursor;
            set => Set(ref forceCursor, value);
        }

        [TransferOrigin(typeof(object))]
        public virtual string ToolTip
        {
            get => toolTip;
            set => Set(ref toolTip, value);
        }

        public virtual HorizontalAlignment HorizontalAlignment
        {
            get => horizontalAlignment;
            set => Set(ref horizontalAlignment, value);
        }

        public virtual VerticalAlignment VerticalAlignment
        {
            get => verticalAlignment;
            set => Set(ref verticalAlignment, value);
        }

        public virtual ThicknessDesigner Margin
        {
            get => margin;
            set => Set(ref margin, value);
        }

        public virtual FlowDirection FlowDirection
        {
            get => flowDirection;
            set => Set(ref flowDirection, value);
        }

        public virtual bool UseLayoutRounding
        {
            get => useLayoutRounding;
            set => Set(ref useLayoutRounding, value);
        }

        public virtual string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        public override void SetDefault()
        {
            base.SetDefault();
            MinWidth = MinHeight = 0;
            MaxWidth = MaxHeight = double.PositiveInfinity;
            Width = Height = double.NaN;
            Tag = Name = null;
            UseLayoutRounding = false;
            FlowDirection = FlowDirection.LeftToRight;
            Margin = null;
            VerticalAlignment = VerticalAlignment.Stretch;
            HorizontalAlignment = HorizontalAlignment.Stretch;
            ToolTip = null;
            ForceCursor = false;
            Cursor = null;
        }

        public void Apply(FrameworkElement value)
        {
            if (value != null)
            {
                base.Apply(value);
                MinWidth = value.MinWidth;
                MinHeight = value.MinHeight;
                MaxHeight = value.MaxHeight;
                MaxWidth = value.MaxWidth;
                Width = value.Width;
                Height = value.Height;
                Tag = value.Tag?.ToString();
                Name = value.Name;
                UseLayoutRounding = value.UseLayoutRounding;
                FlowDirection = value.FlowDirection;
                if (margin is null)
                {
                    Margin = new ThicknessDesigner { Thickness = value.Margin };
                }
                else
                {
                    margin.Thickness = value.Margin;
                }
                VerticalAlignment = value.VerticalAlignment;
                HorizontalAlignment = value.HorizontalAlignment;
                ToolTip = value.ToolTip?.ToString();
                ForceCursor = value.ForceCursor;
                if (cursor is null)
                {
                    Cursor = new CursorDesigner { Cursor = value.Cursor };
                }
                else
                {
                    cursor.Cursor = value.Cursor;
                }
            }
        }

        public void WriteTo(FrameworkElement value)
        {
            if (value != null)
            {
                WriteTo((UIElement)value);
                value.MinWidth = minWidth;
                value.MinHeight = minHeight;
                value.MaxWidth = maxWidth;
                value.MaxHeight = maxHeight;
                value.Width = width;
                value.Height = height;
                value.Tag = Tag;
                value.Name = name;
                value.UseLayoutRounding = useLayoutRounding;
                value.FlowDirection = flowDirection;
                value.Margin = margin?.Thickness ?? default;
                value.VerticalAlignment = verticalAlignment;
                value.HorizontalAlignment = horizontalAlignment;
                value.ToolTip = toolTip;
                value.ForceCursor = forceCursor;
                value.Cursor = cursor?.Cursor;
            }
        }
    }
}
