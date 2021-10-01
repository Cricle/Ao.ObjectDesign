using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(RowDefinition))]
    public class RowDefinitionDesigner : NotifyableObject
    {
        private GridLengthDesigner gridLengthSetting;
        private double minHeight;
        private double maxHeight = double.PositiveInfinity;

        public virtual GridLengthDesigner GridLengthSetting
        {
            get => gridLengthSetting;
            set
            {
                if (gridLengthSetting != null)
                {
                    gridLengthSetting.PropertyChanged -= OnGridLengthSettingPropertyChanged;
                }
                if (value != null)
                {
                    value.PropertyChanged += OnGridLengthSettingPropertyChanged;
                }
                Set(ref gridLengthSetting, value);
                RaiseRowDefinitionChanged();
            }
        }

        private void OnGridLengthSettingPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaiseRowDefinitionChanged();
        }

        [DefaultValue(double.PositiveInfinity)]
        public virtual double MaxHeight
        {
            get => maxHeight;
            set
            {
                Set(ref maxHeight, value);
                RaiseRowDefinitionChanged();
            }
        }


        [DefaultValue(0d)]
        public virtual double MinHeight
        {
            get => minHeight;
            set
            {
                Set(ref minHeight, value);
                RaiseRowDefinitionChanged();
            }
        }
        [PlatformTargetProperty]
        public virtual RowDefinition RowDefinition
        {
            get
            {
                GridLength length = default;
                if (gridLengthSetting != null)
                {
                    length = gridLengthSetting.GridLength;
                }
                return new RowDefinition
                {
                    Height = length,
                    MaxHeight = maxHeight,
                    MinHeight = minHeight
                };
            }
            set
            {
                if (value is null)
                {
                    GridLengthSetting = null;
                }
                else
                {
                    MaxHeight = value.MaxHeight;
                    MinHeight = value.MinHeight;
                    if (GridLengthSetting is null)
                    {
                        GridLengthSetting = new GridLengthDesigner();
                    }
                    GridLengthSetting.Type = value.Height.GridUnitType;
                    GridLengthSetting.Value = value.Height.Value;
                }
            }
        }
        private static readonly PropertyChangedEventArgs rowDefinitionEventArgs = new PropertyChangedEventArgs(nameof(RowDefinition));
        protected void RaiseRowDefinitionChanged()
        {
            RaisePropertyChanged(rowDefinitionEventArgs);
        }
    }
}
