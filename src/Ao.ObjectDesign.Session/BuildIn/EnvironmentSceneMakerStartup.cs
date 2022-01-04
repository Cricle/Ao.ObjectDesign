using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Desiging;
using Ao.ObjectDesign.Session.Environment;
using System.IO.Abstractions;

namespace Ao.ObjectDesign.Session.BuildIn
{
    public abstract class EnvironmentSceneMakerStartup<TScene, TSetting> : SceneMakerStartup<TScene, TSetting>
        where TScene : IDesignScene<TSetting>
    {
        protected override IEngineEnvironment<TScene, TSetting> GetEnvironment()
        {
            var fileSystem = GetFileSystem();
            var fetcher = GetSceneFetcher(fileSystem);
            var stateCreator = CreateStateCreator();
            return CreateEnvironment(fileSystem, fetcher, stateCreator);
        }

        protected abstract IEngineEnvironment<TScene, TSetting> CreateEnvironment(IFileSystem fileSystem, ISceneFetcher<TScene> fetcher, IBindingCreatorStateCreator<TSetting> stateCreator);

        protected abstract ISceneFetcher<TScene> GetSceneFetcher(IFileSystem fileSystem);

        protected virtual IFileSystem GetFileSystem()
        {
            return new FileSystem();
        }
        protected virtual IBindingCreatorStateCreator<TSetting> CreateStateCreator()
        {
            var provider = GetServiceProvider();
            var coll = CreateBindingCreatorStateDecoraterCollection();
            return new BindingCreatorStateCreator<TScene, TSetting>(coll, provider);
        }
        protected virtual BindingCreatorStateDecoraterCollection<TSetting> CreateBindingCreatorStateDecoraterCollection()
        {
            return new BindingCreatorStateDecoraterCollection<TSetting>();
        }
    }
}
