using System;
using System.Linq;
using Ninject;
using DependencyResolver;
using BLL.Interface.Services;
using DAL.EntityFramework.Concrete;
using ORM;
using DAL.Interface.DTO;
using Logger;

namespace ConsolePL
{
    class Program
    {
        private static readonly IKernel resolver;

        static Program()
        {
            resolver = new StandardKernel();
            resolver.ConfigurateResolverConsole();
        }

        static void Main(string[] args)
        {
            var service = resolver.Get<IAccountService>();

            var list = service.GetAllAccountEntities().ToList();
            foreach (var account in list)
            {
                Console.WriteLine(account.AccountNumber);
            }

            service.CloseAccount(list[0].AccountId);
            var listAfterClosing = service.GetAllAccountEntities().ToList();
            Console.WriteLine("After removing first account");
            foreach (var account in listAfterClosing)
            {
                Console.WriteLine(account.AccountNumber);
            }

            var accountBeforeWithdraw = service.GetAccountEntity(listAfterClosing[0].AccountId);
            Console.WriteLine("Before withdraw : {0}", accountBeforeWithdraw.Balance);
            service.Withdraw(listAfterClosing[0].AccountId, 10m);
            var accountAfterWithdraw = service.GetAccountEntity(listAfterClosing[0].AccountId);
            Console.WriteLine("After withdraw : {0}", accountAfterWithdraw.Balance);

            service.CreateNewAccount(1, "GOLD");
            var listAfterCreatingNewAccount = service.GetAllAccountEntities().ToList();
            Console.WriteLine("After creating new account");
            foreach (var account in listAfterCreatingNewAccount)
            {
                Console.WriteLine(account.AccountNumber, account.AccountType, account.AccountOwnerId);
            }

            var accountBeforeTopUp = service.GetAccountEntity(listAfterCreatingNewAccount[0].AccountId);
            Console.WriteLine("Before top up : {0}", accountBeforeTopUp.Balance);
            service.TopUp(listAfterCreatingNewAccount[0].AccountId, 10m);
            var accountAfterTopUp = service.GetAccountEntity(listAfterCreatingNewAccount[0].AccountId);
            Console.WriteLine("After top up : {0}", accountAfterTopUp.Balance);

            var sourceAccount = service.GetAccountEntity(listAfterCreatingNewAccount[0].AccountId);
            Console.WriteLine("Before transfer. Source account : {0}", sourceAccount.Balance);
            var destAccount = service.GetAccountEntity(listAfterCreatingNewAccount[1].AccountId);
            Console.WriteLine("Before transfer. Destination account : {0}", destAccount.Balance);
            service.Transfer(sourceAccount.AccountId, destAccount.AccountId, 10m);
            var sourceAccountAfterTransfer = service.GetAccountEntity(listAfterCreatingNewAccount[0].AccountId);
            Console.WriteLine("After transfer. Source account : {0}", sourceAccountAfterTransfer.Balance);
            var destAccountAfterTransfer = service.GetAccountEntity(listAfterCreatingNewAccount[1].AccountId);
            Console.WriteLine("After transfer. Destination account : {0}", destAccountAfterTransfer.Balance);

            Console.WriteLine("-----------------------------");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("-----------------------------");

            using (var unitOfWork = new UnitOfWork(new AccountSystemContext(), new NLogger()))
            {
                var accountBase = new DalAccount()
                {
                    Id = 1,
                    AccountType = "BASE",
                    Balance = 300,
                    BonusPoints = 3,
                    AccountOwnerId = 1,
                    AccountNumber = Guid.NewGuid().ToString()
                };
                var accountGold = new DalAccount()
                {
                    Id = 2,
                    AccountType = "GOLD",
                    Balance = 500,
                    BonusPoints = 7,
                    AccountOwnerId = 2,
                    AccountNumber = Guid.NewGuid().ToString()
                };
                var accountPlatinum = new DalAccount()
                {
                    Id = 3,
                    AccountType = "PLATINUM",
                    Balance = 1000,
                    BonusPoints = 10,
                    AccountOwnerId = 3,
                    AccountNumber = Guid.NewGuid().ToString()
                };

                var ownerFirst = new DalAccountOwner()
                {
                    FirstName = "Tom",
                    LastName = "Hardi",
                    Email = "tomhardi@gmail.com",
                    Id = 1
                };

                var ownerSecond = new DalAccountOwner()
                {
                    FirstName = "Michella",
                    LastName = "Smith",
                    Email = "michellasm@gmail.com",
                    Id = 2
                };

                var ownerThird = new DalAccountOwner()
                {
                    FirstName = "Emily",
                    LastName = "Hormand",
                    Email = "emilyhormand@gmail.com",
                    Id = 3
                };

                unitOfWork.Accounts.Add(accountBase);
                unitOfWork.Accounts.Add(accountGold);
                unitOfWork.Accounts.Add(accountPlatinum);

                unitOfWork.Owners.Add(ownerFirst);
                unitOfWork.Owners.Add(ownerSecond);
                unitOfWork.Owners.Add(ownerThird);

                unitOfWork.Complete();
            }
        }
    }
}