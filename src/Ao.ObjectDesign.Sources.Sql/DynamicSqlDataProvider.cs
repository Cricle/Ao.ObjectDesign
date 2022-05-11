using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Sources.Sql
{
    public class DynamicSqlDataProvider : SqlDataProvider
    {
        public DynamicSqlDataProvider(Query query,
            Compiler compiler,
            IDbConnection dbConnection)
            : base(query, compiler, dbConnection)
        {
        }

        protected override object CoreGetData(Query query)
        {
            return query.FirstOrDefault(Transaction);
        }

        protected override Task<object> CoreGetDataAsync(Query query)
        {
            return query.FirstOrDefaultAsync(Transaction);
        }
    }
}
