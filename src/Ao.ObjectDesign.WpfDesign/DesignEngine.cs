using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.WpfDesign.Input;
using Ao.ObjectDesign.WpfDesign.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class DesignEngine<TScene,TDesignObject>
        where TScene:IDeisgnScene<TDesignObject>
    {
        private WpfObjectDesigner wpfObjectDesigner;
        private IActionSequencer<IModifyDetail> sequencer;
        private KeyboardBindings keyboardBindings;
        private UIDesignMap uiDesignMap;
        private SceneManager<TScene, TDesignObject> sceneManager;

        public WpfObjectDesigner WpfObjectDesigner => wpfObjectDesigner;

        public IActionSequencer<IModifyDetail> Sequencer => sequencer;

        public KeyboardBindings KeyboardBindings => keyboardBindings;

        public IInputElement InputElement { get; }

        public UIDesignMap UIDesignMap => uiDesignMap;

        public SceneManager<TScene, TDesignObject> SceneManager => sceneManager;

        public DesignEngine(IInputElement inputElement)
        {
            InputElement = inputElement ?? throw new ArgumentNullException(nameof(inputElement));
        }

        public void Initialize()
        {
            keyboardBindings = CreateKeyboardBindings();
            uiDesignMap = CreateUIDesignMap();
            wpfObjectDesigner = CreateWpfObjectDesigner();
            sequencer = CreateSequencer(wpfObjectDesigner);
            sceneManager = CreateSceneManager(wpfObjectDesigner, uiDesignMap);
            OnInitialize();
        }

        protected virtual void OnInitialize()
        {

        }

        protected virtual IActionSequencer<IModifyDetail> CreateSequencer(WpfObjectDesigner designer)
        {
            return designer.Sequencer;
        }

        protected virtual WpfObjectDesigner CreateWpfObjectDesigner()
        {
            return new WpfObjectDesigner(true);
        }

        protected virtual KeyboardBindings CreateKeyboardBindings()
        {
            return new KeyboardBindings(InputElement);
        }

        protected virtual UIDesignMap CreateUIDesignMap()
        {
            return new UIDesignMap();
        }

        protected abstract SceneManager<TScene, TDesignObject> CreateSceneManager(WpfObjectDesigner designer, UIDesignMap designMap);

        public void Dispose()
        {
            KeyboardBindings.Dispose();
            OnDispose();
        }

        protected virtual void OnDispose()
        {

        }
    }
}
