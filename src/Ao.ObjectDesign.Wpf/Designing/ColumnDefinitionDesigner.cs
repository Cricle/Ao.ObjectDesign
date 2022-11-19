using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Designing
{
    [DesignFor(typeof(ColumnDefinition))]
    public class ColumnDefinitionDesigner : NotifyableObject
    {
        private GridLengthDesigner gridLengthSetting;
        private double minWidth;
        private double maxWidth = double.PositiveInfinity;

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
                RaiseColumnDefinitionChanged();
            }
        }

        private void OnGridLengthSettingPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaiseColumnDefinitionChanged();
        }

        [DefaultValue(double.PositiveInfinity)]
        public virtual double MaxWidth
        {
            get => maxWidth;
            set
            {
                Set(ref maxWidth, value);
                RaiseColumnDefinitionChanged();
            }
        }

        [DefaultValue(0d)]
        public virtual double MinWidth
        {
            get => minWidth;
            set
            {
                Set(ref minWidth, value);
                RaiseColumnDefinitionChanged();
            }
        }
        [PlatformTargetGetMethod]
        public virtual ColumnDefinition GetColumnDefinition()
        {
            GridLength length = default;
            if (gridLengthSetting != null)
            {
                length = gridLengthSetting.GetGridLength();
            }
            return new ColumnDefinition
            {
                Width = length,
                MaxWidth = maxWidth,
                MinWidth = minWidth
            };
        }
        [PlatformTargetSetMethod]
        public virtual void SetColumnDefinition(ColumnDefinition value)
        {
            if (value is null)
            {
                GridLengthSetting = null;
            }
            else
            {
                MaxWidth = value.MaxWidth;
                MinWidth = value.MinWidth;
                if (GridLengthSetting is null)
                {
                    GridLengthSetting = new GridLengthDesigner();
                }
                GridLengthSetting.Type = value.Width.GridUnitType;
                GridLengthSetting.Value = value.Width.Value;
            }
        }

        private static readonly PropertyChangedEventArgs columnDefinitionChangedEventArgs = new PropertyChangedEventArgs(nameof(ColumnDefinition));
        protected void RaiseColumnDefinitionChanged()
        {
            RaisePropertyChanged(columnDefinitionChangedEventArgs);
        }
    }
}
