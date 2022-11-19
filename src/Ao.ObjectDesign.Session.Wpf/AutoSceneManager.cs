using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Wpf.Controllers;
using Ao.ObjectDesign.Data;
using System.Collections;
using System.Windows;

namespace Ao.ObjectDesign.Session.Wpf
{
    public class AutoSceneManager<TScene, TSetting> : WpfSceneManager<TScene, TSetting>
        where TScene : IObservableDesignScene<TSetting>
    {
        public AutoSceneManager(IDesignPackage<UIElement, TSetting, IWithSourceBindingScope> designMap,
            IList uIElements)
            : base(designMap, uIElements)
        {
        }


        public new WpfSceneController<TSetting> CurrentSceneController => (WpfSceneController<TSetting>)base.CurrentSceneController;

        public override DesignSceneController<UIElement, TSetting> CreateSceneController(TScene scene)
        {
            var controller = new WpfSceneItemsSceneController<TSetting>(DesignMap,
                UIElements,
                scene)
            {
                LazyBinding = LazyBinding
            };
            return controller;
        }
    }
}
