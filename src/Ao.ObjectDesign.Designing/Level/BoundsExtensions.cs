using System;
using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign.Designing.Level
{
    public static class BoundsExtensions
    {
        public static IRect OutterActual<TUI, TDesignObject>(this IEnumerable<IElementBounds<TUI, TDesignObject>> elementBounds)
        {
            return Outter(elementBounds.Select(x => x.ActualBounds));
        }
        public static IRect Outter<TUI, TDesignObject>(this IEnumerable<IElementBounds<TUI, TDesignObject>> elementBounds)
        {
            return Outter(elementBounds.Select(x => x.Bounds));
        }
        public static IRect Outter(this IEnumerable<IRect> rects)
        {
            if (rects is null)
            {
                throw new ArgumentNullException(nameof(rects));
            }

            if (!rects.Any())
            {
                return DefaultRect.Zero;
            }
            double left = double.PositiveInfinity;
            double top = double.PositiveInfinity;
            double right = double.NegativeInfinity;
            double bottom = double.NegativeInfinity;

            foreach (var item in rects)
            {
                left = Math.Min(left, item.Left);
                top = Math.Min(top, item.Top);
                right = Math.Max(right, item.Right);
                bottom = Math.Max(bottom, item.Bottom);
            }
            return new DefaultRect(left, top, right, bottom);
        }
    }
}
