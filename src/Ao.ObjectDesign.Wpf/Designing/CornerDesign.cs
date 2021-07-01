using Ao.ObjectDesign.Wpf.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignForAttribute(typeof(CornerRadius))]
    public class CornerDesign : NotifyableObject
    {
        private double left;
        private double top;
        private double right;
        private double bottom;
        private bool isSolid;
        private double borderWidth = 1;
        private Color borderColor = Colors.Black;

        public virtual double BorderWidth
        {
            get => borderWidth;
            set
            {
                Set(ref borderWidth, value);
                RaiseBorderMarginChanged();
            }
        }
        public virtual Thickness BorderMargin
        {
            get => new Thickness(borderWidth);
            set
            {
                borderWidth = value.Left;
                RaiseBorderMarginChanged();
            }
        }

        public virtual Color BorderColor
        {
            get => borderColor;
            set => Set(ref borderColor, value);
        }

        public virtual bool IsSolid
        {
            get => isSolid;
            set
            {
                Set(ref isSolid, value);
                RaiseCornerRadiusChanged();
            }
        }

        public virtual double Left
        {
            get => left;
            set
            {
                Set(ref left, value);
                RaiseCornerRadiusChanged();
            }
        }
        public virtual double Top
        {
            get => top;
            set
            {
                Set(ref top, value);
                RaiseCornerRadiusChanged();
            }
        }
        public virtual double Right
        {
            get => right;
            set
            {
                Set(ref right, value);
                RaiseCornerRadiusChanged();
            }
        }
        public virtual double Bottom
        {
            get => bottom;
            set
            {
                Set(ref bottom, value);
                RaiseCornerRadiusChanged();
            }
        }

        [PlatformTargetProperty]
        public virtual CornerRadius CornerRadius
        {
            get => new CornerRadius(left, top, right, bottom);
            set
            {
                Left = value.TopLeft;
                Top = value.TopRight;
                Right = value.TopRight;
                Bottom = value.BottomRight;
                RaiseCornerRadiusChanged();
            }
        }
        private void RaiseCornerRadiusChanged()
        {
            RaisePropertyChanged(nameof(CornerRadius));
        }
        private void RaiseBorderMarginChanged()
        {
            RaisePropertyChanged(nameof(BorderMargin));
        }
    }
}
