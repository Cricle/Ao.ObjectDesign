using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.WpfDesign;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ao.ObjectDesign.Session.DesignHelpers
{
    public class BoundsDesignHelper<TScene, TSetting> : Border, IWpfDesignHelper
        where TScene : IDesignScene<TSetting>
    {
        public BoundsDesignHelper(IDesignSession<TScene, TSetting> session)
        {
            Session = session;
            Visibility = Visibility.Collapsed;
            BorderBrush = Brushes.AliceBlue;
            BorderThickness = new Thickness(1);
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
        }

        public IDesignSession<TScene, TSetting> Session { get; }

        public void Attack(IDesignSuface<UIElement, IWpfDesignContext> panel)
        {
        }

        public void AttackObject(UIElement[] old, UIElement[] @new)
        {
            Visibility = (@new is null || @new.Length == 0) ? Visibility.Collapsed : Visibility.Visible;
        }

        public void Dettck()
        {
        }

        public void UpdateDesign(IWpfDesignContext context)
        {
            var bounds = ViewHelper.GetBound(Session.Root, Session.Suface.DesigningObjects);
            var left = Math.Min(bounds.Left, bounds.Right);
            var top = Math.Min(bounds.Top, bounds.Bottom);
            Canvas.SetLeft(this, left - 1);
            Canvas.SetTop(this, top - 1);

            Width = Math.Abs(bounds.Right - bounds.Left) + 2;
            Height = Math.Abs(bounds.Bottom - bounds.Top) + 2;

        }
    }
}
