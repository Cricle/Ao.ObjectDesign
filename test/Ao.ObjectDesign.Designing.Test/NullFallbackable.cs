namespace Ao.ObjectDesign.Designing.Test
{
    internal class NullFallbackable : IFallbackable
    {
        public FallbackModes Mode { get; }

        public IFallbackable Copy(FallbackModes? mode)
        {
            return null;
        }

        public void Fallback()
        {
        }

        public bool IsReverse(IFallbackable fallbackable)
        {
            return false;
        }

        public IFallbackable Reverse()
        {
            return null;
        }
    }
}
