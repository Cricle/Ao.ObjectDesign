using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.WpfDesign;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace Ao.ObjectDesign.Session.DesignHelpers
{
    public class ResizeDesignHelper<TScene, TSetting> : Canvas, IWpfDesignHelper
        where TScene : IDesignScene<TSetting>
    {
        public ResizeDesignHelper(IDesignSession<TScene, TSetting> session)
        {
            Session = session;
            Thumb = new Thumb
            {
                Background = Brushes.Transparent,
                Width = 8,
                Height = 8,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Visibility = Visibility.Collapsed
            };
            thumbBorder = new Border
            {
                Background = Brushes.White
            };
            thumbBorder.SetBinding(WidthProperty, new Binding(nameof(Thumb.Width)) { Source = Thumb });
            thumbBorder.SetBinding(HeightProperty, new Binding(nameof(Thumb.Height)) { Source = Thumb });
            thumbBorder.SetBinding(HorizontalAlignmentProperty, new Binding(nameof(Thumb.HorizontalAlignment)) { Source = Thumb });
            thumbBorder.SetBinding(VerticalAlignmentProperty, new Binding(nameof(Thumb.VerticalAlignment)) { Source = Thumb });
            thumbBorder.SetBinding(VisibilityProperty, new Binding(nameof(Thumb.Visibility)) { Source = Thumb });

            Thumb.DragStarted += OnThumbDragStarted;
            Thumb.DragDelta += OnThumbDragDelta;
            Thumb.DragCompleted += OnThumbDragCompleted;

            Children.Add(thumbBorder);
            Children.Add(Thumb);
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
        }

        public IDesignSession<TScene, TSetting> Session { get; }

        public bool EnableSequence { get; set; } = true;

        private void OnThumbDragCompleted(object sender, DragCompletedEventArgs e)
        {
            UpdateThumb(true);
            doing = false;
        }

        private void OnThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (items != null && items.Length != 0)
            {
                var set = new ReadOnlyHashSet<UIElement>(items.Select(x => x.UI));
                var rects = ViewHelper.GetBound(Session.Root, items.Select(x => x.UI));
                var len = items.Length;
                for (int i = 0; i < len; i++)
                {
                    var item = items[i];

                    var p = new Point(item.Origin.X + e.HorizontalChange, item.Origin.Y + e.VerticalChange);
                    var v = item.Transform.Transform(p) - item.TransformOrigin;

                    item.CurrentWidth = Math.Max(0, item.OriginWidth + v.X);
                    item.CurrentHeight = Math.Max(0, item.OriginHeight + v.Y);

                    item.UI.Width = item.CurrentWidth;
                    item.UI.Height = item.CurrentHeight;
                }
                SetLeft(thumbBorder, rects.Right);
                SetTop(thumbBorder, rects.Bottom);
                panel.Update();
            }
        }

        protected class ResizeItem
        {
            public FrameworkElement UI;
            public Point Origin;
            public double OriginWidth;
            public double OriginHeight;

            public double CurrentWidth;
            public double CurrentHeight;

            public GeneralTransform Transform;
            public Point TransformOrigin;
        }
        ResizeItem[] items;
        private void BuildResizeItems(UIElement[] objs)
        {
            objs = objs ?? panel?.DesigningObjects;
            if (objs != null && objs.Length != 0)
            {
                items = new ResizeItem[objs.Length];
                for (int i = 0; i < items.Length; i++)
                {
                    FrameworkElement ele = (FrameworkElement)objs[i];
                    ResizeItem item = new ResizeItem
                    {
                        UI = ele,
                        OriginHeight = ele.DesiredSize.Height,
                        OriginWidth = ele.DesiredSize.Width,
                    };
                    FrameworkElement container = WpfDesignContext.GetContainer(ele);
                    bool isCanvas = container is Canvas;
                    if (isCanvas)
                    {
                        item.Origin = new Point(Canvas.GetLeft(ele), Canvas.GetTop(ele));
                    }
                    else
                    {
                        var trans = WpfDesignMetedata.GetOrAddTransform<TranslateTransform>(ele);
                        item.Origin = new Point(trans.X, trans.Y);
                    }
                    var parent = (Visual)ele.Parent;
                    if (ele == Session.Root)
                    {
                        item.TransformOrigin = item.Origin;
                        item.Transform = DesignConst.EmptyTransform;
                    }
                    else
                    {
                        item.Transform = Session.Root.TransformToVisual(parent);
                        item.TransformOrigin = item.Transform.Transform(item.Origin);
                    }
                    items[i] = item;
                }
            }
        }
        protected virtual void OnRecord(ResizeItem[] items)
        {

        }
        public void UpdateThumb(bool reocrd)
        {
            if (items != null)
            {
                var set = new ReadOnlyHashSet<UIElement>(items.Select(x => x.UI));
                var rects = ViewHelper.GetBound(Session.Root, items.Select(x => x.UI));

                SetLeft(thumbBorder, rects.Right);
                SetTop(thumbBorder, rects.Bottom);
                SetLeft(Thumb, rects.Right);
                SetTop(Thumb, rects.Bottom);

                if (reocrd && EnableSequence && items.Length != 0)
                {
                    OnRecord(items);
                }
            }
        }
        private void OnThumbDragStarted(object sender, DragStartedEventArgs e)
        {
            //items = null;
            BuildResizeItems(panel.DesigningObjects);
            if (items != null)
            {
                doing = true;
            }
        }
        private bool doing;
        private IDesignSuface<UIElement, IWpfDesignContext> panel;
        private readonly Border thumbBorder;

        public Thumb Thumb { get; }

        public void UpdateDesign(IWpfDesignContext context)
        {
            if (context.DesignMetedatas is null || context.DesignMetedatas.Count == 0 || doing)
            {
                return;
            }
            UpdateThumb(false);
        }

        public void Attack(IDesignSuface<UIElement, IWpfDesignContext> panel)
        {
            this.panel = panel;
        }

        public void Dettck()
        {
            panel = null;
        }

        public void AttackObject(UIElement[] old, UIElement[] @new)
        {
            if (@new is null || @new.Length == 0)
            {
                Thumb.Visibility = Visibility.Collapsed;
                items = null;
            }
            else
            {
                BuildResizeItems(@new);
                Thumb.Visibility = Visibility.Visible;
                UpdateThumb(false);
            }
        }
    }
}
