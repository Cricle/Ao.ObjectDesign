using Ao.ObjectDesign.Designing.Annotations;
using System.Windows.Controls.Primitives;

namespace ObjectDesign.Brock.Controls
{

    [MappingFor(typeof(RangeBase))]
    public abstract class RangeBaseSetting : ControlSetting
    {
        private double value;
        private double minimum;
        private double maximum;
        private double smallChange;
        private double largeChange;

        public double LargeChange
        {
            get => largeChange;
            set => Set(ref largeChange, value);
        }
        public double SmallChange
        {
            get => smallChange;
            set => Set(ref smallChange, value);
        }
        public double Maximum
        {
            get => maximum;
            set => Set(ref maximum, value);
        }
        public double Minimum
        {
            get => minimum;
            set => Set(ref minimum, value);
        }

        public double Value
        {
            get => value;
            set => Set(ref this.value, value);
        }
        public override void SetDefault()
        {
            base.SetDefault();
            SmallChange = 0.1;
            LargeChange = 1;
            Minimum = 0;
            Maximum = 100;
            Value = 0;
        }
    }
}
