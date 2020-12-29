using System.Data;

namespace UserManagement.Persistence.Repositories
{    
    public class Repository
    {
        protected IDbConnection DbConnection { get; }
        protected IDbTransaction DbTransaction { get; }

        public Repository(IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            DbTransaction = dbTransaction;
            DbConnection = dbConnection;
        }
    }
}