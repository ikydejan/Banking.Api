using System;
using System.Data;

namespace Banking.Api.Repositories.Interfaces
{
    public interface IDapperContext : IDisposable
    {
        IDbConnection db { get; }
		IDbTransaction transaction { get; }
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        void Commit();
        void Rollback();
        string GetGUID();
    }
}