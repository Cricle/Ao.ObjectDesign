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
    public abstract class SqlDataProvider<T> : DataAnyProviderBase<T>,IDisposable
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

        public override T Get()
        {
            return CoreGetData(factory.Value.FromQuery(Query));
        }

        public override Task<T> GetAsync()
        {
            return CoreGetDataAsync(factory.Value.FromQuery(Query));
        }

        protected abstract T CoreGetData(Query query);
        protected abstract Task<T> CoreGetDataAsync(Query query);

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
