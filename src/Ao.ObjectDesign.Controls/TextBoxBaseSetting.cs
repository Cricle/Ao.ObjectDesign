using Ao.ObjectDesign.Wpf.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(TextBoxBase))]
    public abstract class TextBoxBaseSetting : ControlSetting, IMiddlewareDesigner<TextBoxBase>
    {
        private ScrollBarVisibility verticalScrollBarVisibility = ScrollBarVisibility.Auto;
        private bool acceptsReturn = true;
        private bool acceptsTab = true;
        private bool isReadOnlyCaretVisible;
        private ScrollBarVisibility horizontalScrollBarVisibility = ScrollBarVisibility.Auto;
        private double selectionOpacity = 1;
        private bool isUndoEnabled = true;
        private int undoLimit = 1;
        private bool autoWordSelection = true;
        private BrushDesigner selectionBrush;
        private bool isReadOnly;
        private BrushDesigner caretBrush;
        private bool isInactiveSelectionHighlightEnabled;

        public virtual bool IsInactiveSelectionHighlightEnabled
        {
            get => isInactiveSelectionHighlightEnabled;
            set => Set(ref isInactiveSelectionHighlightEnabled, value);
        }

        public virtual BrushDesigner CaretBrush
        {
            get => caretBrush;
            set => Set(ref caretBrush, value);
        }

        public virtual bool IsReadOnly
        {
            get => isReadOnly;
            set => Set(ref isReadOnly, value);
        }
        public virtual BrushDesigner SelectionBrush
        {
            get => selectionBrush;
            set => Set(ref selectionBrush, value);
        }

        public virtual bool AutoWordSelection
        {
            get => autoWordSelection;
            set => Set(ref autoWordSelection, value);
        }

        public virtual int UndoLimit
        {
            get => undoLimit;
            set => Set(ref undoLimit, value);
        }

        public virtual bool IsUndoEnabled
        {
            get => isUndoEnabled;
            set => Set(ref isUndoEnabled, value);
        }

        public virtual double SelectionOpacity
        {
            get => selectionOpacity;
            set => Set(ref selectionOpacity, value);
        }

        public virtual ScrollBarVisibility HorizontalScrollBarVisibility
        {
            get => horizontalScrollBarVisibility;
            set => Set(ref horizontalScrollBarVisibility, value);
        }

        public virtual bool IsReadOnlyCaretVisible
        {
            get => isReadOnlyCaretVisible;
            set => Set(ref isReadOnlyCaretVisible, value);
        }

        public virtual bool AcceptsTab
        {
            get => acceptsTab;
            set => Set(ref acceptsTab, value);
        }
        public virtual bool AcceptsReturn
        {
            get => acceptsReturn;
            set => Set(ref acceptsReturn, value);
        }

        public virtual ScrollBarVisibility VerticalScrollBarVisibility
        {
            get => verticalScrollBarVisibility;
            set => Set(ref verticalScrollBarVisibility, value);
        }


        public override void SetDefault()
        {
            base.SetDefault();
            VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            AcceptsReturn = true;
            AcceptsTab = false;
            HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            SelectionOpacity = 0.4;
            IsUndoEnabled = true;
            UndoLimit = 1;
            AutoWordSelection = false;
            SelectionBrush = null;
            IsReadOnly = false;
            CaretBrush = null;
            IsInactiveSelectionHighlightEnabled = false;
        }

        public void Apply(TextBoxBase value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((Control)value);
                VerticalScrollBarVisibility = value.VerticalScrollBarVisibility;
                AcceptsReturn = value.AcceptsReturn;
                AcceptsTab = value.AcceptsTab;
                HorizontalScrollBarVisibility = value.HorizontalScrollBarVisibility;
                SelectionOpacity = value.SelectionOpacity;
                IsUndoEnabled = value.IsUndoEnabled;
                UndoLimit = value.UndoLimit;
                AutoWordSelection = value.AutoWordSelection;
                SelectionBrush = new BrushDesigner { Brush = value.SelectionBrush };
                IsReadOnly = value.IsReadOnly;
                CaretBrush = new BrushDesigner { Brush = value.CaretBrush };
                IsInactiveSelectionHighlightEnabled = value.IsInactiveSelectionHighlightEnabled;
            }
        }

        public void WriteTo(TextBoxBase value)
        {
            if (value != null)
            {
                WriteTo((Control)value);
                value.VerticalScrollBarVisibility = verticalScrollBarVisibility;
                value.AcceptsReturn = acceptsReturn;
                value.AcceptsTab = acceptsTab;
                value.HorizontalScrollBarVisibility = horizontalScrollBarVisibility;
                value.SelectionOpacity = selectionOpacity;
                value.IsUndoEnabled = isUndoEnabled;
                value.UndoLimit = undoLimit;
                value.AutoWordSelection = autoWordSelection;
                value.SelectionBrush = selectionBrush?.Brush;
                value.IsReadOnly = isReadOnly;
                value.CaretBrush = caretBrush?.Brush;
                value.IsInactiveSelectionHighlightEnabled = isInactiveSelectionHighlightEnabled;
            }
        }
    }
}
