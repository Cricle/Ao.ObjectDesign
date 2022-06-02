using System.Threading.Tasks;

namespace Ao.ObjectDesign.Data
{
    public interface IAsyncDataProvider<T>: IDataProviderIdentity<T>
    {
        Task<T> GetAsync();
    }
    public interface IAsyncDataProvider: IAsyncDataProvider<object>
    {
    }
}
