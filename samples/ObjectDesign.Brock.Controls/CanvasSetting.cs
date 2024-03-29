﻿using Ao.ObjectDesign.Designing.Annotations;
using System.Windows.Controls;
using System.Windows.Media;

namespace ObjectDesign.Brock.Controls
{
    [MappingFor(typeof(Canvas))]
    public class CanvasSetting : PanelSetting
    {
        private BitmapScalingMode bitmapScalingMode;
        private EdgeMode edgeMode;

        public BitmapScalingMode BitmapScalingMode
        {
            get => bitmapScalingMode;
            set => Set(ref bitmapScalingMode, value);
        }

        public EdgeMode EdgeMode
        {
            get => edgeMode;
            set => Set(ref edgeMode, value);
        }

        public override void SetDefault()
        {
            base.SetDefault();
            BitmapScalingMode = BitmapScalingMode.HighQuality;
            EdgeMode = EdgeMode.Unspecified;
        }
    }
}
