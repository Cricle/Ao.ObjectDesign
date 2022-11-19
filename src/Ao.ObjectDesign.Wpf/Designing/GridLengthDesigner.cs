using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows;

namespace Ao.ObjectDesign.Designing
{
    [DesignFor(typeof(GridLength))]
    public class GridLengthDesigner : NotifyableObject
    {
        private static readonly PropertyChangedEventArgs gridLengthChangedEventArgs = new PropertyChangedEventArgs(nameof(GridLength));
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
        [PlatformTargetGetMethod]
        public virtual GridLength GetGridLength()
        {
            if (type == GridUnitType.Auto)
            {
                return GridLength.Auto;
            }
            return new GridLength(value, type);
        }
        [PlatformTargetSetMethod]
        public virtual void SetGridLength(GridLength value)
        {
            Type = value.GridUnitType;
            Value = value.Value;
        }

        protected void RaiseGridLengthChanged()
        {
            RaisePropertyChanged(gridLengthChangedEventArgs);
        }
    }
}
