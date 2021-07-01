using Ao.ObjectDesign.Wpf.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(ColumnDefinition))]
    public class ColumnDefinitionSetting : NotifyableObject
    {
        private GridLengthSetting gridLengthSetting;
        private double minWidth;
        private double maxWidth = double.PositiveInfinity;

        public virtual GridLengthSetting GridLengthSetting
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
                    gridLengthSetting.PropertyChanged += OnGridLengthSettingPropertyChanged;
                }
                Set(ref gridLengthSetting, value);
                RaiseColumnDefinitionChanged();
            }
        }

        private void OnGridLengthSettingPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaiseColumnDefinitionChanged();
        }

        public virtual double MaxWidth
        {
            get => maxWidth;
            set
            {
                Set(ref maxWidth, value);
                RaiseColumnDefinitionChanged();
            }
        }


        public virtual double MinWidth
        {
            get => minWidth;
            set
            {
                Set(ref minWidth, value);
                RaiseColumnDefinitionChanged();
            }
        }
        [PlatformTargetProperty]
        public virtual ColumnDefinition ColumnDefinition
        {
            get
            {
                GridLength length = default;
                if (gridLengthSetting != null)
                {
                    length = gridLengthSetting.GridLength;
                }
                return new ColumnDefinition
                {
                    Width = length,
                    MaxWidth = maxWidth,
                    MinWidth = minWidth
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
                    MaxWidth = value.MaxWidth;
                    MinWidth = value.MinWidth;
                    if (GridLengthSetting is null)
                    {
                        GridLengthSetting = new GridLengthSetting();
                    }
                    GridLengthSetting.Type = value.Width.GridUnitType;
                    GridLengthSetting.Value = value.Width.Value;
                }
            }
        }
        private void RaiseColumnDefinitionChanged()
        {
            RaisePropertyChanged(nameof(ColumnDefinition));
        }
    }
}
