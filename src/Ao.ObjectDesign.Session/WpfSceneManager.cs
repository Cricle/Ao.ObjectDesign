using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.WpfDesign;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace Ao.ObjectDesign.Session
{
    public abstract class WpfSceneManager<TScene,TSetting> : SceneManager<UIElement, TScene, TSetting>
        where TScene : IDesignScene<TSetting>
    {
        protected WpfSceneManager(IDesignPackage<TSetting> designMap,
            IList uiElements)
        {
            DesignMap = designMap ?? throw new ArgumentNullException(nameof(designMap));
            UIElements = uiElements ?? throw new ArgumentNullException(nameof(uiElements));
        }

        public bool LazyBinding { get; set; }

        public IDesignPackage<TSetting> DesignMap { get; }

        public IList UIElements { get; }

        protected override void OnCurrentSceneChanged(CurrentSceneChangedEventArgs<TSetting> e)
        {
            UIElements.Clear();
        }
    }
}
