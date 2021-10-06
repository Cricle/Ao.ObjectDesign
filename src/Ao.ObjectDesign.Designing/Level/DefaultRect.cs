using System;

namespace Ao.ObjectDesign.Designing.Level
{
    [Serializable]
    public class DefaultRect : IRect, IEquatable<DefaultRect>
    {
        public static readonly DefaultRect Zero = new DefaultRect(0, 0, 0, 0);

        public DefaultRect(double left, double top, double right, double bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;

            if (right - left < 0)
            {
                throw new ArgumentException("Width must more or equals than one");
            }
            if (bottom - top < 0)
            {
                throw new ArgumentException("Height must more or equals than one");
            }
        }

        public double Left { get; }

        public double Top { get; }

        public double Right { get; }

        public double Bottom { get; }

        public double Height => Bottom - Top;

        public double Width => Right - Left;

        public bool Equals(DefaultRect other)
        {
            if (other is null)
            {
                return false;
            }
            return other.Left == Left &&
                other.Top == Top &&
                other.Right == Right &&
                other.Bottom == Bottom;
        }
        public override bool Equals(object obj)
        {
            if (obj is DefaultRect rect)
            {
                return Equals(rect);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Left.GetHashCode() ^ Right.GetHashCode() ^
                Top.GetHashCode() ^ Bottom.GetHashCode();
        }
        public override string ToString()
        {
            return $"{{Left:{Left}, Top:{Top}, Right:{Right}, Bottom:{Bottom}}}";
        }
        public bool Contains(double x, double y)
        {
            // We include points on the edge as "contained".
            // We do "x - _width <= _x" instead of "x <= _x + _width"
            // so that this check works when _width is PositiveInfinity
            // and _x is NegativeInfinity.
            return ((x >= Left) && (x - (Right - Left) <= Left) &&
                    (y >= Top) && (y - (Bottom - Top) <= Top));
        }
        public bool Contains(IRect other)
        {
            if (other is null)
            {
                return false;
            }
            return (Left <= other.Left &&
                    Top <= other.Top &&
                    Left + (Right - Left) >= other.Left + (other.Right - other.Left) &&
                    Top + (Bottom - Top) >= other.Top + (other.Bottom - other.Top));
        }
        public bool IntersectsWith(IRect other)
        {
            if (other is null)
            {
                return false;
            }
            return (other.Left <= Right) &&
                   (other.Right >= Left) &&
                   (other.Top <= Bottom) &&
                   (other.Bottom >= Top);
        }
    }
}
