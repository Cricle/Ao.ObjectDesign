using System;
using System.Diagnostics;

namespace Ao.ObjectDesign.Designing
{
    [DebuggerDisplay("Undos = {Undos.Count}, Redos = {Redos.Count}, ListeningCount = {ListeningCount}")]
    public abstract class Sequencer<TFallback> : NotifyObjectManager, IActionSequencer<TFallback>, IUndoRedo
        where TFallback : IFallbackable
    {
        public Sequencer()
        {
            Undos = CreateCommandWays();
            Redos = CreateCommandWays();
        }

        public ICommandWays<TFallback> Undos { get; }

        public ICommandWays<TFallback> Redos { get; }

        public bool CanUndo => Undos.Count > 0;

        public bool CanRedo => Redos.Count > 0;

        protected virtual ICommandWays<TFallback> CreateCommandWays()
        {
            return new CommandWays<TFallback>();
        }
        protected virtual bool IsSequenceIgnore(object sender, PropertyChangeToEventArgs e)
        {
            return false;
        }
        public void Undo()
        {
            Undo(true);
        }
        public void CleanAllRecords()
        {
            Undos.Clear(true);
            Redos.Clear(true);
        }
        public virtual void Undo(bool pushRedo)
        {
            if (Undos.Count != 0)
            {
                TFallback l = Undos.Pop(true);
                Debug.Assert(l != null);
                BeginUndo(l);
                bool succeed = false;
                try
                {
                    l.Fallback();
                    succeed = true;
                }
                catch (Exception ex)
                {
                    UndoFail(l, ex);
                }
                EndUndo(l, succeed);
                if (pushRedo)
                {
                    TFallback rev = Reverse(l);
                    Redos.Push(rev, false);
                }
            }
        }
        protected virtual void BeginUndo(TFallback fallback)
        {

        }
        protected virtual void EndUndo(TFallback fallback, bool succeed)
        {

        }
        protected virtual void UndoFail(TFallback fallback, Exception exception)
        {

        }
        protected abstract TFallback Reverse(TFallback fallback);
        public void Redo()
        {
            Redo(true);
        }
        public virtual void Redo(bool pushUndo)
        {
            if (Redos.Count != 0)
            {
                TFallback l = Redos.Pop(true);
                Debug.Assert(l != null);
                CoreRedo(l, pushUndo);
            }
        }
        private void CoreRedo(TFallback detail, bool pushUndo)
        {
            BeginRedo(detail);
            bool succeed = false;
            try
            {
                detail.Fallback();
                succeed = false;
            }
            catch (Exception ex)
            {
                RedoFail(detail, ex);
            }
            EndRedo(detail, succeed);
            if (pushUndo)
            {
                TFallback rev = Reverse(detail);
                Undos.Push(rev, false);
            }
        }
        protected virtual void BeginRedo(TFallback fallback)
        {

        }
        protected virtual void EndRedo(TFallback fallback, bool succeed)
        {

        }
        protected virtual void RedoFail(TFallback fallback, Exception exception)
        {

        }
        protected override void OnClearNotifyer(IReadOnlyHashSet<INotifyPropertyChangeTo> notifies)
        {
            foreach (INotifyPropertyChangeTo item in notifies)
            {
                item.PropertyChangeTo -= OnNotifyPropertyChangingPropertyChanging;
            }
        }
        protected virtual void OnNotifyPropertyChangingPropertyChanging(object sender, PropertyChangeToEventArgs e)
        {
            if (!IsSequenceIgnore(sender, e))
            {
                TFallback detail = CreateFallback(sender, e);
                Undos.Push(detail, true);
            }
        }
        protected abstract TFallback CreateFallback(object sender, PropertyChangeToEventArgs e);

        protected override void OnStrip(INotifyPropertyChangeTo notifyPropertyChangeTo)
        {
            notifyPropertyChangeTo.PropertyChangeTo -= OnNotifyPropertyChangingPropertyChanging;
        }
        protected override void OnAttack(INotifyPropertyChangeTo notifyPropertyChangeTo)
        {
            notifyPropertyChangeTo.PropertyChangeTo += OnNotifyPropertyChangingPropertyChanging;
        }
    }
}
