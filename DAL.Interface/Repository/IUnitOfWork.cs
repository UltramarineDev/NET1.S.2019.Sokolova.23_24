using System;

namespace DAL.Interface.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts { get; }
        IOwnerRepository Owners { get; }
        int Complete();
    }
}
