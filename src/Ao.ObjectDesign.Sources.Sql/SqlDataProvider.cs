using Ao.ObjectDesign.Data;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Sources.Sql
{
    public class SqlDataProvider<T> : SqlDataProvider, IAsyncDataProvider<List<T>>, IDataProvider<List<T>>
    {
        public SqlDataProvider(Query query, Compiler compiler, IDbConnection dbConnection)
            : base(query, compiler, dbConnection)
        {
        }

        public List<T> GetAsData()
        {
            return (List<T>)GetData();
        }

        public async Task<List<T>> GetAsDataAsync()
        {
            var r=await GetDataAsync();
            return (List<T>)r;
        }

        protected override object CoreGetData(Query query)
        {
            return query.Get<T>(Transaction, Timeout);
        }

        protected override async Task<object> CoreGetDataAsync(Query query)
        {
            var r = await query.GetAsync<T>(Transaction, Timeout);
            return r.ToList();
        }
    }
    public abstract class SqlDataProvider : IDataProvider, IAsyncDataProvider,IDisposable
    {
        protected SqlDataProvider(Query query, Compiler compiler, IDbConnection dbConnection)
        {
            Query = query ?? new Query();
            Compiler = compiler ?? throw new ArgumentNullException(nameof(compiler));
            DbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            factory = new Lazy<QueryFactory>(CreateQueryFactory);
        }

        private Lazy<QueryFactory> factory;

        public Query Query { get; }

        public Compiler Compiler { get; }

        public IDbConnection DbConnection { get; }

        public IDbTransaction Transaction { get; set; }

        public int Timeout { get; set; } = 30;

        public QueryFactory Factory => factory.Value;
        
        public bool IsFactoryCreated => factory.IsValueCreated;

        public object GetData()
        {
            return CoreGetData(factory.Value.FromQuery(Query));
        }

        public Task<object> GetDataAsync()
        {
            return CoreGetDataAsync(factory.Value.FromQuery(Query));
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

        public void Dispose()
        {
            if (factory.IsValueCreated)
            {
                factory.Value.Dispose();
                factory = null;
                GC.SuppressFinalize(this);
            }
        }
    }
}
