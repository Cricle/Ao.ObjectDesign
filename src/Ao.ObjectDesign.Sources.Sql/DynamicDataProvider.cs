using SqlKata;
using SqlKata.Compilers;
using System.Data;
using System.Linq;

namespace Ao.ObjectDesign.Sources.Sql
{
    public class DynamicDataProvider : EnumerableDataProvider<object>
    {
        public DynamicDataProvider(Query query, Compiler compiler, IDbConnection dbConnection) : base(query, compiler, dbConnection)
        {
        }
    }
}
