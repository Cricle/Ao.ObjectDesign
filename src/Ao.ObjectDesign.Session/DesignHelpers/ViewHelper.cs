using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace Ao.ObjectDesign.Session.DesignHelpers
{
    public static class ViewHelper
    {
        public static IEnumerable<Rect> GetBounds(Visual container, IEnumerable<UIElement> targets)
        {
            foreach (var target in targets)
            {
                if (target is FrameworkElement fe && fe.Parent != null)
                {
                    yield return GetRectOfObject(container, fe);
                }
            }

        }
        public static IEnumerable<KeyValuePair<UIElement, Rect>> GetBoundsWithUI(Visual container, IEnumerable<UIElement> targets)
        {
            foreach (var target in targets)
            {
                if (target is FrameworkElement fe && fe.Parent != null)
                {
                    yield return new KeyValuePair<UIElement, Rect>(target, GetRectOfObject(container, fe));
                }
            }

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rect GetRectOfObject(Visual container, FrameworkElement fe)
        {
            return fe.TransformToVisual(container)
                       .TransformBounds(new Rect(fe.DesiredSize));
        }
        private static double Normal(double d)
        {
            if (double.IsNaN(d) || double.IsInfinity(d) || double.IsNegativeInfinity(d))
            {
                return 0;
            }
            return d;
        }
        public static Rect GetBound(Visual container, IEnumerable<UIElement> targets)
        {
            var left = double.PositiveInfinity;
            var top = double.PositiveInfinity;
            var right = double.NegativeInfinity;
            var bottom = double.NegativeInfinity;
            foreach (var target in targets)
            {
                if (target is FrameworkElement fe && fe.Parent != null)
                {
                    var rect = GetRectOfObject(container, fe);
                    left = Math.Min(left, rect.Left);
                    top = Math.Min(top, rect.Top);
                    right = Math.Max(right, rect.Right);
                    bottom = Math.Max(bottom, rect.Bottom);
                }
            }
            left = Normal(left);
            top = Normal(top);
            right = Normal(right);
            bottom = Normal(bottom);
            return new Rect(left, top, right - left, bottom - top);
        }
    }
}
