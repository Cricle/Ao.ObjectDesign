using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Designing;
using System.ComponentModel;
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

        [DefaultValue(false)]
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

        [DefaultValue(false)]
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

        [DefaultValue(false)]
        public virtual bool AutoWordSelection
        {
            get => autoWordSelection;
            set => Set(ref autoWordSelection, value);
        }

        [DefaultValue(1)]
        public virtual int UndoLimit
        {
            get => undoLimit;
            set => Set(ref undoLimit, value);
        }

        [DefaultValue(true)]
        public virtual bool IsUndoEnabled
        {
            get => isUndoEnabled;
            set => Set(ref isUndoEnabled, value);
        }

        [DefaultValue(0.4d)]
        public virtual double SelectionOpacity
        {
            get => selectionOpacity;
            set => Set(ref selectionOpacity, value);
        }

        [DefaultValue(ScrollBarVisibility.Hidden)]
        public virtual ScrollBarVisibility HorizontalScrollBarVisibility
        {
            get => horizontalScrollBarVisibility;
            set => Set(ref horizontalScrollBarVisibility, value);
        }

        [DefaultValue(false)]
        public virtual bool IsReadOnlyCaretVisible
        {
            get => isReadOnlyCaretVisible;
            set => Set(ref isReadOnlyCaretVisible, value);
        }

        [DefaultValue(false)]
        public virtual bool AcceptsTab
        {
            get => acceptsTab;
            set => Set(ref acceptsTab, value);
        }
        [DefaultValue(true)]
        public virtual bool AcceptsReturn
        {
            get => acceptsReturn;
            set => Set(ref acceptsReturn, value);
        }

        [DefaultValue(ScrollBarVisibility.Hidden)]
        public virtual ScrollBarVisibility VerticalScrollBarVisibility
        {
            get => verticalScrollBarVisibility;
            set => Set(ref verticalScrollBarVisibility, value);
        }


        public override void SetDefault()
        {
            base.SetDefault();
            IsReadOnlyCaretVisible = false;
            VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            AcceptsReturn = true;
            AcceptsTab = false;
            HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            SelectionOpacity = 0.4;
            IsUndoEnabled = true;
            UndoLimit = 1;
            AutoWordSelection = false;
            SelectionBrush = new BrushDesigner();
            IsReadOnly = false;
            CaretBrush = new BrushDesigner();
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
                SelectionBrush = new BrushDesigner();
                SelectionBrush.SetBrush(value.SelectionBrush);
                IsReadOnly = value.IsReadOnly;
                CaretBrush = new BrushDesigner();
                CaretBrush.SetBrush(value.CaretBrush);
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
                value.SelectionBrush = selectionBrush?.GetBrush();
                value.IsReadOnly = isReadOnly;
                value.CaretBrush = caretBrush?.GetBrush();
                value.IsInactiveSelectionHighlightEnabled = isInactiveSelectionHighlightEnabled;
            }
        }
    }
}
