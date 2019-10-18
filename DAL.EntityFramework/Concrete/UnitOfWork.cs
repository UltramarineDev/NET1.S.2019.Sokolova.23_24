using DAL.Interface.Repository;
using Logger.Interface;
using ORM;

namespace DAL.EntityFramework.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILog log;
        private readonly AccountSystemContext context;

        public UnitOfWork(AccountSystemContext context, ILog log)
        {
            this.context = context;
            this.log = log;

            Accounts = new AccountRepository(context, log);
            Owners = new OwnerRepository(context);
        }

        public IAccountRepository Accounts { get; private set; }
        public IOwnerRepository Owners { get; private set; }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
