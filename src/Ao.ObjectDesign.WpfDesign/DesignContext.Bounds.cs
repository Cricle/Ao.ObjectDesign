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
        public virtual Vector TargetOffset => VisualTreeHelper.GetOffset(Target);

        public virtual Rect TargetBounds
        {
            get
            {
                if (Target is UIElement uie)
                {
                    var offset = TargetOffset;
                    return new Rect(new Point(offset.X, offset.Y), uie.RenderSize);
                }
                return Rect.Empty;
            }
        }
        public virtual Vector ParentOffset => VisualTreeHelper.GetOffset(Parent);

        public virtual Rect ParentBounds
        {
            get
            {
                if (Parent is UIElement uie)
                {
                    var offset = ParentOffset;
                    return new Rect(new Point(offset.X, offset.Y), uie.RenderSize);
                }
                return Rect.Empty;
            }
        }

        public virtual Vector ContainerOffset => VisualTreeHelper.GetOffset(Container);

        public virtual Rect ContainerBounds
        {
            get
            {
                if (Parent is UIElement uie)
                {
                    var offset = ContainerOffset;
                    return new Rect(new Point(offset.X, offset.Y), uie.RenderSize);
                }
                return Rect.Empty;
            }
        }
    }
}
