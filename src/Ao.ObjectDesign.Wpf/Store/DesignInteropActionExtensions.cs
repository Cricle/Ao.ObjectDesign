using System;
using System.IO;

namespace Ao.ObjectDesign.Wpf.Store
{
    public static class DesignInteropActionExtensions
    {
        public static void SerializeToStream<T>(this IDesignStreamInterop interop,T val,Stream stream)
        {
            if (interop is null)
            {
                throw new ArgumentNullException(nameof(interop));
            }

            interop.SerializeToStream(val,typeof(T), stream);
        }
        public static string SerializeToString<T>(this IDesignStringInterop interop, T val)
        {
            if (interop is null)
            {
                throw new ArgumentNullException(nameof(interop));
            }

            return interop.SerializeToString(val, typeof(T));
        }
        public static byte[] SerializeToByte<T>(this IDesignByteInterop interop, T val)
        {
            if (interop is null)
            {
                throw new ArgumentNullException(nameof(interop));
            }

            return interop.SerializeToByte(val, typeof(T));
        }
        public static object DeserializeByByte<T>(this IDesignByteInterop interop, byte[] data)
        {
            if (interop is null)
            {
                throw new ArgumentNullException(nameof(interop));
            }

            return interop.DeserializeByByte(data, typeof(T));
        }
        public static object DeserializeByStream<T>(this IDesignStreamInterop interop, Stream stream)
        {
            if (interop is null)
            {
                throw new ArgumentNullException(nameof(interop));
            }

            return interop.DeserializeByStream(stream,typeof(T));
        }
        public static object DeserializeByString<T>(this IDesignStringInterop interop, string str)
        {
            if (interop is null)
            {
                throw new ArgumentNullException(nameof(interop));
            }

            return interop.DeserializeByString(str, typeof(T));
        }
    }
}
