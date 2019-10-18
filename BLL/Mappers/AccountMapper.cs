using DAL.Interface.DTO;
using BLL.Interface.Entities;

namespace BLL.Mappers
{
    public static class AccountMapper
    {
        public static DalAccount ToDalAccount(this AccountEntity account)
        {
            return new DalAccount()
            {
                Id = account.AccountId,
                AccountNumber = account.AccountNumber,
                AccountOwnerId = account.AccountOwnerId,
                AccountType = account.AccountType,
                Balance = account.Balance,
                BonusPoints = account.BonusPoints
            };
        }

        public static AccountEntity ToBllAccount(this DalAccount dalAccount)
        {
            return new AccountEntity()
            {
                AccountId = dalAccount.Id,
                AccountNumber = dalAccount.AccountNumber,
                AccountOwnerId = dalAccount.AccountOwnerId,
                AccountType = dalAccount.AccountType,
                Balance = dalAccount.Balance,
                BonusPoints = dalAccount.BonusPoints
            };
        }
    }
}
