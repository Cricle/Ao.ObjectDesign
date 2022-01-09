using System.Windows.Controls;

namespace ObjectDesign.Brock.Controls
{


    public class ProgressBarSetting : RangeBaseSetting
    {
        private bool isIndeterminate;
        private Orientation orientation;

        public bool IsIndeterminate
        {
            get => isIndeterminate;
            set => Set(ref isIndeterminate, value);
        }
        public Orientation Orientation
        {
            get => orientation;
            set => Set(ref orientation, value);
        }
        public override void SetDefault()
        {
            base.SetDefault();
            IsIndeterminate = false;
            Orientation = Orientation.Horizontal;
        }
    }
}
