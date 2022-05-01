using System.Threading.Tasks;

namespace Ao.ObjectDesign.Data
{
    public interface IAsyncDataProvider
    {
        Task<object> GetDataAsync();
    }
}
