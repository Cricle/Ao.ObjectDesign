using Ao.ObjectDesign.Designing.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Session.Desiging
{
    public class EnvironmentSessionManager<TScene, TSetting> : SessionManager<TScene, TSetting>
        where TScene : IObservableDesignScene<TSetting>
    {
        public EnvironmentSessionManager(SceneEngine<TScene, TSetting> engine,
            TemplateContextsDecoraterCollection<TScene, TSetting> decoraters)
            : base(engine)
        {
            Decoraters = decoraters ?? throw new ArgumentNullException(nameof(decoraters));
        }


        public TemplateContextsDecoraterCollection<TScene, TSetting> Decoraters { get; }

        protected override DesignSession<TScene, TSetting> MakeSession(IDesignSessionSettings<TScene, TSetting> settings)
        {
            var ds = CreateDesignSettings();
            return new EnvironmentDesignSession<TScene, TSetting>(settings, ds, Decoraters);
        }
    }
}
