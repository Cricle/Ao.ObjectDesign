using System;

namespace Ao.ObjectDesign.Message
{
    public static class GuidToLongHelper
    {
        public static long Generate()
        {
            return BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        }
    }
}
