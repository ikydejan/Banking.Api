using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Banking.Api.Repositories.Interfaces;

namespace Banking.Api.Repositories.Services
{
    public class DapperContext : IDapperContext
    {
        private IDbConnection _db;
        private IDbTransaction _transaction;
        internal UnitOfWork _uow;
        private readonly string _providerName = "Microsoft.Data.SqlClient";
        private readonly string _connectionString;

        public DapperContext()
        {
            _connectionString = "Data Source= localhost; Initial Catalog=Banking; User ID=slm; Password=P@ssw0rd; MultipleActiveResultSets=True;Encrypt=false;TrustServerCertificate=true";
        
            if (_db == null)
            {
                _db = GetOpenConnection(_providerName, _connectionString);
            }
        }
         

        private IDbConnection GetOpenConnection(string providerName, string connectionString)
        {
            DbConnection conn = null;

            try
            {
                SqlClientFactory provider = SqlClientFactory.Instance;
                conn = provider.CreateConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
            }
            catch
            {
            }

            return conn;
        }

        public IDbConnection db
        {
            get { return _db ?? (_db = GetOpenConnection(_providerName, _connectionString)); }
        }

        public IDbTransaction transaction
        {
            get { return _transaction; }
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (_transaction == null)
                _transaction = _db.BeginTransaction(isolationLevel);
        }

        public void Commit()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            if (_db != null)
            {
                try
                {
                    if (_db.State != ConnectionState.Closed)
                    {
                        if (_transaction != null)
                        {
                            _transaction.Rollback();
                        }

                        _db.Close();
                    }
                }
                finally
                {
                    _db.Dispose();
                }
            }

            GC.SuppressFinalize(this);
        }

        public string GetGUID()
        {
            var result = string.Empty;

            try
            {
                result = Guid.NewGuid().ToString();
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }
        }
    }
}