using System;

namespace Ao.ObjectDesign.Designing.Level
{
    public abstract class SceneManager<TUI, TScene, TDesignObject>
        where TScene : IDeisgnScene<TDesignObject>
    {
        private TScene currentScene;
        private DesignSceneController<TUI, TDesignObject> currentSceneController;

        public virtual DesignSceneController<TUI, TDesignObject> CurrentSceneController => currentSceneController;

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
        public event EventHandler<CurrentSceneControllerChangedEventArgs<TUI, TDesignObject>> CurrentSceneControllerChanged;

        public abstract DesignSceneController<TUI, TDesignObject> CreateSceneController(TScene scene);

        private void BuildController()
        {
            var scene = currentScene;
            if (scene != null)
            {
                var old = currentSceneController;
                var controller = CreateSceneController(scene);
                currentSceneController = controller;
                var e = new CurrentSceneControllerChangedEventArgs<TUI, TDesignObject>(old, controller);
                OnCurrentSceneControllerChanged(e);
                controller.Initialize();
                CurrentSceneControllerChanged?.Invoke(this, e);
            }
        }

        protected virtual void OnCurrentSceneChanged(CurrentSceneChangedEventArgs<TDesignObject> e)
        {

        }
        protected virtual void OnCurrentSceneControllerChanged(CurrentSceneControllerChangedEventArgs<TUI, TDesignObject> e)
        {
            e.Old?.Dispose();
            e.New?.Initialize();
        }
    }
}
