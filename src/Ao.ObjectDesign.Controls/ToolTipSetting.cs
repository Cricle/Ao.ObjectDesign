using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(ToolTip))]
    public class ToolTipSetting : ControlSetting, IMiddlewareDesigner<ToolTip>
    {
        private bool isOpen;
        private bool staysOpen;
        private PlacementMode placement;
        private RectDesigner placementRectangle;
        private double horizontalOffset;
        private double verticalOffset;
        private bool hasDropShadow;

        [DefaultValue(false)]
        public virtual bool HasDropShadow
        {
            get => hasDropShadow;
            set => Set(ref hasDropShadow, value);
        }

        [DefaultValue(0d)]
        public virtual double VerticalOffset
        {
            get => verticalOffset;
            set => Set(ref verticalOffset, value);
        }

        [DefaultValue(0d)]
        public virtual double HorizontalOffset
        {
            get => horizontalOffset;
            set => Set(ref horizontalOffset, value);
        }

        public virtual RectDesigner PlacementRectangle
        {
            get => placementRectangle;
            set => Set(ref placementRectangle, value);
        }

        [DefaultValue(PlacementMode.Mouse)]
        public virtual PlacementMode Placement
        {
            get => placement;
            set => Set(ref placement, value);
        }

        [DefaultValue(false)]
        public virtual bool StaysOpen
        {
            get => staysOpen;
            set => Set(ref staysOpen, value);
        }

        [DefaultValue(false)]
        public virtual bool IsOpen
        {
            get => isOpen;
            set => Set(ref isOpen, value);
        }


        public override void SetDefault()
        {
            base.SetDefault();
            IsOpen = false;
            StaysOpen = false;
            Placement = PlacementMode.Mouse;
            PlacementRectangle = new RectDesigner();
            HorizontalOffset = 0;
            VerticalOffset = 0;
            HasDropShadow = false;
        }

        public void Apply(ToolTip value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((Control)value);
                IsOpen = value.IsOpen;
                StaysOpen = value.StaysOpen;
                Placement = value.Placement;
                PlacementRectangle = new RectDesigner { Rect = value.PlacementRectangle };
                HorizontalOffset = value.HorizontalOffset;
                VerticalOffset = value.VerticalOffset;
                HasDropShadow = value.HasDropShadow;
            }
        }

        public void WriteTo(ToolTip value)
        {
            if (value != null)
            {
                WriteTo((Control)value);
                value.IsOpen = isOpen;
                value.StaysOpen = staysOpen;
                value.Placement = placement;
                value.PlacementRectangle = placementRectangle?.Rect ?? default;
                value.HorizontalOffset = horizontalOffset;
                value.VerticalOffset = verticalOffset;
                value.HasDropShadow = hasDropShadow;
            }
        }
    }
}
