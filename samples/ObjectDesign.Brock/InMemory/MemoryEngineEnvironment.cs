using Ao.ObjectDesign.Session;
using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.Session.Environment;
using Ao.ObjectDesign.WpfDesign;
using ObjectDesign.Brock.Components;
using ObjectDesign.Brock.Level;
using System;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;

namespace ObjectDesign.Brock.InMemory
{
    public class MemoryEngineEnvironment : EngineEnvironmentBase<Scene,UIElementSetting>
    {
        public MemoryEngineEnvironment(IFileSystem fileSystem,
               ISceneFetcher<Scene> sceneFetcher,
               SceneEngine<Scene,UIElementSetting> engine)
            :base(fileSystem,sceneFetcher,engine)
        {
        }


        public MemoryEngineEnvironment(IFileSystem fileSystem,
               ISceneFetcher<Scene> sceneFetcher, 
               IBindingCreatorStateCreator<UIElementSetting> bindingCreatorStateCreator)
            : base(fileSystem, sceneFetcher, bindingCreatorStateCreator)
        {
        }

        public override WpfDesignClipboardManager<UIElementSetting> CreateClipboardManager(SceneEngine<Scene, UIElementSetting> engine)
        {
            return new MemoryDesignClipboardManager();
        }

        public static MemoryEngineEnvironment FromMock(IServiceProvider serviceProvider)
        {
            var sys = new MockFileSystem();
            var fetcher = new MemorySceneFetcher();
            return new MemoryEngineEnvironment(sys, fetcher,
                new BindingCreatorStateCreator<Scene,UIElementSetting>(new BindingCreatorStateDecoraterCollection<UIElementSetting>(),serviceProvider));
        }
    }
}
