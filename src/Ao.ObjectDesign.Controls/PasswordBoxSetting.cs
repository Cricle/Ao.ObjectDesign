using Ao.ObjectDesign.Wpf.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(PasswordBox))]
    public class PasswordBoxSetting : ControlSetting, IMiddlewareDesigner<PasswordBox>
    {
        private BrushDesigner caretBrush;
        private double selectionOpacity;
        private BrushDesigner selectionBrush;
        private int maxLength;
        private char passwordChar;
        private bool isInactiveSelectionHighlightEnabled;

        public virtual bool IsInactiveSelectionHighlightEnabled
        {
            get => isInactiveSelectionHighlightEnabled;
            set => Set(ref isInactiveSelectionHighlightEnabled, value);
        }

        public virtual char PasswordChar
        {
            get => passwordChar;
            set => Set(ref passwordChar, value);
        }

        public virtual int MaxLength
        {
            get => maxLength;
            set => Set(ref maxLength, value);
        }

        public virtual BrushDesigner SelectionBrush
        {
            get => selectionBrush;
            set => Set(ref selectionBrush, value);
        }

        public virtual double SelectionOpacity
        {
            get => selectionOpacity;
            set => Set(ref selectionOpacity, value);
        }

        public virtual BrushDesigner CaretBrush
        {
            get => caretBrush;
            set => Set(ref caretBrush, value);
        }

        public override void SetDefault()
        {
            base.SetDefault();
            CaretBrush = new BrushDesigner();
            SelectionOpacity = 0.4;
            SelectionBrush = new BrushDesigner();
            MaxLength = 0;
            PasswordChar = '●';
            IsInactiveSelectionHighlightEnabled = false;
        }

        public void Apply(PasswordBox value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((Control)value);
                CaretBrush = new BrushDesigner { Brush = value.CaretBrush };
                SelectionOpacity = value.SelectionOpacity;
                SelectionBrush = new BrushDesigner { Brush = value.SelectionBrush };
                MaxLength = value.MaxLength;
                PasswordChar = value.PasswordChar;
                IsInactiveSelectionHighlightEnabled = value.IsInactiveSelectionHighlightEnabled;
            }
        }

        public void WriteTo(PasswordBox value)
        {
            if (value != null)
            {
                WriteTo((Control)value);
                value.CaretBrush = caretBrush?.Brush;
                value.SelectionOpacity = selectionOpacity;
                value.SelectionBrush = selectionBrush?.Brush;
                value.MaxLength = maxLength;
                value.PasswordChar = passwordChar;
                value.IsInactiveSelectionHighlightEnabled = isInactiveSelectionHighlightEnabled;

            }
        }
    }
}
