namespace Ao.ObjectDesign.Designing.Test
{
    internal class ValueFallback : IFallbackable
    {
        public FallbackModes Mode { get; set; }

        public IFallbackable Copy(FallbackModes? mode)
        {
            return new ValueFallback
            {
                Mode = mode ?? Mode
            };
        }

        public bool CallFallback { get; set; }
        public void Fallback()
        {
            CallFallback = true;
        }

        public bool IsReverse(IFallbackable fallbackable)
        {
            return fallbackable.Mode != Mode;
        }

        public IFallbackable Reverse()
        {
            return new ValueFallback { Mode = Mode == FallbackModes.Forward ? FallbackModes.Reverse : FallbackModes.Forward };
        }
    }
}
