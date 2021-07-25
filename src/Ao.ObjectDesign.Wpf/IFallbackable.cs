using System;

namespace Ao.ObjectDesign.Wpf
{
    public interface IFallbackable
    {
        FallbackMode Mode { get; }

        void Fallback();

        IFallbackable Reverse();

        IFallbackable Copy(FallbackMode? mode);
    }
}