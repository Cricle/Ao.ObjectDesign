using Ao.ObjectDesign.AutoBind;
using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Session;
using Ao.ObjectDesign.Session.BuildIn;
using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.Session.DesignHelpers;
using Ao.ObjectDesign.Session.Environment;
using Ao.ObjectDesign.Wpf.Data;
using Microsoft.Extensions.DependencyInjection;
using ObjectDesign.Brock.BindingCreators;
using ObjectDesign.Brock.Components;
using ObjectDesign.Brock.Controls;
using ObjectDesign.Brock.Controls.BindingCreators;
using ObjectDesign.Brock.InMemory;
using ObjectDesign.Brock.InputBindings;
using ObjectDesign.Brock.Level;
using ObjectDesign.Brock.Services;
using ObjectDesign.Brock.Views;
using System;
using System.IO.Abstractions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace ObjectDesign.Brock
{
    internal class MySceneMakerStartup : EnvironmentSceneMakerStartup<Scene, UIElementSetting>
    {
        private readonly IServiceProvider serviceProvider;

        public MySceneMakerStartup(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public override IDesignSessionSettings<Scene, UIElementSetting> CreateSessionSettings(Scene scene)
        {
            if (scene is null)
            {
                throw new ArgumentNullException(nameof(scene));
            }

            CheckState();

            var fs = Environment.FileSystem;

            var fi = fs.FileInfo.FromFileName("D:/123.view");
            var wp = fs.DirectoryInfo.FromDirectoryName("D:/");
            return new DesignSessionSettings<Scene, UIElementSetting>
            {
                Engine = Engine,
                Scene = scene,
                TargetFile = fi,
                WorkSpace = wp
            };
        }

        protected override IServiceProvider GetServiceProvider()
        {
            return serviceProvider;
        }
        public override void Ready()
        {
            CreateBuildIn()
                .AddMap<Rectangle, RectangleSetting>()
                .AddMap<Canvas, CanvasSetting>()
                .AddMap<TextBlock, TextBlockSetting>()
                .AddMap<TextBox, TextBoxSetting>()
                .AddMap<Line, LineSetting>()
                .AddMap<ProgressBar, ProgressBarSetting>()
                .AddMap<Image, RelativeFileImageSetting>()
                .AddMap<MediaElement, MediaElementSetting>()
                .AddBindingCreatorFactory(new TextBlockSettingBindingCreatorFactory())
                .AddBindingCreatorFactory(new RectangleSettingBindingCreatorFactory())
                .AddBindingCreatorFactory(new CanvasSettingBindingCreatorFactory())
                .AddBindingCreatorFactory(new TextBlockSettingBindingCreatorFactory())
                .AddBindingCreatorFactory(new TextBoxSettingBindingCreatorFactory())
                .AddBindingCreatorFactory(new ItemsControlSettingBindingCreatorFactory())
                .AddBindingCreatorFactory(new LineSettingBindingCreatorFactory())
                .AddBindingCreatorFactory(new ProgressBarSettingBindingCreatorFactory())
                .AddBindingCreatorFactory(new RelativeFileImageSettingBindingCreatorFactory())
                .AddBindingCreatorFactory(new MediaElementSettingBindingCreatorFactory())
                .AddTemplateForViewCondition(new PositionSizeCondition())
                .AddTemplateForViewCondition(new TextCondition())
                .AddTemplateForViewCondition(new OpacityCondition())
                .AddTemplateForViewCondition(new StretchCondition())
                .AddTemplateForViewCondition(new BrushDesignerCondition())
                .AddTemplateForViewCondition(new ClipToBoundsCondition())
                .AddTemplateForViewCondition(new StrokeThicknessCondition())
                .AddTemplateForViewCondition(new TextTrimmingCondition())
                .AddTemplateForViewCondition(new TextWrappingCondition())
                .AddTemplateForViewCondition(new FontSizeCondition())
                .AddTemplateForViewCondition(new NameCondition())
                .AddTemplateForViewCondition(new IsIndeterminateCondition())
                .AddTemplateForViewCondition(new TextAlignmentCondition())
                .AddTemplateForViewCondition(new RotateTransformDesignerCondition())
                .AddTemplateForViewCondition(new ResourceIdentityCondition())
                .AddTemplateForViewCondition(new MediaElementSourceCondition());
            Engine.DesignOrderManager
                .Add<FrameworkElementSetting>(x => x.Name)
                .Add<FrameworkElementSetting>(x => x.PositionSize)
                .Add<FrameworkElementSetting>(x => x.Opacity)
                .Add<TextBlockSetting>(x => x.Background)
                .Add<TextBoxBaseSetting>(x => x.Background)
                .Add<ShapeSetting>(x => x.Stroke)
                .Add<ShapeSetting>(x => x.Fill)
                .Add<TextBlock>(x => x.TextWrapping)
                .Add<TextBlock>(x => x.TextTrimming);
            Engine.TemplateContextsDecoraters.Add(new PropertyContextOrderDecoratere<Scene, UIElementSetting>(Engine.DesignOrderManager));
            Engine.TemplateContextsDecoraters.Add(new DesignStatePropertyContextsDecorater<Scene, UIElementSetting>(Engine));

            var runtime = serviceProvider.GetRequiredService<MySceneMakerRuntime>();
            runtime.AutoSwithDesignPanel = true;

        }
        private static readonly Style thumbStyle = (Style)App.Current.FindResource("SceneMaker.Thumb.EmptyBackground");
        public override IDesignSession<Scene, UIElementSetting> NewSession(Scene scene)
        {
            var session = base.NewSession(scene);
            var hp = new MoveDesignHelper<Scene, UIElementSetting>(session);
            hp.Thumb.Style = thumbStyle;
            session.Suface.Children.Add(hp);
            var rh = new ResizeDesignHelper<Scene, UIElementSetting>(session);
            rh.Thumb.Style = thumbStyle;
            session.Suface.Children.Add(rh);
            var dh = new DistanceDesignHelper<Scene, UIElementSetting>(session);
            session.Suface.Children.Add(dh);
            var bh = new BoundsDesignHelper<Scene, UIElementSetting>(session);
            session.Suface.Children.Add(bh);

            session.Suface.Update();

            session.Suface.Focusable = true;

            var runtime = Engine.ServiceProvider.GetRequiredService<MySceneMakerRuntime>();
            var engine = Engine.ServiceProvider.GetRequiredService<SceneEngine<Scene, UIElementSetting>>();
            var actionSettingService = Engine.ServiceProvider.GetRequiredService<ActionSettingService>();
            session.InputBindings.PreviewMouseInputs.Add(new SelectDesignInputBinding(runtime, engine.UIDesignMap));
            session.InputBindings.PreviewMouseInputs.Add(new TransferFocusInputBinding());
            session.InputBindings.PreviewMouseInputs.Add(new RangeSelectInputBinding(session, runtime));

            session.InputBindings.PreviewKeyboardInputs.Add(new CopyPasteInputBinding(session, engine));
            session.InputBindings.PreviewKeyboardInputs.Add(new SelectAllInputBinding(session));
            session.InputBindings.PreviewKeyboardInputs.Add(new DeleteInputBinding(session));
            session.InputBindings.PreviewKeyboardInputs.Add(new FallbackInputBinding(session));
            session.InputBindings.PreviewMouseInputs.Add(new CtrlCopyInputBinding(session, actionSettingService));
            session.InputBindings.PreviewMouseInputs.Add(new TextInputBinding(session));

            session.InputBindings.Listen();

            return session;
        }
        protected override void CreatingSession(IDesignSession<Scene, UIElementSetting> session)
        {
            base.CreatingSession(session);
            session.LazyBinding = true;
        }

        protected override IEngineEnvironment<Scene, UIElementSetting> CreateEnvironment(IFileSystem fileSystem,
            ISceneFetcher<Scene> fetcher, IBindingCreatorStateCreator<UIElementSetting> stateCreator)
        {
            return new MemoryEngineEnvironment(fileSystem, fetcher, stateCreator, GetServiceProvider());
        }

        protected override ISceneFetcher<Scene> GetSceneFetcher(IFileSystem fileSystem)
        {
            return new MemorySceneFetcher();
        }
    }
}
