using Ao.ObjectDesign;
using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.ForView;
using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.Session.EngineConfig;
using Ao.ObjectDesign.Session.Environment;
using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.WpfDesign;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Ao.ObjectDesign.Session
{
    public partial class SceneEngine<TScene,TSetting> : InitableObject
        where TScene:IDesignScene<TSetting>
    {
        public SceneEngine(IEngineConfiguration<TScene, TSetting> configuration)
        {
            EngineConfiguration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            configuration.EnsureNotNull();
            serviceProvider = configuration.ServiceProvider;
            EngineEnvironment = configuration.EngineEnvironment;
        }

        private IServiceProvider serviceProvider;
        private DesignOrderManager designOrderManager;
        private IBindingCreatorStateCreator<TSetting> bindingCreatorStateCreator;
        private WpfDesignClipboardManager<TSetting> clipboardManager;
        private WpfDesignPackage<TSetting> designPackage;
        private UIDesignMap uiDesignMap;
        private SessionManager<TScene,TSetting> sessionManager;
        private TemplateContextsDecoraterCollection<TScene, TSetting> templateContextsDecoraters;
        private IObjectDesigner instanceDesigner;
        private UIGenerator uiGenerator;
        private IForViewBuilder<FrameworkElement, WpfForViewBuildContext> forViewBuilder;
        private IForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext> dataTemplateBuilder;
        private BindingCreatorStateDecoraterCollection<TSetting> bindingCreatorStateDecoraters;

        public BindingCreatorStateDecoraterCollection<TSetting> BindingCreatorStateDecoraters => bindingCreatorStateDecoraters;

        public IForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext> DataTemplateBuilder => dataTemplateBuilder;

        public IForViewBuilder<FrameworkElement, WpfForViewBuildContext> ForViewBuilder => forViewBuilder;

        public IServiceProvider ServiceProvider => serviceProvider;

        public UIGenerator UIGenerator => uiGenerator;

        public IObjectDesigner InstanceDesigner => instanceDesigner;

        public TemplateContextsDecoraterCollection<TScene, TSetting> TemplateContextsDecoraters => templateContextsDecoraters;

        public DesignOrderManager DesignOrderManager => designOrderManager;

        public IBindingCreatorStateCreator<TSetting> BindingCreatorStateCreator => bindingCreatorStateCreator;

        public WpfDesignPackage<TSetting> DesignPackage => designPackage;

        public WpfDesignClipboardManager<TSetting> ClipboardManager => clipboardManager;

        public IEngineEnvironment<TScene,TSetting> EngineEnvironment { get; }

        public IEngineConfiguration<TScene,TSetting> EngineConfiguration { get; }

        public UIDesignMap UIDesignMap => uiDesignMap;

        public SessionManager<TScene,TSetting> SessionManager => sessionManager;

        protected override void OnInitialize()
        {
            Debug.Assert(EngineEnvironment != null);
            Debug.Assert(EngineConfiguration != null);
            base.OnInitialize();
            designOrderManager = CreateDesignOrderManager();
            bindingCreatorStateCreator = CreateBindingCreatorStateCreator();
            clipboardManager = CreateClipboardManager();
            templateContextsDecoraters = CreateDecoraterCollection();
            sessionManager = CreateSessionManager(templateContextsDecoraters);
            designPackage = (WpfDesignPackage<TSetting>)CreateDesignPackage();
            uiDesignMap = designPackage.UIDesinMap;
            instanceDesigner = CreateInstanceDesigner();
            forViewBuilder = CreateForViewBuilder();
            uiGenerator = CreateUIGenerator(forViewBuilder);
            dataTemplateBuilder = CreateDataTemplateBuilder();
            bindingCreatorStateDecoraters = CreateBindingCreatorStateDecoraters();
        }
        protected virtual BindingCreatorStateDecoraterCollection<TSetting> CreateBindingCreatorStateDecoraters()
        {
            return new BindingCreatorStateDecoraterCollection<TSetting>();
        }
        protected virtual IForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext> CreateDataTemplateBuilder()
        {
            return new ForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext>();
        }
        protected virtual IForViewBuilder<FrameworkElement, WpfForViewBuildContext> CreateForViewBuilder()
        {
            return new ForViewBuilder<FrameworkElement, WpfForViewBuildContext>();
        }
        protected virtual UIGenerator CreateUIGenerator(IForViewBuilder<FrameworkElement, WpfForViewBuildContext> forViewBuilder)
        {
            return new UIGenerator(instanceDesigner, forViewBuilder);
        }
        protected virtual IObjectDesigner CreateInstanceDesigner()
        {
            return ObjectDesigner.Instance;
        }
        protected virtual TemplateContextsDecoraterCollection<TScene, TSetting> CreateDecoraterCollection()
        {
            return new TemplateContextsDecoraterCollection<TScene, TSetting>();
        }

        protected virtual IBindingCreatorStateCreator<TSetting> CreateBindingCreatorStateCreator()
        {
            return EngineEnvironment.BindingCreatorStateCreator;
        }

        protected virtual SessionManager<TScene,TSetting> CreateSessionManager(TemplateContextsDecoraterCollection<TScene, TSetting> decoraters)
        {
            ThrowIfEnvironmentNull();
            return EngineEnvironment.CreateSessionManager(this);
        }
        protected virtual DesignOrderManager CreateDesignOrderManager()
        {
            return new DesignOrderManager();
        }

        protected virtual IDesignPackage<UIElement,TSetting,IWithSourceBindingScope> CreateDesignPackage()
        {
            return new WpfDesignPackage<TSetting>(new UIDesignMap(), EngineEnvironment.BindingCreatorStateCreator);
        }
        protected virtual WpfDesignClipboardManager<TSetting> CreateClipboardManager()
        {
            ThrowIfEnvironmentNull();
            return EngineEnvironment.CreateClipboardManager(this);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void ThrowIfEnvironmentNull()
        {
            Debug.Assert(EngineEnvironment != null, "When EngineEnvironment is null, can't create scene manager");
        }

        public WpfObjectDesignerSettings CreateDesignSettings()
        {
            return new WpfObjectDesignerSettings
            {
                DataTemplateBuilder = DataTemplateBuilder,
                Designer = InstanceDesigner,
                UIBuilder = ForViewBuilder,
                UIGenerator = UIGenerator
            };
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                SessionManager.Dispose();
                sessionManager = null;
            }
        }
    }
}
