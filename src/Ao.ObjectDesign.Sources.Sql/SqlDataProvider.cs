using Ao.ObjectDesign.Data;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Sources.Sql
{
    public abstract class SqlDataProvider : IDataProvider, IAsyncDataProvider
    {
        private static readonly Query EmptyQuery = new Query();

        protected SqlDataProvider(Query query, Compiler compiler, IDbConnection dbConnection)
        {
            Query = query ?? EmptyQuery.Clone();
            Compiler = compiler ?? throw new System.ArgumentNullException(nameof(compiler));
            DbConnection = dbConnection ?? throw new System.ArgumentNullException(nameof(dbConnection));
        }

        public Query Query { get; }

        public Compiler Compiler { get; }

        public IDbConnection DbConnection { get; }

        public int Timeout { get; set; } = 30;

        public object GetData()
        {
            using (var fc = CreateQueryFactory())
            {
                return CoreGetData(fc.FromQuery(Query));
            }
        }

        public async Task<object> GetDataAsync()
        {
            using (var fc = CreateQueryFactory())
            {
                return await CoreGetDataAsync(fc.FromQuery(Query));
            }
        }

        protected abstract object CoreGetData(Query query);
        protected abstract Task<object> CoreGetDataAsync(Query query);

        private QueryFactory CreateQueryFactory()
        {
            var factory = new QueryFactory(DbConnection, Compiler, Timeout);
            OnCreatedQueryFactory(factory);
            return factory;
        }

        protected virtual void OnCreatedQueryFactory(QueryFactory factory)
        {

        }
    }
}
