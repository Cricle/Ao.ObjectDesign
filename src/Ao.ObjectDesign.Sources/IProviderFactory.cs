using Ao.ObjectDesign.Data;

namespace Ao.ObjectDesign.Sources
{
    public interface IProviderFactory: IProviderFactoryCondition
    {
        IDataProvider GetDataProvider();
    }
}
