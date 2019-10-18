using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IAccountService
    {
        void CreateNewAccount(int ownerId, string accountType);
        void CloseAccount(int accountId);
        void Withdraw(int accountId, decimal amount);
        void Transfer(int sourceAccountId, int destinationAccountId, decimal amountToTransfer);
        void TopUp(int accountId, decimal amount);
        IEnumerable<AccountEntity> GetAllAccountEntities();
        AccountEntity GetAccountEntity(int accountId);
    }
}
