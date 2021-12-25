namespace Ao.ObjectDesign.Session
{
    public interface IInitableObject
    {
        bool IsInitialized { get; }

        void Initialize();
    }
}
