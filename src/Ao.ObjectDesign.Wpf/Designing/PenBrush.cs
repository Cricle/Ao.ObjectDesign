using Ao.ObjectDesign.Wpf.Annotations;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(Brush))]
    public class PenBrush : NotifyableObject
    {
        private PenBrushTypes type;

        private PenSolidSetting solidSetting;
        private PenLinerSetting linerSetting;
        private PenRadialSetting radialSetting;

        public virtual PenRadialSetting RadialSetting
        {
            get => radialSetting;
            set
            {
                if (radialSetting !=null)
                {
                    radialSetting.PropertyChanged -= OnPropertyChanged;
                }
                if (value!=null)
                {
                    value.PropertyChanged += OnPropertyChanged;
                }
                Set(ref radialSetting, value);
            }
        }



        public virtual PenLinerSetting LinerSetting
        {
            get => linerSetting;
            set
            {
                if (linerSetting != null)
                {
                    linerSetting.PropertyChanged -= OnPropertyChanged;
                }
                if (value != null)
                {
                    value.PropertyChanged += OnPropertyChanged;
                }
                Set(ref linerSetting, value);
            }
        }

        public virtual PenSolidSetting SolidSetting
        {
            get => solidSetting;
            set
            {
                if (solidSetting != null)
                {
                    solidSetting.PropertyChanged -= OnPropertyChanged;
                }
                if (value != null)
                {
                    value.PropertyChanged += OnPropertyChanged;
                }
                Set(ref solidSetting, value);
            }
        }
        private Brush brush;
        [PlatformTargetProperty]
        public virtual Brush Brush
        {
            get
            {
                if (Type == PenBrushTypes.None)
                {
                    brush = null;
                }
                else if (Type == PenBrushTypes.Solid)
                {
                    var ss = SolidSetting;
                    if (ss is null || ss.Color is null)
                    {
                        return null;
                    }
                    if (brush is SolidColorBrush sbrush)
                    {
                        sbrush.Color = ss.Color.Value;
                    }
                    else
                    {
                        brush = new SolidColorBrush(ss.Color.Value);
                    }
                }
                else if (Type == PenBrushTypes.Liner)
                {
                    var ls = LinerSetting;
                    if (ls is null)
                    {
                        brush = null;
                    }
                    if (!(brush is LinearGradientBrush b))
                    {
                        b = new LinearGradientBrush();
                    }
                    b.StartPoint = ls.StartPoint;
                    b.EndPoint = ls.EndPoint;
                    b.ColorInterpolationMode = ls.ColorInterpolationMode;
                    b.MappingMode = ls.MappingMode;
                    b.Opacity = ls.Opacity;
                    b.GradientStops = new GradientStopCollection(ls.GradientStops);
                    brush = b;
                }
                else if (Type == PenBrushTypes.Radial)
                {
                    var rs = RadialSetting;
                    if (rs is null)
                    {
                        brush = null;
                    }
                    else
                    {
                        if (!(brush is RadialGradientBrush b))
                        {
                            b = new RadialGradientBrush();
                        }
                        b.RadiusX = rs.RadiusX;
                        b.RadiusY = rs.RadiusY;
                        b.Center = rs.Center;
                        b.ColorInterpolationMode = rs.ColorInterpolationMode;
                        b.GradientOrigin = rs.GradientOrigin;
                        b.MappingMode = rs.MappingMode;
                        b.SpreadMethod = rs.SpreadMethod;
                        b.Opacity = rs.Opacity;
                        b.GradientStops = new GradientStopCollection(rs.GradientStops);
                        brush = b;
                    }
                }
                return brush;
            }
            set
            {
                if (value is null)
                {
                    Type = PenBrushTypes.None;
                }
                else if(value is SolidColorBrush scb)
                {
                    Type = PenBrushTypes.Solid;
                    SolidSetting.Color = scb.Color;
                }
                else if (value is LinearGradientBrush lgb)
                {
                    Type = PenBrushTypes.Liner;
                    LinerSetting.StartPoint = lgb.StartPoint;
                    LinerSetting.EndPoint = lgb.EndPoint;
                    LinerSetting.MappingMode = lgb.MappingMode;
                    LinerSetting.ColorInterpolationMode = lgb.ColorInterpolationMode;
                    LinerSetting.Opacity = lgb.Opacity;
                    LinerSetting.SpreadMethod = lgb.SpreadMethod;
                    LinerSetting.PenGradientStops.Clear();
                    if (lgb.GradientStops != null)
                    {
                        LinerSetting.PenGradientStops.AddRange(lgb.GradientStops.Select(x => new PenGradientStop(x.Color, x.Offset)));
                    }
                }
                else if (value is RadialGradientBrush rgb)
                {
                    Type = PenBrushTypes.Radial;
                    RadialSetting.Center = rgb.GradientOrigin;
                    RadialSetting.ColorInterpolationMode = rgb.ColorInterpolationMode;
                    RadialSetting.GradientOrigin = rgb.GradientOrigin;
                    RadialSetting.MappingMode = rgb.MappingMode;
                    RadialSetting.Opacity = rgb.Opacity;
                    RadialSetting.RadiusX = rgb.RadiusX;
                    RadialSetting.RadiusY = rgb.RadiusY;
                    RadialSetting.SpreadMethod = rgb.SpreadMethod;
                    RadialSetting.PenGradientStops.Clear();
                    if (rgb.GradientStops != null)
                    {
                        RadialSetting.PenGradientStops.AddRange(rgb.GradientStops.Select(x => new PenGradientStop(x.Color, x.Offset)));
                    }
                }
                else
                {
                    Type = PenBrushTypes.None;
                }
            }
        }

        public virtual PenBrushTypes Type
        {
            get => type;
            set
            {
                if (value == PenBrushTypes.Liner)
                {
                    if (LinerSetting is null)
                    {
                        LinerSetting = new PenLinerSetting();
                    }
                }
                else if (value == PenBrushTypes.Radial)
                {
                    if (RadialSetting is null)
                    {
                        RadialSetting = new PenRadialSetting();
                    }
                }
                else if (value == PenBrushTypes.Solid)
                {
                    if (SolidSetting is null)
                    {
                        SolidSetting = new PenSolidSetting();
                    }
                }
                Set(ref type, value);
                RaiseBrushChanged();
            }
        }
        public void RaiseBrushChanged()
        {
            RaisePropertyChanged(nameof(Brush));
        }
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaiseBrushChanged();
        }
    }
}
