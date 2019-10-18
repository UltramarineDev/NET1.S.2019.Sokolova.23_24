using System;
using System.Collections.Generic;
using DAL.Interface.DTO;

namespace DAL.Fake.Repositories
{
    public static class FakeAccountStorage
    {
        public static List<DalAccount> accounts = new List<DalAccount>()
        {
           new DalAccount()
           {
               Id = 1,
               AccountType = "BASE",
               Balance = 300,
               BonusPoints = 3,
               AccountOwnerId = 1,
               AccountNumber = Guid.NewGuid().ToString()
           },
           new DalAccount()
           {
               Id = 2,
               AccountType = "GOLD",
               Balance = 500,
               BonusPoints = 7,
               AccountOwnerId = 2,
               AccountNumber = Guid.NewGuid().ToString()
           },
           new DalAccount()
           {
               Id = 3,
               AccountType = "PLATINUM",
               Balance = 1000,
               BonusPoints = 10,
               AccountOwnerId = 3,
               AccountNumber = Guid.NewGuid().ToString()
           },
       };

        internal static List<DalAccountOwner> accountOwners = new List<DalAccountOwner>()
        {
            new DalAccountOwner()
            {
                FirstName = "Tom",
                LastName = "Hardi",
                Email = "tomhardi@gmail.com",
                Id = 1
            },
            new DalAccountOwner()
            {
                FirstName = "Michella",
                LastName = "Smith",
                Email = "michellasm@gmail.com",
                Id = 2
            },
            new DalAccountOwner()
            {
                FirstName = "Emily",
                LastName = "Hormand",
                Email = "emilyhormand@gmail.com",
                Id = 3
            }
        };
    }
}
