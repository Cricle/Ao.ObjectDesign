using System.Runtime.CompilerServices;
using ValueBuffer;

namespace ObjectDesign.Brock
{
    internal static class ArrayBuffer
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueList<T> Create<T>()
        {
            return new ValueList<T>();
        }
    }
}
