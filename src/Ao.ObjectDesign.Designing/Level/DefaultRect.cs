using System;

namespace Ao.ObjectDesign.Designing.Level
{
    [Serializable]
    public struct DefaultRect : IRect, IEquatable<DefaultRect>
    {
        public static readonly DefaultRect Zero = new DefaultRect(0, 0, 0, 0);

        public DefaultRect(double left, double top, double right, double bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public double Left { get; }

        public double Top { get; }

        public double Right { get; }

        public double Bottom { get; }

        public bool Equals(DefaultRect other)
        {
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
    }
}
