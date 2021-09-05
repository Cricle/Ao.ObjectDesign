namespace Ao.ObjectDesign.Designing.Level
{
    //https://source.dot.net/#WindowsBase/System/Windows/Rect.cs,2c7bd202993dcfdc
    public static class RectActionsExtensions
    {
        public static bool Contains(this IRect rect, IRect other)
        {
            if (rect is null || other is null)
            {
                return false;
            }

            return (rect.Left <= other.Left &&
                    rect.Top <= other.Top &&
                    rect.Left + (rect.Right - rect.Left) >= other.Left + (other.Right - other.Left) &&
                    rect.Top + (rect.Bottom - rect.Top) >= other.Top + (other.Bottom - other.Top));
        }
        public static bool IntersectsWith(this IRect rect,IRect other)
        {
            if (rect is null || other is null)
            {
                return false;
            }

            return (other.Left <= rect.Right) &&
                   (other.Right >= rect.Left) &&
                   (other.Top <= rect.Bottom) &&
                   (other.Bottom >= rect.Top);
        }
        public static bool Contains(this IRect rect,double x, double y)
        {
            if (rect is null)
            {
                return false;
            }
            // We include points on the edge as "contained".
            // We do "x - _width <= _x" instead of "x <= _x + _width"
            // so that this check works when _width is PositiveInfinity
            // and _x is NegativeInfinity.
            return ((x >= rect.Left) && (x - (rect.Right-rect.Left) <= rect.Left) &&
                    (y >= rect.Top) && (y - (rect.Bottom - rect.Top) <= rect.Top));
        }
    }
    public interface IRect
    {
        double Left { get; }

        double Top { get; }

        double Right { get; }

        double Bottom { get; }
    }
}
