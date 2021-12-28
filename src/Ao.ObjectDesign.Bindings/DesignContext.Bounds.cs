using Ao.ObjectDesign.Designing.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Bindings
{
    public partial class DesignContext<TUI, TContext>
    {
        private IVector targetOffset;
        private IVector parentOffset;
        private IVector containerOffset;

        private IRect targetBounds;
        private IRect parentBounds;
        private IRect containerBounds;

        public virtual IVector TargetOffset
        {
            get
            {
                if (targetOffset is null)
                {
                    targetOffset = CreatePosition(x => x.TargetOffset);
                }
                return targetOffset;
            }
        }

        public virtual IRect TargetBounds
        {
            get
            {
                if (targetBounds is null)
                {
                    targetBounds = CreateBounds(x => x.TargetBounds);
                }
                return targetBounds;
            }
        }

        public virtual IVector ParentOffset
        {
            get
            {
                if (parentOffset is null)
                {
                    parentOffset = CreatePosition(x => x.ParentOffset);
                }
                return parentOffset;
            }
        }

        public virtual IRect ParentBounds
        {
            get
            {
                if (parentBounds is null)
                {
                    parentBounds = CreateBounds(x => x.ParentBounds);
                }
                return parentBounds;
            }
        }

        public virtual IVector ContainerOffset
        {
            get
            {
                if (containerOffset is null)
                {
                    containerOffset = CreatePosition(x => x.ContainerOffset);
                }
                return containerOffset;
            }
        }

        public virtual IRect ContainerBounds
        {
            get
            {
                if (containerBounds is null)
                {
                    containerBounds = CreateBounds(x => x.ContainerBounds);
                }
                return containerBounds;
            }
        }

        protected virtual IVector CreatePosition(Func<IDesignMetedata<TUI, TContext>, IVector> selector)
        {
            if (DesignMetedatas.Count == 0)
            {
                return default;
            }
            if (DesignMetedatas.Count == 1)
            {
                return selector(DesignMetedatas[0]);
            }
            var query = DesignMetedatas.Select(selector);
            if (!query.Any())
            {
                return default;
            }
            double x = double.PositiveInfinity;
            double y = double.PositiveInfinity;
            foreach (var item in query)
            {
                x = Math.Min(x, item.X);
                y = Math.Min(y, item.Y);
            }
            return new DefaultVector(x, y);
        }

        protected virtual IRect CreateBounds(Func<IDesignMetedata<TUI, TContext>, IRect> selector)
        {
            if (DesignMetedatas.Count == 0)
            {
                return DefaultRect.Zero;
            }
            if (DesignMetedatas.Count == 1)
            {
                return selector(DesignMetedatas[0]);
            }
            var query = DesignMetedatas.Select(selector);
            if (!query.Any())
            {
                return DefaultRect.Zero;
            }
            double left = double.PositiveInfinity;
            double top = double.PositiveInfinity;
            double right = 0;
            double bottom = 0;
            foreach (var g in query)
            {
                left = Math.Min(left, g.Left);
                top = Math.Min(top, g.Top);
                right = Math.Max(right, g.Right);
                bottom = Math.Max(bottom, g.Bottom);
            }
            var width = Math.Max(0, right - left);
            var height = Math.Max(0, bottom - top);
            return new DefaultRect(left, top, width, height);

        }
    }
}
