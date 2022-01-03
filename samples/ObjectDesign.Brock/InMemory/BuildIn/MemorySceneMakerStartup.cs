using Ao.ObjectDesign.Session.BuildIn;
using Ao.ObjectDesign.Session.Environment;
using ObjectDesign.Brock.Components;
using ObjectDesign.Brock.Level;

namespace ObjectDesign.Brock.InMemory.BuildIn
{
    public abstract class MemorySceneMakerStartup : SceneMakerStartup<Scene, UIElementSetting>
    {
        protected override IEngineEnvironment<Scene,UIElementSetting> GetEnvironment()
        {
            var provider = GetServiceProvider();
            return MemoryEngineEnvironment.FromMock(provider);
        }
    }
}
