namespace Ao.ObjectDesign.Designing
{
    public interface IFallbackable
    {
        FallbackModes Mode { get; }

        void Fallback();

        bool IsReverse(IFallbackable fallbackable);

        IFallbackable Reverse();

        IFallbackable Copy(FallbackModes? mode);
    }
}