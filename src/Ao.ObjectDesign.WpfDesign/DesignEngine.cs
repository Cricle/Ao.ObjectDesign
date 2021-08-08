using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.WpfDesign.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ao.ObjectDesign.WpfDesign
{
    public class DesignEngine<TDesignObject>
    {
        public WpfObjectDesigner WpfObjectDesigner { get; }

        public IActionSequencer<IModifyDetail> Sequencer { get; }

        public KeyboardBindings KeyboardBindings { get; }

        public IInputElement InputElement { get; }

        public UIDesignMap UIDesignMap { get; }

        public SceneManager<TDesignObject> SceneManager { get; }

        public DesignEngine(IInputElement inputElement, SceneManager<TDesignObject> sceneManager)
        {
            InputElement = inputElement ?? throw new ArgumentNullException(nameof(inputElement));
            SceneManager = sceneManager;
            WpfObjectDesigner = CreateObjectDesigner();
            Sequencer = WpfObjectDesigner.Sequencer;
            KeyboardBindings = new KeyboardBindings(inputElement);
            UIDesignMap = new UIDesignMap();
        }

        protected virtual WpfObjectDesigner CreateObjectDesigner()
        {
            return new WpfObjectDesigner(true);
        }

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
