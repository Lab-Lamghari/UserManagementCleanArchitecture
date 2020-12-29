using System.Data;
using UserManagement.Domain.Repositories;
using UserManagement.Domain.UnitOfWork;
using UserManagement.Persistence.Repositories;

namespace UserManagement.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection dbConnection;
        private IDbTransaction transaction;

        public UnitOfWork(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
            this.ManageConnection();
        }

        public IUserRepository Users => new UserRepository(this.dbConnection, this.transaction);
        //public IUserRepository Categories => new CategoryRepository(this.dbConnection, this.transaction);

        public void StartTransaction()
        {
            if (this.transaction == null)
            {
                this.transaction = this.dbConnection.BeginTransaction();
            }
        }
        public void Commit()
        {
            try
            {
                this.transaction.Commit();
            }
            catch
            {
                this.transaction.Rollback();
            }
        }

        private void ManageConnection()
        {
            if (this.dbConnection.State == ConnectionState.Closed)
            {
                this.dbConnection.Open();
            }
        }
    }
}