using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using BLL.Interface.Services;
using DAL.Interface.Repository;
using DAL.Interface.DTO;
using BLL.ServiceImplementation;
using NUnit.Framework;
using BLL.Mappers;
using Logger.Interface;

namespace BLL.Tests
{
    public class AccountServiceTests
    {
        [Test]
        public void CreateNewAccount_EmptyString_ThrowArgumentException()
        {
            var repository = new Mock<IAccountRepository>();
            var bonusCalculator = new Mock<IBonuseCalculator>();
            var unitow = new Mock<IUnitOfWork>();
            var logger = new Mock<ILog>();

            var service = new AccountService(repository.Object, bonusCalculator.Object, unitow.Object, logger.Object);

            Assert.Throws<ArgumentException>(() => service.CreateNewAccount(1, ""));
        }

        [Test]
        public void CreateNewAccount_OwnerIsNotExists_ThrowArgumentException()
        {
            var repository = new Mock<IAccountRepository>();
            var bonusCalculator = new Mock<IBonuseCalculator>();
            var unitow = new Mock<IUnitOfWork>();
            var logger = new Mock<ILog>();

            var service = new AccountService(repository.Object, bonusCalculator.Object, unitow.Object, logger.Object);

            Assert.Throws<ArgumentException>(() => service.CreateNewAccount(100, "GOLD"));
        }

        [Test]
        public void WithDraw_AccountsNotExists_ThrowArgumentException()
        {
            var repository = new Mock<IAccountRepository>();
            var bonusCalculator = new Mock<IBonuseCalculator>();
            var unitow = new Mock<IUnitOfWork>();
            var logger = new Mock<ILog>();

            var service = new AccountService(repository.Object, bonusCalculator.Object, unitow.Object, logger.Object);

            Assert.Throws<ArgumentException>(() => service.Withdraw(1, 50));
        }

        [Test]
        public void CreateNewAccount_InvalidAccountType_ThrowArgumentException()
        {
            var repository = new Mock<IAccountRepository>();
            var bonusCalculator = new Mock<IBonuseCalculator>();
            var unitow = new Mock<IUnitOfWork>();
            var logger = new Mock<ILog>();

            var service = new AccountService(repository.Object, bonusCalculator.Object, unitow.Object, logger.Object);

            Assert.Throws<ArgumentException>(() => service.CreateNewAccount(1, "NotExists"));
        }

        [TestCase(1, "BASE")]
        [TestCase(2, "GOLD")]
        [TestCase(1, "PLATINUM")]
        [TestCase(3, "BASE")]
        public void CreateNewAccount_ValidAccounts_CreatesNewAccounts(int ownerId, string accountType)
        {
            dynamic temp = null;
            var accountRepositoryMock = new Mock<IAccountRepository>();
            accountRepositoryMock.SetupSequence(x => x.GetAll())
                .Returns(new List<DalAccount>())
                .Returns(new List<DalAccount>() { temp });

            accountRepositoryMock.Setup(x => x.IsOwnerExists(It.IsAny<int>())).Returns(true);

            accountRepositoryMock.Setup(x => x.Add(It.IsAny<DalAccount>()))
                .Callback((DalAccount param) => temp = param.ToBllAccount());
            
            var repository = accountRepositoryMock.Object;
            var bonusCalculator = new Mock<IBonuseCalculator>().Object;
            var unitow = new Mock<IUnitOfWork>();
            var logger = new Mock<ILog>();

            var service = new AccountService(repository.Object, bonusCalculator.Object, unitow.Object, logger.Object);

            var listBeforeCount = repository.GetAll().ToList().Count;
            service.CreateNewAccount(ownerId, accountType);
            var listAfterCount = repository.GetAll().ToList().Count;

            Assert.AreNotEqual(listBeforeCount, listAfterCount);
            Assert.AreEqual(ownerId, temp.AccountOwnerId);
            Assert.AreEqual(accountType, temp.AccountType);
        }

        [Test]
        public void Transfer_InvalidSourceAccountNumber_ThrowArgumentException()
        {
            var repository = new Mock<IAccountRepository>();
            var bonusCalculator = new Mock<IBonuseCalculator>();
            var unitow = new Mock<IUnitOfWork>();
            var logger = new Mock<ILog>();

            var service = new AccountService(repository.Object, bonusCalculator.Object, unitow.Object, logger.Object);

            Assert.Throws<ArgumentException>(() => service.CloseAccount(647388));
        }
    }
}
