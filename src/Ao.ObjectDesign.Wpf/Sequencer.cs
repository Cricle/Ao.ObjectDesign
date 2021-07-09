using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace Ao.ObjectDesign.Wpf
{
    [DebuggerDisplay("Undos = {Undos.Count}, Redos = {Redos.Count}")]
    public class Sequencer : NotifyObjectManager, ISequencer
    {
        [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
        readonly struct IgnoreIdentity : IEquatable<IgnoreIdentity>
        {
            public readonly object Instance;

            public readonly string PropertyName;

            public IgnoreIdentity(object instance, string propertyName)
            {
                Instance = instance;
                PropertyName = propertyName;
            }

            public bool Equals(IgnoreIdentity other)
            {
                return Instance == other.Instance &&
                    PropertyName == other.PropertyName;
            }
            public override bool Equals(object obj)
            {
                if (obj is IgnoreIdentity identity)
                {
                    return Equals(identity);
                }
                return false;
            }
            public override string ToString()
            {
                return $"{{{Instance}, {PropertyName}}}";
            }

            public override int GetHashCode()
            {
                return Instance.GetHashCode() ^ PropertyName.GetHashCode();
            }

            private string GetDebuggerDisplay()
            {
                return ToString();
            }
        }
        public CommandWays<ModifyDetail> Undos { get; } = new CommandWays<ModifyDetail>();

        public CommandWays<ModifyDetail> Redos { get; } = new CommandWays<ModifyDetail>();

        private readonly HashSet<IgnoreIdentity> ignores = new HashSet<IgnoreIdentity>();

        protected virtual void OnReset(ModifyDetail detail)
        {
            var prop = detail.Instance.GetType().GetProperty(detail.PropertyName);
            prop.SetValue(detail.Instance, detail.From);
        }
        private void ResetValue(ModifyDetail detail)
        {
            var identity = new IgnoreIdentity(detail.Instance, detail.PropertyName);
            try
            {
                ignores.Add(identity);
                OnReset(detail);
            }
            finally
            {
                ignores.Remove(identity);
            }
        }
        public void Undo()
        {
            Undo(true);
        }
        public void CleanAllRecords()
        {
            Undos.Clear();
            Redos.Clear();
        }
        public virtual void Undo(bool pushRedo)
        {
            if (Undos.Count != 0)
            {
                var l = Undos.Pop();
                Debug.Assert(l != null);
                ResetValue(l);
                if (pushRedo)
                {
                    var rev = l.Reverse();
                    Redos.Push(rev, false);
                }
            }
        }
        public void Redo()
        {
            Redo(true);
        }
        public virtual void Redo(bool pushUndo)
        {
            if (Redos.Count != 0)
            {
                var l = Redos.Pop();
                Debug.Assert(l != null);
                ResetValue(l);
                if (pushUndo)
                {
                    var rev = l.Reverse();
                    Undos.Push(rev, false);
                }
            }
        }
        protected override void OnClearNotifyer(IReadOnlyHashSet<INotifyPropertyChangeTo> notifies)
        {
            foreach (var item in notifies)
            {
                item.PropertyChangeTo -= OnNotifyPropertyChangingPropertyChanging;
            }
        }
        protected virtual void OnNotifyPropertyChangingPropertyChanging(object sender, PropertyChangeToEventArgs e)
        {
            var identity = new IgnoreIdentity(sender, e.PropertyName);
            if (!ignores.Contains(identity))
            {
                var detail = new ModifyDetail(sender, e.PropertyName, e.From, e.To);
                Undos.Push(detail);
            }
        }
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
