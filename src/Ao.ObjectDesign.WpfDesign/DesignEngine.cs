﻿using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.WpfDesign.Input;
using Ao.ObjectDesign.Designing.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public abstract class DesignEngine<TScene,TDesignObject>
        where TScene:IObservableDeisgnScene<TDesignObject>
    {
        private bool isInitialized;
        private WpfObjectDesigner wpfObjectDesigner;
        private IActionSequencer<IModifyDetail> sequencer;
        private AccessInputBindings keyboardBindings;
        private UIDesignMap uiDesignMap;
        private SceneManager<UIElement,TScene, TDesignObject> sceneManager;


        public bool IsInitialized => isInitialized;

        public WpfObjectDesigner WpfObjectDesigner => wpfObjectDesigner;

        public IActionSequencer<IModifyDetail> Sequencer => sequencer;

        public AccessInputBindings KeyboardBindings => keyboardBindings;

        public IInputElement InputElement { get; }

        public UIDesignMap UIDesignMap => uiDesignMap;

        public SceneManager<UIElement, TScene, TDesignObject> SceneManager => sceneManager;

        public DesignEngine(IInputElement inputElement)
        {
            InputElement = inputElement ?? throw new ArgumentNullException(nameof(inputElement));
        }

        public void Initialize()
        {
            if (isInitialized)
            {
                return;
            }
            keyboardBindings = CreateKeyboardBindings();
            uiDesignMap = CreateUIDesignMap();
            wpfObjectDesigner = CreateWpfObjectDesigner();
            sequencer = CreateSequencer(wpfObjectDesigner);
            sceneManager = CreateSceneManager(wpfObjectDesigner, uiDesignMap);
            OnInitialize();
            isInitialized = true;
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

        protected virtual AccessInputBindings CreateKeyboardBindings()
        {
            return new AccessInputBindings(InputElement);
        }

        protected virtual UIDesignMap CreateUIDesignMap()
        {
            return new UIDesignMap();
        }

        protected abstract SceneManager<UIElement, TScene, TDesignObject> CreateSceneManager(WpfObjectDesigner designer, UIDesignMap designMap);

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
