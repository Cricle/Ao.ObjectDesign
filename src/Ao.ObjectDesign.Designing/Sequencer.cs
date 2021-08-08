using System;
using System.Diagnostics;

namespace Ao.ObjectDesign.Designing
{
    [DebuggerDisplay("Undos = {Undos.Count}, Redos = {Redos.Count}, ListeningCount = {ListeningCount}")]
    public abstract class Sequencer<TFallback> : NotifyObjectManager, IActionSequencer<TFallback>
        where TFallback : IFallbackable
    {
        protected Sequencer()
        {
        }

        private ICommandWays<TFallback> undos;
        private ICommandWays<TFallback> redos;

        public ICommandWays<TFallback> Undos
        {
            get
            {
                if (undos is null)
                {
                    undos = CreateCommandWays();
                }
                return undos;
            }
        }

        public ICommandWays<TFallback> Redos
        {
            get
            {
                if (redos is null)
                {
                    redos = CreateCommandWays();
                }
                return redos;
            }
        }

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
            if (succeed &&
                Undos.First != null &&
                Undos.First.IsReverse(fallback))
            {
                Undos.Pop(false);
            }
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
                BeginRedo(l);
                bool succeed = false;
                try
                {
                    l.Fallback();
                    succeed = false;
                }
                catch (Exception ex)
                {
                    RedoFail(l, ex);
                }
                EndRedo(l, succeed);
                if (pushUndo)
                {
                    TFallback rev = Reverse(l);
                    Undos.Push(rev, false);
                }
            }
        }
        protected virtual void BeginRedo(TFallback fallback)
        {

        }
        protected virtual void EndRedo(TFallback fallback, bool succeed)
        {
            if (succeed &&
                Redos.First != null &&
                Redos.First.IsReverse(fallback))
            {
                Redos.Pop(false);
            }
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
