﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace Ao.ObjectDesign.Wpf
{
    [DebuggerDisplay("Undos = {Undos.Count}, Redos = {Redos.Count}, ListeningCount = {ListeningCount}")]
    public class Sequencer : NotifyObjectManager, ISequencer
    {
        public Sequencer()
        {
            Undos = CreateCommandWays();
            Redos = CreateCommandWays();
        }
        public ICommandWays<IModifyDetail> Undos { get; }

        public ICommandWays<IModifyDetail> Redos { get; }

        private readonly HashSet<IgnoreIdentity> ignores = new HashSet<IgnoreIdentity>();

        protected virtual ICommandWays<IModifyDetail> CreateCommandWays()
        {
            return new CommandWays<IModifyDetail>();
        }

        protected virtual void OnReset(IModifyDetail detail)
        {
            var prop = detail.Instance.GetType().GetProperty(detail.PropertyName);
            prop.SetValue(detail.Instance, detail.From);
        }
        private void ResetValue(IModifyDetail detail)
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
            Undos.Clear(true);
            Redos.Clear(true);
        }
        public virtual void Undo(bool pushRedo)
        {
            if (Undos.Count != 0)
            {
                var l = Undos.Pop(true);
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
                var l = Redos.Pop(true);
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
                Undos.Push(detail, true);
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