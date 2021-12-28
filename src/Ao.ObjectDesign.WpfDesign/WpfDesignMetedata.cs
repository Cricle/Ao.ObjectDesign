using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ao.ObjectDesign.WpfDesign
{
    public class WpfDesignMetedata : DesignMetedata<UIElement, IWpfDesignContext>, IWpfDesignMetedata
    {
        public WpfDesignMetedata(IDesignContext<UIElement, IWpfDesignContext> designContext, UIElement target, UIElement parent, UIElement container)
            : base(designContext, target, parent)
        {
            this.container = container;
            loadedContainer = true;
        }
        public WpfDesignMetedata(IDesignContext<UIElement, IWpfDesignContext> designContext, UIElement target)
            : this(designContext, target, (UIElement)VisualTreeHelper.GetParent(target), null)
        {
        }

        private bool loadedContainer;
        private UIElement container;

        public override UIElement Container
        {
            get
            {
                if (!loadedContainer)
                {
                    container = WpfDesignContext.GetContainer(Target as FrameworkElement);
                    loadedContainer = true;
                }
                return container;
            }
        }

        public override bool IsContainerCanvas => Container is Canvas;


        public override IVector InCanvasPosition
        {
            get
            {
                if (IsContainerCanvas)
                {
                    var x = Canvas.GetLeft(Target);
                    var y = Canvas.GetTop(Target);
                    return new DefaultVector(x, y);
                }
                return DefaultVector.Zero;
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

        public override IVector GetPosition(UIElement element)
        {
            if (element is null)
            {
                return DefaultVector.Zero;
            }
            var offset = VisualTreeHelper.GetOffset(element);
            return new DefaultVector(offset.X, offset.Y);
        }

        public override IVector GetSize(UIElement element)
        {
            if (element is null)
            {
                return DefaultVector.Zero;
            }
            var size = element.DesiredSize;
            return new DefaultVector(size.Width, size.Height);
        }

        public override IVector GetInCanvaPosition(UIElement element)
        {
            if (element is null)
            {
                return DefaultVector.Zero;
            }
            var x = Canvas.GetLeft(element);
            var y = Canvas.GetTop(element);
            return new DefaultVector(x, y);
        }
    }
    public abstract class DirectDesignMetedata : WpfDesignMetedata
    {
        public DirectDesignMetedata(IDesignContext<UIElement, IWpfDesignContext> designContext, UIElement target) : base(designContext, target)
        {
        }

        public DirectDesignMetedata(IDesignContext<UIElement, IWpfDesignContext> designContext, UIElement target, UIElement parent, UIElement container) : base(designContext, target, parent, container)
        {
        }

        public override IVector GetSize(UIElement element)
        {
            var ps = GetPositionSize(element);
            if (ps is null)
            {
                return DefaultVector.Zero;
            }
            return ps.Size;
        }
        public override IVector GetPosition(UIElement element)
        {
            var ps = GetPositionSize(element);
            if (ps is null)
            {
                return DefaultVector.Zero;
            }
            return ps.Position;
        }

        protected abstract IPositionSize GetPositionSize(UIElement element);
    }
}
