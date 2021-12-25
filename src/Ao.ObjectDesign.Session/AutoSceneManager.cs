using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Controllers;
using Ao.ObjectDesign.WpfDesign;
using System.Collections;
using System.Windows;

namespace Ao.ObjectDesign.Session
{
    public class AutoSceneManager<TScene, TSetting> : WpfSceneManager<TScene,TSetting>
        where TScene:IObservableDesignScene<TSetting>
    {
        public AutoSceneManager(IDesignPackage<TSetting> designMap,
            IList uIElements)
            : base(designMap, uIElements)
        {
        }


        public new WpfSceneController<TSetting> CurrentSceneController => (WpfSceneController<TSetting>)base.CurrentSceneController;

        public override DesignSceneController<UIElement, TSetting> CreateSceneController(TScene scene)
        {
            var controller = new WpfItemsSceneController<TSetting>(DesignMap, UIElements, scene) { LazyBinding = LazyBinding };
            return controller;
        }
    }
}
