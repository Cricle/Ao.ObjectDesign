using Ao.ObjectDesign.Wpf.Designing;
using Ao.ObjectDesign.Wpf;
namespace ObjectDesign.Wpf
{
    public class MyControl : NotifyableObject
    {
        private double opacity = 1;

        private FontSetting font = new FontSetting();
        private PenBrush background = new PenBrush();
        private LocationSize size = new LocationSize();
        private CornerDesign cornerDesign = new CornerDesign();

        public FontSetting Font
        {
            get => font;
            set => Set(ref font, value);
        }

        public PenBrush Background
        {
            get => background;
            set => Set(ref background, value);
        }

        public LocationSize Size
        {
            get => size;
            set => Set(ref size, value);
        }

        public CornerDesign CornerDesign
        {
            get => cornerDesign;
            set => Set(ref cornerDesign, value);
        }

        public double Opacity
        {
            get => opacity;
            set => Set(ref opacity, value);
        }
    }
}
