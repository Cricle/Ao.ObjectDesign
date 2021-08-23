using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Ao.ObjectDesign.WpfDesign
{
    public partial class DesignContext
    {
        private Vector? targetOffset;
        private Vector? parentOffset;
        private Vector? containerOffset;

        private Rect? targetBounds;
        private Rect? parentBounds;
        private Rect? containerBounds;

        public virtual Vector TargetOffset
        {
            get
            {
                if (targetOffset is null)
                {
                    targetOffset = CreatePosition(x => x.TargetOffset);
                }
                return targetOffset.Value;
            }
        }

        public virtual Rect TargetBounds
        {
            get
            {
                if (targetBounds is null)
                {
                    targetBounds = CreateBounds(x => x.TargetBounds);
                }
                return targetBounds.Value;
            }
        }

        public virtual Vector ParentOffset
        {
            get
            {
                if (parentOffset is null)
                {
                    parentOffset = CreatePosition(x => x.ParentOffset);
                }
                return parentOffset.Value;
            }
        }

        public virtual Rect ParentBounds
        {
            get
            {
                if (parentBounds is null)
                {
                    parentBounds = CreateBounds(x => x.ParentBounds);
                }
                return parentBounds.Value;
            }
        }

        public virtual Vector ContainerOffset
        {
            get
            {
                if (containerOffset is null)
                {
                    containerOffset = CreatePosition(x => x.ContainerOffset);
                }
                return containerOffset.Value;
            }
        }

        public virtual Rect ContainerBounds
        {
            get
            {
                if (containerBounds is null)
                {
                    containerBounds = CreateBounds(x => x.ContainerBounds);
                }
                return containerBounds.Value;
            }
        }

        private Vector CreatePosition(Func<DesignMetedata, Vector> selector)
        {
            if (DesignMetedatas.Count == 0)
            {
                return default;
            }
            if (DesignMetedatas.Count == 1)
            {
                return selector(DesignMetedatas[0]);
            }
            var offsetGroups = DesignMetedatas.Select(selector).ToList();
            var x = offsetGroups.Min(q => q.X);
            var y = offsetGroups.Min(q => q.Y);
            return new Vector(x, y);
        }

        private Rect CreateBounds(Func<DesignMetedata, Rect> selector)
        {
            if (DesignMetedatas.Count == 0)
            {
                return Rect.Empty;
            }
            if (DesignMetedatas.Count == 1)
            {
                return selector(DesignMetedatas[0]);
            }
            var offsetGroups = DesignMetedatas.Select(selector).ToList();
            var left = offsetGroups.Min(q => q.Left);
            var top = offsetGroups.Min(q => q.Top);
            var right = offsetGroups.Max(q => q.Right);
            var bottom = offsetGroups.Max(q => q.Bottom);
            var width = Math.Max(0, right - left);
            var height = Math.Max(0, bottom - top);
            return new Rect(left, top, width, height);

        }
    }
}
