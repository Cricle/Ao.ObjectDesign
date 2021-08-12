using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ao.ObjectDesign.WpfDesign
{
    public class DesignMetedata
    {
        private bool loadedContainer;
        private UIElement container;

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

        public DesignContext DesignContext { get; }

        public UIElement Target { get; }

        public UIElement Parent { get; }

        public UIElement Container
        {
            get
            {
                if (!loadedContainer)
                {
                    container = (UIElement)DesignContext.GetContainer(Target);
                    loadedContainer = true;
                }
                return container;
            }
        }

        public bool IsContainerCanvas => Container is Canvas;

        public Vector InCanvasPosition
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
                return VisualTreeHelper.GetOffset(Target);
            }
        }

        public virtual Vector IgnoreCanvasTargetOffset => VisualTreeHelper.GetOffset(Target);

        public virtual Rect TargetBounds
        {
            get
            {
                if (Target != null)
                {
                    var offset = TargetOffset;
                    return new Rect(new Point(offset.X, offset.Y), Target.RenderSize);
                }
                return Rect.Empty;
            }
        }
        public virtual Vector ParentOffset => VisualTreeHelper.GetOffset(Parent);

        public virtual Rect ParentBounds
        {
            get
            {
                if (Parent != null)
                {
                    var offset = ParentOffset;
                    return new Rect(new Point(offset.X, offset.Y), Parent.RenderSize);
                }
                return Rect.Empty;
            }
        }

        public virtual Vector ContainerOffset => VisualTreeHelper.GetOffset(Container);

        public virtual Rect ContainerBounds
        {
            get
            {
                if (Container != null)
                {
                    var offset = ContainerOffset;
                    return new Rect(new Point(offset.X, offset.Y), Container.RenderSize);
                }
                return Rect.Empty;
            }
        }

        public IEnumerable<DependencyObject> GetBrothersWithContainer()
        {
            return GetBrothersWithContainer(_ => true);
        }

        public virtual IEnumerable<DependencyObject> GetBrothersWithContainer(Predicate<DependencyObject> childFilter)
        {
            if (Container != null)
            {
                var count = VisualTreeHelper.GetChildrenCount(Container);
                for (int i = 0; i < count; i++)
                {
                    var val = VisualTreeHelper.GetChild(Container, i);
                    if (childFilter(val))
                    {
                        yield return val;
                    }
                }
            }
        }
        public T GetOrAddTransform<T>()
            where T : Transform, new()
        {
            return GetOrAddTransform<T>(Target);
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
    }
}
