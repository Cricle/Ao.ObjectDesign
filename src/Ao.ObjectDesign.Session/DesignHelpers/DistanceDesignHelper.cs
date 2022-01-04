using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.WpfDesign;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Ao.ObjectDesign.Session.DesignHelpers
{
    public class DistanceDesignHelper<TScene, TSetting> : Canvas, IWpfDesignHelper
        where TScene : IDesignScene<TSetting>
    {
        public DistanceDesignHelper(IDesignSession<TScene, TSetting> session)
        {
            Session = session;

            DoubleCollection ds = new DoubleCollection(new double[] { 2, 4 });
            XAxis = new Line { Stroke = Brushes.Red, StrokeThickness = 2, StrokeDashArray = ds };

            XAxisLabel = new TextBlock();

            YAxis = new Line { Stroke = Brushes.Red, StrokeThickness = 2, StrokeDashArray = ds };
            YAxis.RenderTransformOrigin = new Point(0.5, 0.5);
            YAxisLabel = new TextBlock
            {
                RenderTransform = new RotateTransform { Angle = 90 }
            };

            Children.Add(XAxis);
            Children.Add(YAxis);

            Children.Add(XAxisLabel);
            Children.Add(YAxisLabel);
            foreach (FrameworkElement item in Children)
            {
                item.HorizontalAlignment = HorizontalAlignment.Left;
                item.VerticalAlignment = VerticalAlignment.Top;
            }
            DistanceFormat = "f1";
            IsHitTestVisible = false;
        }
        public IDesignSession<TScene, TSetting> Session { get; }

        public string DistanceFormat { get; set; }

        public Line XAxis { get; }

        public TextBlock XAxisLabel { get; }

        public Line YAxis { get; }

        public TextBlock YAxisLabel { get; }

        public RotateTransform YAxisLabelRotateTransform { get; }

        private bool anyTrans;

        public void Attack(IDesignSuface<UIElement, IWpfDesignContext> panel)
        {
        }

        public void AttackObject(UIElement[] old, UIElement[] @new)
        {
            anyTrans = false;
            Visibility v = Visibility.Visible;
            if (@new is null || @new.Length == 0)
            {
                v = Visibility.Collapsed;
            }
            else
            {
                var anyTrans = false;
                foreach (FrameworkElement item in @new)
                {
                    if (item.Parent == Session.Root)
                    {
                        continue;
                    }
                    var hasTrans = false;
                    var parent = item;
                    while (parent != null && parent != Session.Root)
                    {
                        if (parent.RenderTransform != null && parent.RenderTransform.Value != Matrix.Identity)
                        {
                            hasTrans = true;
                            break;
                        }
                        parent = (FrameworkElement)parent.Parent;
                    }
                    if (hasTrans)
                    {
                        v = Visibility.Collapsed;
                        anyTrans = true;
                        break;
                    }
                }
                this.anyTrans = anyTrans;
            }

            foreach (FrameworkElement item in Children)
            {
                if (item.Visibility != v)
                {
                    item.Visibility = v;
                }
            }
        }

        public void Dettck()
        {
        }

        public void UpdateDesign(IWpfDesignContext context)
        {
            if (!anyTrans && context.DesignMetedatas is null || context.DesignMetedatas.Count == 0)
            {
                return;
            }

            var rc1 = ViewHelper.GetBound(Session.Root, context.DesignMetedatas.Select(x => x.Parent));
            var rc2 = ViewHelper.GetBound((Visual)VisualTreeHelper.GetParent(context.DesignMetedatas[0].Target),
                context.DesignMetedatas.Select(x => x.Target));

            double posX1X = rc1.Left;
            double posX1Y = rc1.Top + rc2.Top + (rc2.Bottom - rc2.Top) / 2;

            double posX2X = rc1.Left + rc2.Left;
            double posX2Y = rc1.Top + rc2.Top + (rc2.Bottom - rc2.Top) / 2;

            double posY1X = rc1.Left + rc2.Left + (rc2.Right - rc2.Left) / 2;
            double posY1Y = rc1.Top;

            double posY2X = rc1.Left + rc2.Left + (rc2.Right - rc2.Left) / 2;
            double posY2Y = rc1.Top + rc2.Top;

            YAxis.X1 = double.IsNaN(posY1X) ? 0 : posY1X;
            YAxis.X2 = double.IsNaN(posY2X) ? 0 : posY2X;
            YAxis.Y1 = double.IsNaN(posY1Y) ? 0 : posY1Y;
            YAxis.Y2 = double.IsNaN(posY2Y) ? 0 : posY2Y;

            XAxis.X1 = double.IsNaN(posX1X) ? 0 : posX1X;
            XAxis.X2 = double.IsNaN(posX2X) ? 0 : posX2X;
            XAxis.Y1 = double.IsNaN(posX1Y) ? 0 : posX1Y;
            XAxis.Y2 = double.IsNaN(posX2Y) ? 0 : posX2Y;

            YAxisLabel.Text = (posY2Y - posY1Y).ToString(DistanceFormat);

            if (!double.IsNaN(posY2X) && !double.IsNaN(posY2Y) && !double.IsNaN(posY1Y))
            {
                SetLeft(YAxisLabel, posY2X);
                SetTop(YAxisLabel, posY1Y + (posY2Y - posY1Y) / 2);
            }

            XAxisLabel.Text = (posX2X - posX1X).ToString(DistanceFormat);
            if (!double.IsNaN(posX2X) && !double.IsNaN(posX1X) && !double.IsNaN(posX2Y))
            {
                SetLeft(XAxisLabel, posX1X + (posX2X - posX1X) / 2);
                SetTop(XAxisLabel, posX2Y);
            }

        }
    }
}
