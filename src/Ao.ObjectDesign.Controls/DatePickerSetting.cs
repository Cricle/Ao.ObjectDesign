using Ao.ObjectDesign.Wpf.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(DatePicker))]
    public class DatePickerSetting : FrameworkElementSetting, IMiddlewareDesigner<DatePicker>
    {
        private DateTime? displayDateStart;
        private DateTime? displayDateEnd;
        private bool isTodayHighlighted;
        private bool isDropDownOpen;
        private DatePickerFormat selectedDateFormat;
        private string text;
        private DayOfWeek firstDayOfWeek;
        private DateTime? selectedDate;

        [DefaultValue(null)]
        public virtual DateTime? SelectedDate
        {
            get => selectedDate;
            set => Set(ref selectedDate, value);
        }

        [DefaultValue(DayOfWeek.Sunday)]
        public virtual DayOfWeek FirstDayOfWeek
        {
            get => firstDayOfWeek;
            set => Set(ref firstDayOfWeek, value);
        }

        [DefaultValue(null)]
        public virtual string Text
        {
            get => text;
            set => Set(ref text, value);
        }

        [DefaultValue(DatePickerFormat.Long)]
        public virtual DatePickerFormat SelectedDateFormat
        {
            get => selectedDateFormat;
            set => Set(ref selectedDateFormat, value);
        }

        [DefaultValue(false)]
        public virtual bool IsDropDownOpen
        {
            get => isDropDownOpen;
            set => Set(ref isDropDownOpen, value);
        }

        [DefaultValue(true)]
        public virtual bool IsTodayHighlighted
        {
            get => isTodayHighlighted;
            set => Set(ref isTodayHighlighted, value);
        }

        [DefaultValue(null)]
        public virtual DateTime? DisplayDateEnd
        {
            get => displayDateEnd;
            set => Set(ref displayDateEnd, value);
        }

        [DefaultValue(null)]
        public virtual DateTime? DisplayDateStart
        {
            get => displayDateStart;
            set => Set(ref displayDateStart, value);
        }
        public override void SetDefault()
        {
            base.SetDefault();
            DisplayDateStart = DisplayDateEnd = null;
            Text = null;
            SelectedDateFormat = DatePickerFormat.Long;
            FirstDayOfWeek = DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek;
            SelectedDate = null;
            IsTodayHighlighted = true;
            IsDropDownOpen = false;
        }

        public void Apply(DatePicker value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((FrameworkElement)value);
                DisplayDateStart = value.DisplayDateStart;
                DisplayDateEnd = value.DisplayDateEnd;
                Text = value.Text;
                SelectedDateFormat = value.SelectedDateFormat;
                FirstDayOfWeek = value.FirstDayOfWeek;
                SelectedDate = value.SelectedDate;
                IsTodayHighlighted = value.IsTodayHighlighted;
                IsDropDownOpen = value.IsDropDownOpen;
            }
        }

        public void WriteTo(DatePicker value)
        {
            if (value != null)
            {
                WriteTo((FrameworkElement)value);
                value.DisplayDateStart = DisplayDateStart;
                value.DisplayDateEnd = DisplayDateEnd;
                value.Text = Text;
                value.SelectedDateFormat = SelectedDateFormat;
                value.FirstDayOfWeek = FirstDayOfWeek;
                value.SelectedDate = SelectedDate;
                value.IsTodayHighlighted = IsTodayHighlighted;
                value.IsDropDownOpen = IsDropDownOpen;
            }
        }
    }
}
