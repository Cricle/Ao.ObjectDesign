using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Session.Wpf;
using Ao.ObjectDesign.Session.Wpf.Desiging;
using Ao.ObjectDesign.Session.Wpf.Environment;
using ObjectDesign.Brock.Components;
using ObjectDesign.Brock.Level;
using System;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;

namespace ObjectDesign.Brock.InMemory
{
    public class MemoryEngineEnvironment : EngineEnvironmentBase<Scene, UIElementSetting>
    {
        public MemoryEngineEnvironment(IFileSystem fileSystem, ISceneFetcher<Scene> sceneFetcher, SceneEngine<Scene, UIElementSetting> engine, IServiceProvider serviceProvider) : base(fileSystem, sceneFetcher, engine, serviceProvider)
        {
        }

        public MemoryEngineEnvironment(IFileSystem fileSystem, ISceneFetcher<Scene> sceneFetcher, IBindingCreatorStateCreator<UIElementSetting> creator, IServiceProvider serviceProvider) : base(fileSystem, sceneFetcher, creator, serviceProvider)
        {
        }

        public override DesignClipboardManager<UIElementSetting> CreateClipboardManager(SceneEngine<Scene, UIElementSetting> engine)
        {
            return new MemoryDesignClipboardManager();
        }

        public static MemoryEngineEnvironment FromMock(IServiceProvider serviceProvider)
        {
            var sys = new MockFileSystem();
            var fetcher = new MemorySceneFetcher();
            return new MemoryEngineEnvironment(sys, fetcher,
                new BindingCreatorStateCreator<Scene, UIElementSetting>(new BindingCreatorStateDecoraterCollection<UIElementSetting>(), serviceProvider),
                serviceProvider);
        }
    }
}
