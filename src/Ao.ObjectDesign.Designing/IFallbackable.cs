using System;

namespace Ao.ObjectDesign.Designing
{
    public interface IFallbackable
    {
        FallbackMode Mode { get; }

        void Fallback();

        IFallbackable Reverse();

        IFallbackable Copy(FallbackMode? mode);
    }
}