using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign.Wpf
{
    public class NotifyObjectManager: INotifyObjectManager
    {
        private readonly HashSet<INotifyPropertyChangeTo> listenings = new HashSet<INotifyPropertyChangeTo>();

        public IReadOnlyHashSet<INotifyPropertyChangeTo> Listenings => new ReadOnlyHashSet<INotifyPropertyChangeTo>(listenings);

        public int ListeningCount => listenings.Count;

        public bool IsAttacked(INotifyPropertyChangeTo notifyPropertyChangeTo)
        {
            return listenings.Contains(notifyPropertyChangeTo);
        }
        public void ClearNotifyer()
        {
            var ds = Listenings;
            listenings.Clear();
            OnClearNotifyer(ds);
        }
        protected virtual void OnClearNotifyer(IReadOnlyHashSet<INotifyPropertyChangeTo> notifies)
        {

        }

        public bool Attack(INotifyPropertyChangeTo notifyPropertyChangeTo)
        {
            if (listenings.Add(notifyPropertyChangeTo))
            {
                OnAttack(notifyPropertyChangeTo);
                return true;
            }
            return false;
        }

        protected virtual void OnAttack(INotifyPropertyChangeTo notifyPropertyChangeTo)
        {
        }

        public void Strip(INotifyPropertyChangeTo notifyPropertyChangeTo)
        {
            if (listenings.Remove(notifyPropertyChangeTo))
            {
                OnStrip(notifyPropertyChangeTo);
            }
        }
        protected virtual void OnStrip(INotifyPropertyChangeTo notifyPropertyChangeTo)
        {
        }

        
    }
}
