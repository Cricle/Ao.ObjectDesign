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

        protected virtual Vector CreatePosition(Func<IDesignMetedata, Vector> selector)
        {
            if (DesignMetedatas.Count == 0)
            {
                return default;
            }
            if (DesignMetedatas.Count == 1)
            {
                return selector(DesignMetedatas[0]);
            }
            if (!DesignMetedatas.Select(selector).Any())
            {
                return default;
            }
            double x = double.PositiveInfinity;
            double y = double.PositiveInfinity;
            foreach (var item in DesignMetedatas.Select(selector))
            {
                x = Math.Min(x, item.X);
                y = Math.Min(y, item.Y);
            }
            return new Vector(x, y);
        }

        protected virtual Rect CreateBounds(Func<IDesignMetedata, Rect> selector)
        {
            if (DesignMetedatas.Count == 0)
            {
                return Rect.Empty;
            }
            if (DesignMetedatas.Count == 1)
            {
                return selector(DesignMetedatas[0]);
            }
            if (!DesignMetedatas.Select(selector).Any())
            {
                return Rect.Empty;
            }
            double left = double.PositiveInfinity;
            double top = double.PositiveInfinity;
            double right = 0;
            double bottom = 0;
            foreach (var g in DesignMetedatas.Select(selector))
            {
                left = Math.Min(left, g.Left);
                top = Math.Min(top, g.Top);
                right = Math.Max(right, g.Right);
                bottom = Math.Max(bottom, g.Bottom);
            }
            var width = Math.Max(0, right - left);
            var height = Math.Max(0, bottom - top);
            return new Rect(left, top, width, height);

        }
    }
}
