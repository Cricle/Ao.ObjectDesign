using Ao.ObjectDesign.WpfDesign.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class SceneManager<TScene, TDesignObject>
        where TScene : IDeisgnScene<TDesignObject>
    {
        private TScene currentScene;
        private DesignSceneController<TDesignObject> currentSceneController;

        public virtual DesignSceneController<TDesignObject> CurrentSceneController => currentSceneController;

        public virtual TScene CurrentScene
        {
            get => currentScene;
            set
            {
                var old = currentScene;
                currentScene = value;
                var e = new CurrentSceneChangedEventArgs<TDesignObject>(old, value);
                OnCurrentSceneChanged(e);
                CurrentSceneChanged?.Invoke(this, e);
                BuildController();
            }
        }

        public event EventHandler<CurrentSceneChangedEventArgs<TDesignObject>> CurrentSceneChanged;
        public event EventHandler<CurrentSceneControllerChangedEventArgs<TDesignObject>> CurrentSceneControllerChanged;

        public abstract DesignSceneController<TDesignObject> CreateSceneController(TScene scene);

        private void BuildController()
        {
            var scene = currentScene;
            if (scene != null)
            {
                var old = currentSceneController;
                var controller = CreateSceneController(scene);
                currentSceneController = controller;
                controller.Initialize();
                var e = new CurrentSceneControllerChangedEventArgs<TDesignObject>(old, controller);
                OnCurrentSceneControllerChanged(e);
                CurrentSceneControllerChanged?.Invoke(this, e);
            }
        }

        protected virtual void OnCurrentSceneChanged(CurrentSceneChangedEventArgs<TDesignObject> e)
        {

        }
        protected virtual void OnCurrentSceneControllerChanged(CurrentSceneControllerChangedEventArgs<TDesignObject> e)
        {

        }
    }
}
