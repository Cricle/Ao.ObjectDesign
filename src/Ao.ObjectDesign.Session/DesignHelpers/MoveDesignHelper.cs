using Ao.ObjectDesign;
using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.WpfDesign;
using Ao.ObjectDesign.WpfDesign.Designers;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Ao.ObjectDesign.Session.DesignHelpers
{
    public class MoveDesignHelper<TScene, TSetting> : Border, IWpfDesignHelper
        where TScene : IDesignScene<TSetting>
    {
        protected class MoveItem
        {
            public Point Origin;

            public Point TransformOrigin;

            public double NowX;

            public double NowY;

            public FrameworkElement UI;

            public FrameworkElement Container;

            public TranslateTransform TranslateTransform;

            public bool IsCanvas;

            public GeneralTransform Transform;
        }
        public MoveDesignHelper(IDesignSession<TScene, TSetting> session)
        {
            Session = session;
            Thumb = new Thumb
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Background = Brushes.Transparent,
                Visibility = Visibility.Collapsed
            };

            Thumb.DragStarted += OnThumbDragStarted;
            Thumb.DragDelta += OnThumbDragDelta;
            Thumb.DragCompleted += OnThumbDragCompleted;
            Child= Thumb;
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
            //Background = new SolidColorBrush
            //{
            //    Color = Color.FromArgb(0x66, 0x22, 0x33, 0x44)
            //};
        }

        private void OnThumbDragStarted(object sender, DragStartedEventArgs e)
        {
            BuildMovableItems(panel.DesigningObjects);
            isMoving = true;
        }
        private bool isMoving;

        public IDesignSession<TScene, TSetting> Session { get; }

        public bool EnableSequence { get; set; } = true;

        private void OnThumbDragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (moveItems != null && moveItems.Length != 0)
            {
                UpdateMovable(true);
            }
            isMoving = false;
        }
        private void BuildMovableItems(UIElement[] objs)
        {
            objs = objs ?? panel?.DesigningObjects;
            if (objs != null && objs.Length != 0)
            {
                moveItems = new MoveItem[objs.Length];
                for (int i = 0; i < objs.Length; i++)
                {
                    FrameworkElement item = (FrameworkElement)objs[i];

                    FrameworkElement container = WpfDesignContext.GetContainer(item);
                    bool isCanvas = container is Canvas;

                    MoveItem mi = new MoveItem
                    {
                        Container = container,
                        UI = item,
                        IsCanvas = isCanvas
                    };
                    if (!isCanvas)
                    {
                        mi.TranslateTransform = WpfDesignMetedata.GetOrAddTransform<TranslateTransform>(item);
                        mi.Origin = new Point(mi.TranslateTransform.X, mi.TranslateTransform.Y);
                        mi.NowX = mi.Origin.X;
                        mi.NowY = mi.Origin.Y;
                    }
                    else
                    {
                        var x = Canvas.GetLeft(item);
                        var y = Canvas.GetTop(item);
                        mi.Origin = new Point(x, y);
                        mi.NowX = x;
                        mi.NowY = y;
                    }
                    var parent = VisualTreeHelper.GetParent(item);
                    if (parent == Session.Root)
                    {
                        mi.TransformOrigin = mi.Origin;
                        mi.Transform = DesignConst.EmptyTransform;
                    }
                    else
                    {
                        mi.Transform = Session.Root.TransformToVisual((Visual)parent);
                        mi.TransformOrigin = mi.Transform.Transform(mi.Origin);
                    }
                    moveItems[i] = mi;
                }
            }
        }
        private MoveItem[] moveItems;

        private void OnThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            UIElement[] objs = panel?.DesigningObjects;
            if (moveItems != null && moveItems.Length != 0 && objs != null && objs.Length == moveItems.Length)
            {
                var len = moveItems.Length;
                for (int i = 0; i < len; i++)
                {
                    var item = moveItems[i];
                    var p = new Point(item.Origin.X + e.HorizontalChange, item.Origin.Y + e.VerticalChange);
                    var v = item.Transform.Transform(p) - item.TransformOrigin;
                    if (item.IsCanvas)
                    {

                        item.NowX = item.Origin.X + v.X;
                        item.NowY = item.Origin.Y + v.Y;
                        Canvas.SetLeft(item.UI, item.NowX);
                        Canvas.SetTop(item.UI, item.NowY);
                    }
                    else
                    {
                        item.TranslateTransform.X = item.Origin.X + v.X;
                        item.TranslateTransform.Y = item.Origin.Y + v.Y;
                    }
                }
                panel.Update();
            }
        }

        private IDesignSuface<UIElement, IWpfDesignContext> panel;

        public IDesignSuface<UIElement, IWpfDesignContext> Panel => panel;

        public Thumb Thumb { get; }

        public void Attack(IDesignSuface<UIElement, IWpfDesignContext> panel)
        {
            this.panel = panel;
        }

        public void Dettck()
        {
            panel = null;
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);
            Translate(Thumb);
        }
        private void Translate(DependencyObject @object)
        {
            int count = VisualTreeHelper.GetChildrenCount(@object);
            for (int i = 0; i < count; i++)
            {
                DependencyObject val = VisualTreeHelper.GetChild(@object, i);
                if (val is Border b)
                {
                    b.Background = Brushes.Transparent;
                }
                Translate(val);
            }
        }
        public void UpdateDesign(IWpfDesignContext context)
        {
            if (context.DesignMetedatas is null || context.DesignMetedatas.Count == 0)
            {
                return;
            }
            if (isMoving)
            {
                var bs = ViewHelper.GetBound(Session.Root, context.DesignSuface.DesigningObjects);
                Thumb.Width = bs.Width;
                Thumb.Height = bs.Height;
            }
            else
            {
                UpdateMovable(false);
            }
        }
        protected virtual void OnRecord(MoveItem[] items)
        {

        }
        private void UpdateMovable(bool record)
        {
            var set = new ReadOnlyHashSet<UIElement>(moveItems.Select(x => x.UI));
            var rects = ViewHelper.GetBound(Session.Root, moveItems.Select(x => x.UI));

            Canvas.SetLeft(this, rects.Left);
            Canvas.SetTop(this, rects.Top);
            Thumb.Width = Math.Max(0, rects.Width);
            Thumb.Height = Math.Max(0, rects.Height);

            var len = moveItems.Length;
            if (record && EnableSequence && moveItems.Length != 0)
            {
                OnRecord(moveItems);
            }
        }
        public void AttackObject(UIElement[] old, UIElement[] @new)
        {
            if (@new != null && @new.Length != 0)
            {
                BuildMovableItems(@new);
                UpdateMovable(false);
            }
            else
            {
                moveItems = null;
            }

            Thumb.Visibility = (@new is null || @new.Length == 0) ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
