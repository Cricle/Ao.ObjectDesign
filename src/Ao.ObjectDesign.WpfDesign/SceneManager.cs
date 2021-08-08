using Ao.ObjectDesign.WpfDesign.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class SceneManager<TDesignObject>
    {

        //TODO: some actions

        public abstract DesignSceneController<TDesignObject> CreateSceneController(IDeisgnScene<TDesignObject> name);
    }
}
