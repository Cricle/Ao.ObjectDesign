using Ao.ObjectDesign.Wpf.Annotations;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(GridLength))]
    public class GridLengthSetting : NotifyableObject
    {
        private GridUnitType type;
        private double value = 1;

        public virtual double Value
        {
            get => value;
            set
            {
                Set(ref value, value);
                RaiseGridLengthChanged();
            }
        }
        public virtual GridUnitType Type
        {
            get => type;
            set
            {
                Set(ref type, value);
                RaiseGridLengthChanged();
            }
        }
        [PlatformTargetProperty]
        public virtual GridLength GridLength
        {
            get
            {
                if (type == GridUnitType.Auto)
                {
                    return GridLength.Auto;
                }
                return new GridLength(value, type);
            }
            set
            {
                Type = value.GridUnitType;
                Value = value.Value;
            }
        }

        private void RaiseGridLengthChanged()
        {
            RaisePropertyChanged(nameof(GridLength));
        }
    }
}
