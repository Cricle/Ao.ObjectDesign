using Ao.ObjectDesign.Data;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Sources
{
    public class InMemoryDataProvider : IDataProvider,IAsyncDataProvider
    {
        public object Value { get; set; }

        public object GetData()
        {
            return Value;
        }

        public Task<object> GetDataAsync()
        {
            return Task.FromResult(Value);
        }
    }
}
