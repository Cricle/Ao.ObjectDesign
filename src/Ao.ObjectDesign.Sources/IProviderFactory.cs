using Ao.ObjectDesign.Data;

namespace Ao.ObjectDesign.Sources
{
    public interface IProviderFactory<TContext>: IProviderFactoryCondition<TContext>
    {
        IDataProvider GetDataProvider(TContext context);
    }
}
