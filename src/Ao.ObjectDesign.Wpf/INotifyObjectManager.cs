namespace Ao.ObjectDesign.Wpf
{
    public interface INotifyObjectManager
    {
        IReadOnlyHashSet<INotifyPropertyChangeTo> Listenings { get; }

        int ListeningCount { get; }

        bool IsAttacked(INotifyPropertyChangeTo notifyPropertyChangeTo);

        void ClearNotifyer();

        bool Attack(INotifyPropertyChangeTo notifyPropertyChangeTo);

        void Strip(INotifyPropertyChangeTo notifyPropertyChangeTo);
    }
}
