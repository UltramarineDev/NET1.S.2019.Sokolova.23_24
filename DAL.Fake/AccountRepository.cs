using System;
using System.Collections.Generic;
using DAL.Interface.Repository;
using DAL.Interface.DTO;
using System.Linq.Expressions;

namespace DAL.Fake
{
    public class AccountRepository : IAccountRepository
    {
        private List<DalAccount> accountList;
        private List<DalAccountOwner> accountOwners;

        public AccountRepository()
        {
            accountList = new List<DalAccount>();
            accountList.AddRange(Repositories.FakeAccountStorage.accounts);
            accountOwners = new List<DalAccountOwner>();
            accountOwners.AddRange(Repositories.FakeAccountStorage.accountOwners);
        }

        public void Remove(DalAccount dalAccount)
        {
            if (IsAccountExists(dalAccount.Id))
            {
                var isRemoved = accountList.RemoveAll(x => x.AccountNumber.Equals(dalAccount.AccountNumber));
            }
        }

        public void Add(DalAccount dalAccount)
        {
            accountList.Add(new DalAccount()
            {
                AccountNumber = dalAccount.AccountNumber,
                AccountType = dalAccount.AccountType,
                AccountOwnerId = dalAccount.AccountOwnerId
            });
        }

        public bool IsOwnerExists(int ownerId)
            => accountOwners.Exists(x => x.Id.Equals(ownerId));

        public bool IsAccountExists(int accountId)
            => accountList.Exists(x=> x.Id == accountId);

        public void WithDraw(int accountId, decimal amount)
        {
            var accountInstance = accountList.Find(x => x.Id.Equals(accountId));
            accountInstance.Balance -= amount;
        }

        public void TopUp(int accountId, decimal amount)
        {
            var accountInstance = accountList.Find(x => x.Id.Equals(accountId));
            accountInstance.Balance += amount;
        }

        public void Transfer(int sourceAccountId, int destinationAccountId, decimal amountToTransfer)
        {
            WithDraw(sourceAccountId, amountToTransfer);
            TopUp(destinationAccountId, amountToTransfer);
        }

        public string GetAccountType(int accountId)
            => accountList.Find(x => x.Id.Equals(accountId)).AccountType;

        public void AddBonuses(int accountId, decimal bonuses)
        {
            var accountInstance = accountList.Find(x => x.Id.Equals(accountId));
            accountInstance.BonusPoints += bonuses;
        }

        public IEnumerable<DalAccount> GetOwnerAccounts(int ownerId)
            => accountList.FindAll(x => x.AccountOwnerId.Equals(ownerId));

        public DalAccount Get(int id)
            => accountList.Find(x => x.Id.Equals(id));

        public IEnumerable<DalAccount> GetAll()
            => accountList;

        public DalAccount Find(Expression<Func<DalAccount, bool>> predicate)
        {
            Func<DalAccount, bool> func = predicate.Compile();
            Predicate<DalAccount> pr = func.Invoke;
            return accountList.Find(pr);
        }
    }
}
