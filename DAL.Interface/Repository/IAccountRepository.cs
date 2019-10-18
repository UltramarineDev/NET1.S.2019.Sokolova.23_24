using System.Collections.Generic;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IAccountRepository: IRepository<DalAccount>
    {
        void WithDraw(int id, decimal amount);
        void TopUp(int id, decimal amount);
        void Transfer(int sourceAccountId, int destinationAccountId, decimal amountToTransfer);
        bool IsOwnerExists(int ownerId);
        bool IsAccountExists(int accountId);
        string GetAccountType(int accountId);
        void AddBonuses(int accountId, decimal bonuses);
        IEnumerable<DalAccount> GetOwnerAccounts(int ownerId);
    }
}
