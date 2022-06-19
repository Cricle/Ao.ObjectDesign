using Ao.ObjectDesign.Data;

namespace Ao.ObjectDesign.Sources
{
    public interface IAsyncProviderFactory<TContext> : IProviderFactoryCondition<TContext>
    {
        IAsyncDataProvider GetAsyncDataProvider(TContext context);
    }
}
