namespace Ao.ObjectDesign.Designing
{
    public interface IInitableObject
    {
        bool IsInitialized { get; }

        void Initialize();
    }
}
