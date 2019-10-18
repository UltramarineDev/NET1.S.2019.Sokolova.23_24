using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Interface.Repository;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using Logger.Interface;

namespace BLL.ServiceImplementation
{
    public class AccountService : IAccountService
    {
        private readonly ILog log;
        private readonly IAccountRepository accountRepository;
        private readonly IBonuseCalculator bonusCalculator;
        private readonly IUnitOfWork unitOfWork;

        public AccountService(IAccountRepository accountRepository, IBonuseCalculator calculator, IUnitOfWork unitOfWork, ILog log)
        {
            this.accountRepository = accountRepository;
            this.bonusCalculator = calculator;
            this.unitOfWork = unitOfWork;
            this.log = log;
        }

        public AccountEntity GetAccountEntity(int accountId)
        {
            return accountRepository.Get(accountId).ToBllAccount();
        }

        public IEnumerable<AccountEntity> GetAllAccountEntities()
        {
            return accountRepository.GetAll().Select(account => account.ToBllAccount());
        }

        public void CreateNewAccount(int ownerId, string accountType)
        {
            if (string.IsNullOrEmpty(accountType))
            {
                log.Log("create new account - string is null or empty");
                throw new ArgumentException("Account type can not be null or empty", nameof(accountType));
            }

            if (!accountRepository.IsOwnerExists(ownerId))
            {
                log.Log("create new account - owner does not exists");
                throw new ArgumentException("Can not find owner id", nameof(ownerId));
            }

            AccountEntity account = new AccountEntity() { AccountOwnerId = ownerId, AccountType = accountType.ToUpper(), AccountNumber = Guid.NewGuid().ToString() };
            accountRepository.Add(account.ToDalAccount());
        }

        public void Withdraw(int accountId, decimal amount)
        {
            CheckAccountExistance(accountId);

            accountRepository.WithDraw(accountId, amount);

            var bonuses = bonusCalculator.CalculateBonuses(accountRepository.GetAccountType(accountId), amount);

            accountRepository.AddBonuses(accountId, bonuses);
        }

        public void TopUp(int accountId, decimal amount)
        {
            CheckAccountExistance(accountId);

            accountRepository.TopUp(accountId, amount);
        }

        public void CloseAccount(int accountId)
        {
            CheckAccountExistance(accountId);
            AccountEntity account = GetAccountEntity(accountId);
            accountRepository.Remove(account.ToDalAccount());
        }

        public void Transfer(int sourceAccountId, int destinationAccountId, decimal amountToTransfer)
        {
            CheckAccountExistance(sourceAccountId);
            CheckAccountExistance(destinationAccountId);

            accountRepository.Transfer(sourceAccountId, destinationAccountId, amountToTransfer);
        }

        private void CheckAccountExistance(int accountId)
        {
            if (!accountRepository.IsAccountExists(accountId))
            {
                log.Log("sercive method - account does not exists");
                throw new ArgumentException("Can not find account number", nameof(accountId));
            }
        }
    }
}
