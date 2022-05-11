using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Sources.Sql
{
    public class DynamicsSqlDataProvider : SqlDataProvider
    {
        public DynamicsSqlDataProvider(Query query,
              Compiler compiler,
              IDbConnection dbConnection)
              : base(query, compiler, dbConnection)
        {
        }

        protected override object CoreGetData(Query query)
        {
            return query.Get(Transaction).ToList();
        }

        protected override async Task<object> CoreGetDataAsync(Query query)
        {
            var res = await query.GetAsync(Transaction);
            return res.ToList();
        }
    }
}
