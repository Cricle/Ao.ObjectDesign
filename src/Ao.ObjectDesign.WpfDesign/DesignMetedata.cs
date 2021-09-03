using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ao.ObjectDesign.WpfDesign
{
    public class DesignMetedata: IDesignMetedata
    {
        public DesignMetedata(DesignContext designContext, UIElement target, UIElement parent, UIElement container)
        {
            DesignContext = designContext;
            Target = target;
            Parent = parent;
            this.container = container;
            loadedContainer = true;
        }
        public DesignMetedata(DesignContext designContext, UIElement target)
        {
            DesignContext = designContext;
            Target = target;
            Parent = (UIElement)VisualTreeHelper.GetParent(target);
        }

        private bool loadedContainer;
        private UIElement container;

        public DesignContext DesignContext { get; }

        public UIElement Target { get; }

        public UIElement Parent { get; }

        public virtual UIElement Container
        {
            get
            {
                if (!loadedContainer)
                {
                    container = DesignContext.GetContainer(Target as FrameworkElement);
                    loadedContainer = true;
                }
                return container;
            }
        }

        public virtual bool IsContainerCanvas => Container is Canvas;


        public virtual Vector InCanvasPosition
        {
            get
            {
                if (IsContainerCanvas)
                {
                    var x = Canvas.GetLeft(Target);
                    var y = Canvas.GetTop(Target);
                    return new Vector(x, y);
                }
                return new Vector();
            }
        }

        public virtual Vector TargetOffset
        {
            get
            {
                if (IsContainerCanvas)
                {
                    return InCanvasPosition;
                }
                return TargetOffset;
            }
        }

        public virtual Vector IgnoreCanvasTargetOffset => GetPosition(Target);

        public virtual Rect TargetBounds
        {
            get
            {
                var offset = TargetOffset;
                var size = GetSize(Target);
                return new Rect(new Point(offset.X, offset.Y), size);
            }
        }
        public virtual Vector ParentOffset => GetPosition(Parent);

        public virtual Rect ParentBounds
        {
            get
            {
                var offset = ParentOffset;
                var size = GetSize(Parent);
                return new Rect(new Point(offset.X, offset.Y), size);
            }
        }

        public virtual Vector ContainerOffset => GetPosition(Container);

        public virtual Rect ContainerBounds
        {
            get
            {
                var offset = ContainerOffset;
                var size = GetSize(container);
                return new Rect(new Point(offset.X, offset.Y), size);
            }
        }


        public static T GetOrAddTransform<T>(UIElement target)
            where T : Transform, new()
        {
            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (target.RenderTransform is T t)
            {
                return t;
            }
            else if (target.RenderTransform is TransformGroup tg)
            {
                var transform = tg.Children.FirstOrDefault(x => x is T) as T;
                if (transform is null)
                {
                    transform = new T();
                }
                tg.Children.Add(transform);
                return transform;
            }
            else if (target.RenderTransform is null)
            {
                var transform = new T();
                target.RenderTransform = transform;
                return transform;
            }
            else
            {
                var group = new TransformGroup();
                var transform = new T();
                group.Children.Add(target.RenderTransform);
                group.Children.Add(transform);
                target.RenderTransform = group;
                return transform;
            }
        }

        public virtual Vector GetPosition(UIElement element)
        {
            return VisualTreeHelper.GetOffset(element);
        }

        public virtual Size GetSize(UIElement element)
        {
            if (element is null)
            {
                return Size.Empty;
            }
            return element.DesiredSize;
        }

        public virtual Vector GetInCanvaPosition(UIElement element)
        {
            var x = Canvas.GetLeft(element);
            var y = Canvas.GetTop(element);
            return new Vector(x, y);
        }
    }
    public abstract class DirectDesignMetedata : DesignMetedata
    {
        public DirectDesignMetedata(DesignContext designContext, UIElement target) : base(designContext, target)
        {
        }

        public DirectDesignMetedata(DesignContext designContext, UIElement target, UIElement parent, UIElement container) : base(designContext, target, parent, container)
        {
        }

        public override Size GetSize(UIElement element)
        {
            var ps = GetPositionSize(element);
            if (ps is null)
            {
                return Size.Empty;
            }
            return Size.Empty;
        }
        public override Vector GetPosition(UIElement element)
        {
            var ps = GetPositionSize(element);
            if (ps is null)
            {
                return new Vector();
            }
            return ps.Position;
        }

        protected abstract IPositionSize GetPositionSize(UIElement element);
    }
}
