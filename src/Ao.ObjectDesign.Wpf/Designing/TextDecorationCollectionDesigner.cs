using Ao.ObjectDesign.Wpf.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public virtual bool IsBaseline
        {
            get => isBaseline;
            set
            {
                Set(ref isBaseline, value);
                RaiseTextDecorationCollectionChanged();
            }
        }

        public virtual bool IsOverLine
        {
            get => isOverLine;
            set
            {
                Set(ref isOverLine, value);
                RaiseTextDecorationCollectionChanged();
            }
        }

        public virtual bool IsStrikethrough
        {
            get => isStrikethrough;
            set
            {
                Set(ref isStrikethrough, value);
                RaiseTextDecorationCollectionChanged();
            }
        }
        public virtual bool IsUnderline
        {
            get => isUnderline;
            set
            {
                Set(ref isUnderline, value);
                RaiseTextDecorationCollectionChanged();
            }
        }

        public virtual bool HasTextDecorations => isUnderline || isOverLine || isStrikethrough || isBaseline;

        [PlatformTargetProperty]
        public virtual TextDecorationCollection TextDecorationCollection
        {
            get
            {
                if (!HasTextDecorations)
                {
                    return null;
                }
                var coll = new TextDecorationCollection();
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
                    var set = new HashSet<TextDecoration>(value);
                    IsBaseline = TextDecorations.Baseline.All(x => set.Contains(x));
                    IsUnderline = TextDecorations.Underline.All(x => set.Contains(x));
                    IsOverLine = TextDecorations.OverLine.All(x => set.Contains(x));
                    IsStrikethrough = TextDecorations.Strikethrough.All(x => set.Contains(x));
                }
            }
        }
        protected void RaiseTextDecorationCollectionChanged()
        {
            RaisePropertyChanged(nameof(TextDecorationCollection));
        }
    }
}
