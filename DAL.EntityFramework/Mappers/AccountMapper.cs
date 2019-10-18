using DAL.Interface.DTO;
using ORM;

namespace DAL.EntityFramework.Mappers
{
    public static class AccountMapper
    {
        public static DalAccount ToDalAccount(this Account account)
        {
            return new DalAccount()
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                AccountOwnerId = account.AccountOwnerId,
                AccountType = account.AccountType,
                Balance = account.Balance,
                BonusPoints = account.BonusPoints
            };
        }

        public static Account ToAccountORM(this DalAccount dalAccount)
        {
            return new Account()
            {
                Id = dalAccount.Id,
                AccountNumber = dalAccount.AccountNumber,
                AccountOwnerId = dalAccount.AccountOwnerId,
                AccountType = dalAccount.AccountType,
                Balance = dalAccount.Balance,
                BonusPoints = dalAccount.BonusPoints
            };
        }
    }
}
