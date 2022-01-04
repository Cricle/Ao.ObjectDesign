using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Desiging;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.WpfDesign;
using Ao.ObjectDesign.WpfDesign.Input;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO.Abstractions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ao.ObjectDesign.Session.Desiging
{
    [DebuggerDisplay("{Id}")]
    public abstract class DesignSession<TScene, TSetting> : InitableObject, IDesignSession<TScene, TSetting>
        where TScene : IDesignScene<TSetting>
    {
        protected DesignSession(IDesignSessionSettings<TScene, TSetting> settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            Id = Guid.NewGuid();
            settings.Check();

            Debug.Assert(settings.TargetFile != null);
            Debug.Assert(settings.Engine != null);
            Debug.Assert(settings.WorkSpace != null);
            Debug.Assert(settings.Scene != null);

            Scene = settings.Scene;
            TargetFile = settings.TargetFile;
            Engine = settings.Engine;
            WorkSpace = settings.WorkSpace;
        }

        private WpfSceneManager<TScene, TSetting> sceneManager;
        private IPropertyPanel<TScene, TSetting> propertyPanel;
        private WpfObjectDesigner objectDesigner;
        private IActionSequencer<IFallbackable> sequencer;
        private TemplateContextsDecoraterCollection<TScene, TSetting> contextsDecoraters;
        private FrameworkElement root;
        private IList elementContriner;
        private AccessInputBindings inputBindings;
        private DesignSuface suface;

        public bool LazyBinding { get; set; }

        public DesignSuface Suface
        {
            get
            {
                ThrowIfDisposed();
                return suface;
            }
        }

        public FrameworkElement Root
        {
            get
            {
                ThrowIfDisposed();
                return root;
            }
        }

        public TScene Scene { get; }

        public SceneEngine<TScene, TSetting> Engine { get; }

        public Guid Id { get; }
        public IList ElementContriner
        {
            get
            {
                ThrowIfDisposed();
                return elementContriner;
            }
        }


        public WpfSceneManager<TScene, TSetting> SceneManager
        {
            get
            {
                ThrowIfDisposed();
                return sceneManager;
            }
        }

        public IPropertyPanel<TScene, TSetting> PropertyPanel
        {
            get
            {
                ThrowIfDisposed();
                return propertyPanel;
            }
        }

        public WpfObjectDesigner ObjectDesigner
        {
            get
            {
                ThrowIfDisposed();
                return objectDesigner;
            }
        }

        public IDirectoryInfo WorkSpace { get; }

        public IFileInfo TargetFile { get; }

        public TemplateContextsDecoraterCollection<TScene, TSetting> ContextsDecoraters
        {
            get
            {
                ThrowIfDisposed();
                return contextsDecoraters;
            }
        }

        public IActionSequencer<IFallbackable> Sequencer
        {
            get
            {
                ThrowIfDisposed();
                return sequencer;
            }
        }

        public AccessInputBindings InputBindings
        {
            get
            {
                ThrowIfDisposed();
                return inputBindings;
            }
        }

        protected override void OnInitialize()
        {
            ThrowIfDisposed();
            base.OnInitialize();
            objectDesigner = CreateObjectDesigner();
            sequencer = CreateSequencer();
            contextsDecoraters = CreateContextsDecoraters();
            root = CreateRoot();
            elementContriner = GetElementContainer(root);
            propertyPanel = CreatePropertyPanel();
            sceneManager = CreateSceneManager(objectDesigner);
            suface = CreateDesignSuface();
            inputBindings = CreateInputBindings();

        }

        protected virtual TemplateContextsDecoraterCollection<TScene, TSetting> CreateContextsDecoraters()
        {
            Engine.ThrowIfNoInitialized();
            return Engine.TemplateContextsDecoraters;
        }

        protected abstract WpfSceneManager<TScene, TSetting> CreateSceneManager(WpfObjectDesigner designer);

        protected abstract IPropertyPanel<TScene, TSetting> CreatePropertyPanel();

        protected abstract WpfObjectDesigner CreateObjectDesigner();

        protected virtual AccessInputBindings CreateInputBindings()
        {
            return new AccessInputBindings(suface);
        }
        protected virtual DesignSuface CreateDesignSuface()
        {
            return new DesignSuface
            {
                Background = Brushes.Transparent
            };
        }

        protected virtual IActionSequencer<IFallbackable> CreateSequencer()
        {
            return new ActionSequencer();
        }
        protected virtual FrameworkElement CreateRoot()
        {
            return new Canvas
            {
                ClipToBounds = true
            };
        }
        protected virtual IList GetElementContainer(FrameworkElement element)
        {
            if (element is Canvas c)
            {
                return c.Children;
            }
            throw new InvalidCastException($"Can't case {element} to {typeof(Canvas)}");
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing && inputBindings != null)
            {
                inputBindings.Dispose();
                sceneManager?.CurrentSceneController?.Dispose();
            }
        }
    }
}
