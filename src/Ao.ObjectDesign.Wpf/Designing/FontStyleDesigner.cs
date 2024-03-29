﻿using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System.ComponentModel;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(FontStyle))]
    public class FontStyleDesigner : NotifyableObject
    {
        private static readonly PropertyChangedEventArgs fontStyleChangedEventArgs = new PropertyChangedEventArgs(nameof(FontStyle));
        private bool isItalic;
        private bool isUnerline;

        [PlatformTargetGetMethod]
        public virtual FontStyle GetFontStyle()
        {
            return IsItalic ? FontStyles.Italic : FontStyles.Normal;
        }
        [PlatformTargetSetMethod]
        public virtual void SetFontStyle(FontStyle value)
        {
            IsItalic = value == FontStyles.Italic;
        }

        [DefaultValue(false)]
        public bool IsUnderLine
        {
            get => isUnerline;
            set
            {
                Set(ref isUnerline, value);
                RaiseFontStyleChanged();
            }
        }

        [DefaultValue(false)]
        public bool IsItalic
        {
            get => isItalic;
            set
            {
                Set(ref isItalic, value);
                RaiseFontStyleChanged();
            }
        }
        protected void RaiseFontStyleChanged()
        {
            RaisePropertyChanged(fontStyleChangedEventArgs);
        }
    }
}
