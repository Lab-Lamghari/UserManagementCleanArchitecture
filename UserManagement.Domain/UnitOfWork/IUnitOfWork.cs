using UserManagement.Domain.Repositories;

namespace UserManagement.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        void StartTransaction();
        void Commit();
    }
}