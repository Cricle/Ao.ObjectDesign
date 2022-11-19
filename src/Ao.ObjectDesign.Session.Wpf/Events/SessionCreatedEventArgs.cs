using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Wpf.Desiging;
using System;

namespace Ao.ObjectDesign.Session.Wpf.Events
{
    public class SessionCreatedEventArgs<TScene, TSetting> : EventArgs
        where TScene : IDesignScene<TSetting>
    {
        public SessionCreatedEventArgs(IDesignSession<TScene, TSetting> session)
        {
            Session = session;
        }

        public IDesignSession<TScene, TSetting> Session { get; }
    }
}
