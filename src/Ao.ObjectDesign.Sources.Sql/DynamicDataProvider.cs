using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Sources.Sql
{
    public class DynamicDataProvider : SqlDataProvider
    {
        public DynamicDataProvider(Query query, Compiler compiler, IDbConnection dbConnection) : base(query, compiler, dbConnection)
        {
        }

        protected override object CoreGetData(Query query)
        {
            return query.Get(Transaction, Timeout);
        }

        protected override async Task<object> CoreGetDataAsync(Query query)
        {
            var r = await query.GetAsync(Transaction, Timeout);
            return r.ToList();
        }
    }
}
