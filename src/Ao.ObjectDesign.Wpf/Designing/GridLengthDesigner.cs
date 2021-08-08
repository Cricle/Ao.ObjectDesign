using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(GridLength))]
    public class GridLengthDesigner : NotifyableObject
    {
        private GridUnitType type;
        private double value = 1;

        [DefaultValue(1d)]
        public virtual double Value
        {
            get => value;
            set
            {
                Set(ref this.value, value);
                RaiseGridLengthChanged();
            }
        }
        [DefaultValue(GridUnitType.Star)]
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

        protected void RaiseGridLengthChanged()
        {
            RaisePropertyChanged(nameof(GridLength));
        }
    }
}
