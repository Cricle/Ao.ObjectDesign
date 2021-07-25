using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing
{
    public class NotifyObjectManager : INotifyObjectManager
    {
        private readonly HashSet<INotifyPropertyChangeTo> listenings = new HashSet<INotifyPropertyChangeTo>();

        public IReadOnlyHashSet<INotifyPropertyChangeTo> Listenings => new ReadOnlyHashSet<INotifyPropertyChangeTo>(listenings);

        public int ListeningCount => listenings.Count;

        public bool IsAttacked(INotifyPropertyChangeTo notifyPropertyChangeTo)
        {
            if (notifyPropertyChangeTo is null)
            {
                throw new ArgumentNullException(nameof(notifyPropertyChangeTo));
            }

            return listenings.Contains(notifyPropertyChangeTo);
        }

        public void ClearNotifyer()
        {
            IReadOnlyHashSet<INotifyPropertyChangeTo> ds = Listenings;
            listenings.Clear();
            OnClearNotifyer(ds);
        }
        protected virtual void OnClearNotifyer(IReadOnlyHashSet<INotifyPropertyChangeTo> notifies)
        {

        }

        public bool Attack(INotifyPropertyChangeTo notifyPropertyChangeTo)
        {
            if (notifyPropertyChangeTo is null)
            {
                throw new ArgumentNullException(nameof(notifyPropertyChangeTo));
            }

            bool succeed = listenings.Add(notifyPropertyChangeTo);

            if (succeed)
            {
                OnAttack(notifyPropertyChangeTo);
            }
            return succeed;
        }

        protected virtual void OnAttack(INotifyPropertyChangeTo notifyPropertyChangeTo)
        {
        }

        public void Strip(INotifyPropertyChangeTo notifyPropertyChangeTo)
        {
            if (notifyPropertyChangeTo is null)
            {
                throw new ArgumentNullException(nameof(notifyPropertyChangeTo));
            }

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
