using Ao.ObjectDesign.Data;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Ao.ObjectDesign.Sources
{


    public interface IProviderFactory<TContext> : IProviderFactoryCondition<TContext>
    {
        IDataProvider GetDataProvider(TContext context);
    }
}
