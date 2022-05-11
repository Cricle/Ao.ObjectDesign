using Ao.ObjectDesign.Data;

namespace Ao.ObjectDesign.Sources
{
    public interface IAsyncProviderFactory : IProviderFactoryCondition
    {
        IAsyncDataProvider GetAsyncDataProvider();
    }
}
