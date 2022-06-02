using System.Collections.Generic;
using System.Text;

namespace Ao.ObjectDesign.Data
{
    public interface IDataProvider<T>: IDataProviderIdentity<T>
    {
        T Get();
    }
    public interface IDataProvider : IDataProvider<object>
    {
    }
    public interface IDataAnyProvider<T> : IDataProvider<T>, IAsyncDataProvider<T>
    {

    }
    public interface IDataAnyProvider : IDataAnyProvider<object>
    {

    }
}
