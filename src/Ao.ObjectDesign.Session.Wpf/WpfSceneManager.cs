using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Data;
using System;
using System.Collections;
using System.Windows;

namespace Ao.ObjectDesign.Session.Wpf
{
    public abstract class WpfSceneManager<TScene, TSetting> : SceneManager<UIElement, TScene, TSetting>
        where TScene : IDesignScene<TSetting>
    {
        protected WpfSceneManager(IDesignPackage<UIElement, TSetting, IWithSourceBindingScope> designMap,
            IList uiElements)
        {
            DesignMap = designMap ?? throw new ArgumentNullException(nameof(designMap));
            UIElements = uiElements ?? throw new ArgumentNullException(nameof(uiElements));
        }

        public bool LazyBinding { get; set; }

        public IDesignPackage<UIElement, TSetting, IWithSourceBindingScope> DesignMap { get; }

        public IList UIElements { get; }

        protected override void OnCurrentSceneChanged(CurrentSceneChangedEventArgs<TSetting> e)
        {
            UIElements.Clear();
        }
    }
}
