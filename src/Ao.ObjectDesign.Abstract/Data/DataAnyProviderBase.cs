using System.Threading.Tasks;

namespace Ao.ObjectDesign.Data
{
    public abstract class DataAnyProviderBase<T> : DataProviderIdentityBase<T>, IDataAnyProvider<T>
    {
        public abstract T Get();

        public virtual Task<T> GetAsync()
        {
            return Task.FromResult(Get());
        }
    }
}
