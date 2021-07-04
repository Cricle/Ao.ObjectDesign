using Ao.ObjectDesign.Wpf.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(Window))]
    public class WindowSetting : ControlSetting
    {
        private bool showInTaskbar;
        private WindowStartupLocation windowStartupLocation;
        private double top;
        private bool sizeToContent;
        private bool allowsTransparency;
        private double left;
        private ImageSourceDesigner icon;
        private WindowStyle windowStyle;
        private WindowState windowState;
        private ResizeMode resizeMode;
        private bool topmost;
        private bool showActivated;
        private string title;

        public virtual string Title
        {
            get => title;
            set => Set(ref title, value);
        }

        public virtual bool ShowActivated
        {
            get => showActivated;
            set => Set(ref showActivated, value);
        }

        public virtual bool Topmost
        {
            get => topmost;
            set => Set(ref topmost, value);
        }

        public virtual ResizeMode ResizeMode
        {
            get => resizeMode;
            set => Set(ref resizeMode, value);
        }

        public virtual WindowState WindowState
        {
            get => windowState;
            set => Set(ref windowState, value);
        }

        public virtual WindowStyle WindowStyle
        {
            get => windowStyle;
            set => Set(ref windowStyle, value);
        }

        public virtual ImageSourceDesigner Icon
        {
            get => icon;
            set => Set(ref icon, value);
        }

        public virtual double Left
        {
            get => left;
            set => Set(ref left, value);
        }

        public virtual bool AllowsTransparency
        {
            get => allowsTransparency;
            set => Set(ref allowsTransparency, value);
        }

        public virtual bool SizeToContent
        {
            get => sizeToContent;
            set => Set(ref sizeToContent, value);
        }

        public virtual double Top
        {
            get => top;
            set => Set(ref top, value);
        }

        public virtual WindowStartupLocation WindowStartupLocation
        {
            get => windowStartupLocation;
            set => Set(ref windowStartupLocation, value);
        }

        public virtual bool ShowInTaskbar
        {
            get => showInTaskbar;
            set => Set(ref showInTaskbar, value);
        }
    }
}
