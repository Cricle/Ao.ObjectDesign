using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Wpf.Events;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Ao.ObjectDesign.Session.Wpf.Desiging
{
    public abstract class SessionManager<TScene, TSetting> : IEnumerable<KeyValuePair<Guid, IDesignSession<TScene, TSetting>>>, IDisposable
        where TScene : IDesignScene<TSetting>
    {
        private readonly Dictionary<Guid, IDesignSession<TScene, TSetting>> sessionMap;

        public IReadOnlyDictionary<Guid, IDesignSession<TScene, TSetting>> SessionMap => sessionMap;

        protected SessionManager(SceneEngine<TScene, TSetting> engine)
        {
            Engine = engine ?? throw new ArgumentNullException(nameof(engine));
            sessionMap = new Dictionary<Guid, IDesignSession<TScene, TSetting>>();
        }
        private bool isDisposed;

        public bool IsDisposed => isDisposed;

        public SceneEngine<TScene, TSetting> Engine { get; }

        public event EventHandler<SessionCreatedEventArgs<TScene, TSetting>> SessionCreated;
        public event EventHandler<SessionRemovedEventArgs<TScene, TSetting>> SessionRemoved;
        public event EventHandler CleanRemoved;

        public IDesignSession<TScene, TSetting> Create(IDesignSessionSettings<TScene, TSetting> settings)
        {
            ThrowIfDisposed();
            var session = MakeSession(settings);
            if (session is null)
            {
                throw new InvalidOperationException("Call MakeSession return null, it was not allow!");
            }
            sessionMap.Add(session.Id, session);
            OnCreatedSession(session);
            SessionCreated?.Invoke(this, new SessionCreatedEventArgs<TScene, TSetting>(session));
            return session;
        }

        public virtual void InitializeSession(IDesignSession<TScene, TSetting> session)
        {
            ThrowIfDisposed();
            session.Initialize();
        }

        public bool Remove(Guid id)
        {
            ThrowIfDisposed();
            if (sessionMap.TryGetValue(id, out var session))
            {
                OnRemovingSession(id, session);
                session.Dispose();
                sessionMap.Remove(id);
                OnRemovedSession(id, session);
                SessionRemoved?.Invoke(this, new SessionRemovedEventArgs<TScene, TSetting>(session));
                return true;
            }
            return false;
        }
        public void Clear()
        {
            ThrowIfDisposed();
            var ids = sessionMap.Keys.ToList();
            foreach (var item in ids)
            {
                Remove(item);
            }
            CleanRemoved?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnRemovingSession(Guid id, IDesignSession<TScene, TSetting> session)
        {

        }
        protected virtual void OnRemovedSession(Guid id, IDesignSession<TScene, TSetting> session)
        {

        }
        protected virtual void OnCreatedSession(IDesignSession<TScene, TSetting> session)
        {

        }

        protected abstract DesignSession<TScene, TSetting> MakeSession(IDesignSessionSettings<TScene, TSetting> settings);

        protected virtual WpfObjectDesignerSettings CreateDesignSettings()
        {
            Debug.Assert(Engine != null);
            return new WpfObjectDesignerSettings
            {
                UIGenerator = Engine.UIGenerator,
                Designer = Engine.InstanceDesigner,
                UIBuilder = Engine.ForViewBuilder,
                DataTemplateBuilder = Engine.DataTemplateBuilder,
                Sequencer = new CompiledPropertySequencer()
            };
        }

        public IEnumerator<KeyValuePair<Guid, IDesignSession<TScene, TSetting>>> GetEnumerator()
        {
            ThrowIfDisposed();
            return sessionMap.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                Clear();
                isDisposed = true;
            }
        }
        protected void ThrowIfDisposed()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }
    }
}
