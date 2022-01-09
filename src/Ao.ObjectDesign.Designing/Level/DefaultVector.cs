using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Ao.ObjectDesign.Designing.Level
{
    [Serializable]
    public struct DefaultVector : IVector, IEquatable<DefaultVector>
    {
        public static readonly DefaultVector Zero = new DefaultVector(0, 0);
        public static readonly DefaultVector One = new DefaultVector(1, 1);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DefaultVector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; }

        public double Y { get; }

        public override int GetHashCode()
        {
            unchecked
            {
                var h = 17 * 31 + X.GetHashCode();
                h = 31 * h + Y.GetHashCode();
                return h;
            }
        }

        public Vector<double> ToVector2d()
        {
            return new Vector<double>(new double[] { X, Y });
        }

        public override bool Equals(object obj)
        {
            if (obj is DefaultVector vector)
            {
                return Equals(vector);
            }
            return false;
        }
        public override string ToString()
        {
            return $"{{X:{X}, Y:{Y}}}";
        }

        public bool Equals(DefaultVector other)
        {
            return other.X == X && other.Y == Y;
        }
    }
}
