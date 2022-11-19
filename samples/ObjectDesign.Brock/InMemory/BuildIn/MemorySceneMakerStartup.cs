using Ao.ObjectDesign.Session.Wpf.BuildIn;
using Ao.ObjectDesign.Session.Wpf.Environment;
using ObjectDesign.Brock.Components;
using ObjectDesign.Brock.Level;

namespace ObjectDesign.Brock.InMemory.BuildIn
{
    public abstract class MemorySceneMakerStartup : SceneMakerStartup<Scene, UIElementSetting>
    {
        protected override IEngineEnvironment<Scene, UIElementSetting> GetEnvironment()
        {
            var provider = GetServiceProvider();
            return MemoryEngineEnvironment.FromMock(provider);
        }
    }
}
