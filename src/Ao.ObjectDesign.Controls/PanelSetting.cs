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
    [MappingFor(typeof(Panel))]
    public class PanelSetting : FrameworkElementSetting,IMiddlewareDesigner<Panel>
    {
        private bool isItemsHost;
        private BrushDesigner background;

        public virtual BrushDesigner Background
        {
            get => background;
            set => Set(ref background, value);
        }
        public virtual bool IsItemsHost
        {
            get => isItemsHost;
            set => Set(ref isItemsHost, value);
        }
        public override void SetDefault()
        {
            base.SetDefault();
            IsItemsHost = false;
            Background = new BrushDesigner();
        }
        public void Apply(Panel value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((FrameworkElement)value);
                IsItemsHost = value.IsItemsHost;
                Background = new  BrushDesigner{Brush= value.Background };

            }
        }

        public void WriteTo(Panel value)
        {
            if (value!=null)
            {
                WriteTo((FrameworkElement)value);
                value.IsItemsHost = isItemsHost;
                value.Background = background?.Brush;
            }
        }
    }
}
