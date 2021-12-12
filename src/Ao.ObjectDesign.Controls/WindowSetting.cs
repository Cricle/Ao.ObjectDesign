using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(Window))]
    public class WindowSetting : ControlSetting, IMiddlewareDesigner<Window>
    {
        private bool showInTaskbar;
        private WindowStartupLocation windowStartupLocation;
        private double top;
        private SizeToContent sizeToContent;
        private bool allowsTransparency;
        private double left;
        private ImageSourceDesigner icon;
        private WindowStyle windowStyle;
        private WindowState windowState;
        private ResizeMode resizeMode;
        private bool topmost;
        private bool showActivated;
        private string title;

        [DefaultValue(null)]
        public virtual string Title
        {
            get => title;
            set => Set(ref title, value);
        }

        [DefaultValue(true)]
        public virtual bool ShowActivated
        {
            get => showActivated;
            set => Set(ref showActivated, value);
        }

        [DefaultValue(false)]
        public virtual bool Topmost
        {
            get => topmost;
            set => Set(ref topmost, value);
        }

        [DefaultValue(ResizeMode.CanResize)]
        public virtual ResizeMode ResizeMode
        {
            get => resizeMode;
            set => Set(ref resizeMode, value);
        }

        [DefaultValue(WindowState.Normal)]
        public virtual WindowState WindowState
        {
            get => windowState;
            set => Set(ref windowState, value);
        }

        [DefaultValue(WindowStyle.None)]
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

        [DefaultValue(double.NaN)]
        public virtual double Left
        {
            get => left;
            set => Set(ref left, value);
        }

        [DefaultValue(false)]
        public virtual bool AllowsTransparency
        {
            get => allowsTransparency;
            set => Set(ref allowsTransparency, value);
        }

        [DefaultValue(SizeToContent.Manual)]
        public virtual SizeToContent SizeToContent
        {
            get => sizeToContent;
            set => Set(ref sizeToContent, value);
        }

        [DefaultValue(double.NaN)]
        public virtual double Top
        {
            get => top;
            set => Set(ref top, value);
        }

        [DefaultValue(WindowStartupLocation.Manual)]
        public virtual WindowStartupLocation WindowStartupLocation
        {
            get => windowStartupLocation;
            set => Set(ref windowStartupLocation, value);
        }

        [DefaultValue(true)]
        public virtual bool ShowInTaskbar
        {
            get => showInTaskbar;
            set => Set(ref showInTaskbar, value);
        }
        public override void SetDefault()
        {
            base.SetDefault();
            ShowInTaskbar = true;
            WindowStartupLocation = WindowStartupLocation.Manual;
            Top = double.NaN;
            SizeToContent = SizeToContent.Manual;
            AllowsTransparency = false;
            Left = double.NaN;
            Icon = new ImageSourceDesigner();
            WindowStyle = WindowStyle.SingleBorderWindow;
            WindowState = WindowState.Normal;
            ResizeMode = ResizeMode.CanResize;
            Topmost = false;
            ShowActivated = true;
            Title = null;
        }
        public void Apply(Window value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((Control)value);
                ShowInTaskbar = value.ShowInTaskbar;
                WindowStartupLocation = value.WindowStartupLocation;
                Top = value.Top;
                SizeToContent = value.SizeToContent;
                AllowsTransparency = value.AllowsTransparency;
                Left = value.Left;
                Icon = new ImageSourceDesigner ();
                Icon.SetImageSource(value.Icon);
                WindowStyle = value.WindowStyle;
                WindowState = value.WindowState;
                ResizeMode = value.ResizeMode;
                Topmost = value.Topmost;
                ShowActivated = value.ShowActivated;
                Title = value.Title;
            }
        }

        public void WriteTo(Window value)
        {
            if (value != null)
            {
                WriteTo((Control)value);
                value.ShowInTaskbar = showInTaskbar;
                value.WindowStartupLocation = windowStartupLocation;
                value.Top = top;
                value.SizeToContent = sizeToContent;
                value.AllowsTransparency = allowsTransparency;
                value.Left = left;
                value.Icon = icon?.GetImageSource();
                value.WindowStyle = windowStyle;
                value.WindowState = windowState;
                value.ResizeMode = resizeMode;
                value.Topmost = topmost;
                value.ShowActivated = showActivated;
                value.Title = title;
            }
        }
    }
}
