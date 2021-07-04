using Ao.ObjectDesign.Wpf.Annotations;
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
    [MappingFor(typeof(Viewbox))]
    public class ViewboxSetting : FrameworkElementSetting,IMiddlewareDesigner<Viewbox>
    {
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

        public override void SetDefault()
        {
            base.SetDefault();
            Stretch = Stretch.None;
            StretchDirection = StretchDirection.Both;
        }

        public void Apply(Viewbox value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((FrameworkElement)value);
                Stretch = value.Stretch;
                StretchDirection = value.StretchDirection;
            }
        }

        public void WriteTo(Viewbox value)
        {
            if (value != null)
            {
                WriteTo((FrameworkElement)value);
                value.Stretch = Stretch;
                value.StretchDirection = StretchDirection;
            }
        }
    }
}
