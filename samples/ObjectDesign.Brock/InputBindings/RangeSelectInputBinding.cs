using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Wpf.Desiging;
using Ao.ObjectDesign.Session.Wpf.DesignHelpers;
using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Input;
using ObjectDesign.Brock.Components;
using ObjectDesign.Brock.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ObjectDesign.Brock.InputBindings
{
    internal class RangeSelectInputBinding : PreviewMouseInputBase, IDisposable
    {
        public RangeSelectInputBinding(IDesignSession<Scene, UIElementSetting> session,
            MySceneMakerRuntime runtime)
        {
            Runtime = runtime;
            Session = session;
            RangeSelect = new Rectangle
            {
                Stroke = Brushes.IndianRed,
                StrokeDashArray = new DoubleCollection(new double[] { 2, 4 }),
                StrokeThickness = 2,
                Width = 0,
                Height = 0
            };
        }

        public IDesignSession<Scene, UIElementSetting> Session { get; }

        public Rectangle RangeSelect { get; }

        public MySceneMakerRuntime Runtime { get; }

        public bool SelectFullContains { get; set; } = true;

        private Point startPoint;
        private bool isSelecting;
        private bool scopeSelectFullContains;

        private static bool CanSelect(in Point p1, in Point p2)
        {
            return Math.Abs(p2.X - p1.X) >= SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(p2.Y - p1.Y) >= SystemParameters.MinimumVerticalDragDistance;
        }
        public override void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Alt) != ModifierKeys.Alt)
            {
                return;
            }
            isSelecting = true;
            startPoint = e.GetPosition(sender as UIElement);
            Canvas.SetLeft(RangeSelect, startPoint.X);
            Canvas.SetTop(RangeSelect, startPoint.Y);
            RangeSelect.Width = RangeSelect.Height = 0;
            if (!Session.Suface.Children.Contains(RangeSelect))
            {
                Session.Suface.Children.Add(RangeSelect);
            }
            scopeSelectFullContains = SelectFullContains;
            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                scopeSelectFullContains = true;
            }
        }
        public override void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!isSelecting)
            {
                return;
            }
            Point currentPoint = e.GetPosition(Session.Suface);
            if (!CanSelect(startPoint, currentPoint))
            {
                return;
            }
            double x = startPoint.X;
            double y = startPoint.Y;
            double width = currentPoint.X - startPoint.X;
            double height = currentPoint.Y - startPoint.Y;
            if (width < 0)
            {
                x = currentPoint.X;
                width = Math.Abs(width);
            }
            if (height < 0)
            {
                y = currentPoint.Y;
                height = Math.Abs(height);
            }
            Canvas.SetLeft(RangeSelect, x);
            Canvas.SetTop(RangeSelect, y);
            RangeSelect.Width = width;
            RangeSelect.Height = height;
        }
        public override void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!isSelecting)
            {
                return;
            }
            isSelecting = false;
            var selected = new List<UIElement>();
            IEnumerable<IDesignSceneController<UIElement, UIElementSetting>> current =
                new IDesignSceneController<UIElement, UIElementSetting>[] { Session.SceneManager.CurrentSceneController };

            var act = ViewHelper.GetRectOfObject(Session.Root, RangeSelect);

            while (current.Any())
            {
                var rcs = ViewHelper.GetBoundsWithUI(Session.Root, current.SelectMany(x => x.DesignUnitMap.Keys));
                foreach (var item in rcs)
                {
                    if (scopeSelectFullContains)
                    {
                        if (act.Contains(item.Value))
                        {
                            selected.Add(item.Key);
                        }
                    }
                    else
                    {
                        if (act.IntersectsWith(item.Value))
                        {
                            selected.Add(item.Key);
                        }
                    }
                }
                current = current.SelectMany(x => x.DesignUnitNextMap.Values);
            }

            Session.Suface.Children.Remove(RangeSelect);

            Runtime.DesigningContexts.Clear();
            Session.Suface.DesigningObjects = selected.ToArray();
            Session.Suface.UpdateInRender();

        }

        public void Dispose()
        {
            Session.Suface.Children.Remove(RangeSelect);
        }

    }
}
