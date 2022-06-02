using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Sources.Sql
{
    public class EnumerableDataProvider<T> : SqlDataProvider<IEnumerable<T>>
    {
        public EnumerableDataProvider(Query query, Compiler compiler, IDbConnection dbConnection) : base(query, compiler, dbConnection)
        {
        }

        protected override IEnumerable<T> CoreGetData(Query query)
        {
            return query.Get<T>(Transaction, Timeout);
        }

        protected override Task<IEnumerable<T>> CoreGetDataAsync(Query query)
        {
            return query.GetAsync<T>(Transaction, Timeout);
        }
    }
}
