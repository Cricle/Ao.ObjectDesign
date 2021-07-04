using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(UIElement))]
    public class UIElementSetting : NotifyableObject, IMiddlewareDesigner<UIElement>
    {
        private string uid;
        private Visibility visibility;
        private bool clipToBounds;
        private bool snapsToDevicePixels;
        private bool isEnabled;
        private bool isHitTestVisible;
        private bool isManipulationEnabled;
        private bool focusable;
        private double opacity;
        private string tag;
        private bool allowDrop;
        private BrushDesigner opacityMask;
        private PointDesigner renderTransformOrigin;

        public virtual PointDesigner RenderTransformOrigin
        {
            get => renderTransformOrigin;
            set => Set(ref renderTransformOrigin, value);
        }

        public virtual BrushDesigner OpacityMask
        {
            get => opacityMask;
            set => Set(ref opacityMask, value);
        }

        public virtual bool AllowDrop
        {
            get => allowDrop;
            set => Set(ref allowDrop, value);
        }

        [TransferOrigin(typeof(object))]
        public virtual string Tag
        {
            get => tag;
            set => Set(ref tag, value);
        }

        public virtual string Uid
        {
            get => uid;
            set => Set(ref uid, value);
        }

        public virtual double Opacity
        {
            get => opacity;
            set => Set(ref opacity, value);
        }

        public virtual bool Focusable
        {
            get => focusable;
            set => Set(ref focusable, value);
        }

        public virtual bool IsManipulationEnabled
        {
            get => isManipulationEnabled;
            set => Set(ref isManipulationEnabled, value);
        }

        public virtual bool IsHitTestVisible
        {
            get => isHitTestVisible;
            set => Set(ref isHitTestVisible, value);
        }

        public virtual bool IsEnabled
        {
            get => isEnabled;
            set => Set(ref isEnabled, value);
        }

        public virtual bool SnapsToDevicePixels
        {
            get => snapsToDevicePixels;
            set => Set(ref snapsToDevicePixels, value);
        }

        public virtual bool ClipToBounds
        {
            get => clipToBounds;
            set => Set(ref clipToBounds, value);
        }

        public virtual Visibility Visibility
        {
            get => visibility;
            set => Set(ref visibility, value);
        }

        public virtual void Apply(UIElement value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Uid = value.Uid;
                Visibility = value.Visibility;
                ClipToBounds = value.ClipToBounds;
                SnapsToDevicePixels = value.SnapsToDevicePixels;
                IsEnabled = value.IsEnabled;
                IsHitTestVisible = value.IsHitTestVisible;
                IsManipulationEnabled = value.IsManipulationEnabled;
                Focusable = value.Focusable;
                AllowDrop = value.AllowDrop;
                Opacity = value.Opacity;
                if (opacityMask is null)
                {
                    opacityMask = new BrushDesigner { Brush = value.OpacityMask };
                }
                else
                {
                    opacityMask.Brush = value.OpacityMask;
                }
                if (renderTransformOrigin is null)
                {
                    RenderTransformOrigin = new PointDesigner { Point = value.RenderTransformOrigin };
                }
                else
                {
                    renderTransformOrigin.Point = value.RenderTransformOrigin;
                }
            }
        }

        public virtual void SetDefault()
        {
            Uid = null;
            Visibility = Visibility.Visible;
            ClipToBounds = false;
            SnapsToDevicePixels = false;
            IsEnabled = true;
            IsHitTestVisible = true;
            IsManipulationEnabled = false;
            Focusable = false;
            AllowDrop = false;
            Opacity = 1;
            OpacityMask = new BrushDesigner();
            RenderTransformOrigin = new PointDesigner();
        }

        public virtual void WriteTo(UIElement value)
        {
            if (value != null)
            {
                value.Uid = uid;
                value.Visibility = visibility;
                value.ClipToBounds = clipToBounds;
                value.SnapsToDevicePixels = snapsToDevicePixels;
                value.IsEnabled = isEnabled;
                value.IsHitTestVisible = isHitTestVisible;
                value.IsManipulationEnabled = isManipulationEnabled;
                value.Focusable = focusable;
                value.AllowDrop = allowDrop;
                value.Opacity = opacity;
                value.OpacityMask = opacityMask?.Brush;
                value.RenderTransformOrigin = renderTransformOrigin?.Point ?? default;
            }
        }
    }
}
