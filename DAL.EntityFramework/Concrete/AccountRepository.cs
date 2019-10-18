using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using System.Data.Entity;
using DAL.EntityFramework.Mappers;
using ORM;
using System.Linq.Expressions;
using Logger.Interface;

namespace DAL.EntityFramework.Concrete
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DbContext context;
        private readonly ILog log;

        public AccountRepository(DbContext uow, ILog log)
        {
            this.context = uow;
            this.log = log;
        }

        public void Add(DalAccount entity)
        {
            context.Set<Account>().Add(entity.ToAccountORM());
            log.Log("Add method - Add account");
        }

        public void AddBonuses(int accountId, decimal bonuses)
        {
            var account = context.Set<Account>().FirstOrDefault(acc => acc.Id == accountId);
            account.Balance += bonuses;
        }

        public DalAccount Find(Expression<Func<DalAccount, bool>> predicate)
        {
            Expression<Func<DalAccount, Account>> convert =
            account => account.ToAccountORM();

            var param = Expression.Parameter(typeof(Account));
            var body = Expression.Invoke(predicate,
              Expression.Invoke(convert, param));

            var lambda = Expression.Lambda<Func<Account, bool>>(body, param);
            var func = lambda.Compile();

            return context.Set<Account>().Find(func).ToDalAccount();
        }

        public DalAccount Get(int id)
        {
            var ormAccount = context.Set<Account>().FirstOrDefault(account => account.Id == id);
            return ormAccount.ToDalAccount();
        }

        public string GetAccountType(int accountId)
            => context.Set<Account>().Find(accountId).AccountType;

        public IEnumerable<DalAccount> GetAll()
            => context.Set<Account>().Select(account => account.ToDalAccount());


        public IEnumerable<DalAccount> GetOwnerAccounts(int ownerId)
            => context.Set<AccountOwner>().Find(ownerId).Accounts.Select(x => x.ToDalAccount());

        public bool IsAccountExists(int accountId)
            => context.Set<Account>().Find(accountId) != null;

        public bool IsOwnerExists(int ownerId)
            => context.Set<AccountOwner>().Find(ownerId) != null;

        public void Remove(DalAccount entity)
        {
            var accountORM = entity.ToAccountORM();
            var account = context.Set<Account>().Single(u => u.Id == accountORM.Id);
            context.Set<Account>().Remove(account);

            log.Log($"Removed account {entity}");
        }

        public void TopUp(int id, decimal amount)
        {
            var account = context.Set<Account>().FirstOrDefault(acc => acc.Id == id);
            account.Balance += amount;
        }

        public void Transfer(int sourceAccountId, int destinationAccountId, decimal amountToTransfer)
        {
            var sourceAccount = context.Set<Account>().FirstOrDefault(acc => acc.Id == sourceAccountId);
            var destAccount = context.Set<Account>().FirstOrDefault(acc => acc.Id == destinationAccountId);
            sourceAccount.Balance -= amountToTransfer;
            destAccount.Balance += amountToTransfer;
        }

        public void WithDraw(int id, decimal amount)
        {
            var account = context.Set<Account>().FirstOrDefault(acc => acc.Id == id);
            account.Balance -= amount;
        }
    }
}
