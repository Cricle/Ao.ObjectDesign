using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Ao.ObjectDesign.Wpf.Designing
{
    [DesignFor(typeof(TextDecorationCollection))]
    public class TextDecorationCollectionDesigner : NotifyableObject
    {
        private bool isUnderline;
        private bool isStrikethrough;
        private bool isOverLine;
        private bool isBaseline;

        [DefaultValue(false)]
        public virtual bool IsBaseline
        {
            get => isBaseline;
            set
            {
                Set(ref isBaseline, value);
                RaiseTextDecorationCollectionChanged();
            }
        }

        [DefaultValue(false)]
        public virtual bool IsOverLine
        {
            get => isOverLine;
            set
            {
                Set(ref isOverLine, value);
                RaiseTextDecorationCollectionChanged();
            }
        }

        [DefaultValue(false)]
        public virtual bool IsStrikethrough
        {
            get => isStrikethrough;
            set
            {
                Set(ref isStrikethrough, value);
                RaiseTextDecorationCollectionChanged();
            }
        }
        [DefaultValue(false)]
        public virtual bool IsUnderline
        {
            get => isUnderline;
            set
            {
                Set(ref isUnderline, value);
                RaiseTextDecorationCollectionChanged();
            }
        }

        public virtual bool HasTextDecorations
        {
            get => isUnderline || isOverLine || isStrikethrough || isBaseline;
            set
            {
                if (value)
                {
                    throw new NotSupportedException($"HasTextDecorations setter only accept false");
                }
                else
                {
                    IsUnderline = IsOverLine = IsStrikethrough = IsBaseline = false;
                }
            }
        }

        [PlatformTargetProperty]
        public virtual TextDecorationCollection TextDecorationCollection
        {
            get
            {
                if (!HasTextDecorations)
                {
                    return null;
                }
                TextDecorationCollection coll = new TextDecorationCollection();
                if (isUnderline)
                {
                    coll.Add(TextDecorations.Underline);
                }
                if (isOverLine)
                {
                    coll.Add(TextDecorations.OverLine);
                }
                if (isStrikethrough)
                {
                    coll.Add(TextDecorations.Strikethrough);
                }
                if (isBaseline)
                {
                    coll.Add(TextDecorations.Baseline);
                }
                return coll;
            }
            set
            {
                if (value is null)
                {
                    IsBaseline = IsUnderline = IsOverLine = IsStrikethrough = false;
                }
                else
                {
                    HashSet<TextDecoration> set = new HashSet<TextDecoration>(value);
                    IsBaseline = TextDecorations.Baseline.All(x => set.Contains(x));
                    IsUnderline = TextDecorations.Underline.All(x => set.Contains(x));
                    IsOverLine = TextDecorations.OverLine.All(x => set.Contains(x));
                    IsStrikethrough = TextDecorations.Strikethrough.All(x => set.Contains(x));
                }
            }
        }
        private static readonly PropertyChangedEventArgs textDecorationCollectionEventArgs = new PropertyChangedEventArgs(nameof(TextDecorationCollection));
        protected void RaiseTextDecorationCollectionChanged()
        {
            RaisePropertyChanged(textDecorationCollectionEventArgs);
        }
    }
}
