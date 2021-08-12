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
        public virtual Vector TargetOffset => CreatePosition(x => x.TargetOffset);

        public virtual Rect TargetBounds => CreateBounds(x => x.TargetBounds);

        public virtual Vector ParentOffset => CreatePosition(x => x.ParentOffset);

        public virtual Rect ParentBounds => CreateBounds(x => x.ParentBounds);

        public virtual Vector ContainerOffset => CreatePosition(x => x.ContainerOffset);

        public virtual Rect ContainerBounds => CreateBounds(x => x.ContainerBounds);

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
