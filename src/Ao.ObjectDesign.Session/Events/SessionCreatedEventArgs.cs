using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Desiging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Session.Events
{
    public class SessionCreatedEventArgs<TScene,TSetting> : EventArgs
        where TScene:IDesignScene<TSetting>
    {
        public SessionCreatedEventArgs(IDesignSession<TScene,TSetting> session)
        {
            Session = session;
        }

        public IDesignSession<TScene, TSetting> Session { get; }
    }
}
